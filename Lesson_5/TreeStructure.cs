using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_5
{
    class TreeStructure
    {

        public delegate void Function();
        Function Funk = delegate () {
            Console.WriteLine("Значение функции по умолчанию");
        };
        
        public TreeStructure Parent { get; private set; }
        public TreeStructure[] children = new TreeStructure[0];
        public string Name { get; private set; }
        private static TreeStructure RootOfTree { get; set; }

        public TreeStructure()
        {
            Name = "RootOfTree";
            RootOfTree = this;
            Console.WriteLine($"Name of root: {RootOfTree.Name}");
        }
        private TreeStructure(string name)
        {
            Name = name;
        }
        public TreeStructure AddChildren (string child)
        {
            Array.Resize(ref children, children.Length + 1);
            children[^1] = new TreeStructure(child) { Parent = this };
            return children[^1];
        }

        public void SetNodeFunc(Function funk)
        {
            Funk = funk;
            Console.WriteLine("Funk seted.");
        }
        public TreeStructure FindNodeInAllChild(string node)
        {
            TreeStructure findingNode = null;
            if(this.Name == node)
            {
                findingNode = this;
                return findingNode;
            }
            foreach ( var child in children)
            {
                if(child?.Name == node)
                {
                    findingNode = child;
                    break;
                }
                var temp = child.FindNodeInAllChild(node);
                if ( temp?.Name == node)
                {
                    findingNode = temp;
                    break;
                }
                
            }
            //if (findingNode == null)
            //    Console.WriteLine("Узел не найден.");
            return findingNode;
        }

        
        public static TreeStructure FindNode(string node)
        {
            TreeStructure findingNode = null;
            foreach (var child in RootOfTree.children)
            {
                var temp = child.FindNodeInAllChild(node);
                if (temp?.Name == node) 
                { 
                    findingNode =  temp;
                    break;

                }
            }
            if (findingNode == null)
                Console.WriteLine("Узел не найден.");
            return findingNode;
        }
        public static TreeStructure FindNodeInChild(string node, TreeStructure startNode)
        {
            TreeStructure findingNode = null;
            foreach (var child in startNode.children)
            {
                if (child?.Name == node)
                {
                    findingNode = child;
                    break;
                }
            }
            if (findingNode == null)
                Console.WriteLine("Узел не найден.");
            return findingNode;
        }
        public static TreeStructure[] FindNameInChild(string node, TreeStructure startNode)
        {
            TreeStructure[] findingNode = new TreeStructure[0];
            int index = 0;
            foreach (var child in startNode.children)
            {
                string name = child?.Name;
                if (name.StartsWith(node))
                {
                    Array.Resize(ref findingNode, findingNode.Length + 1);
                    findingNode[^1] = child;
                }
            }
            if (findingNode.Length == 0)
                Console.WriteLine("Узел не найден.");
            return findingNode;
        }
        public static void EnterCommand()
        {
            static bool CompareTo(char ch)
            {
                string alph = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM _-,.;:()1234567890";
                foreach(char b in alph)
                {
                    if (ch == b)
                        return true;
                }
                return false;
            }
            Console.WriteLine("vvod com");
            string comm = "";
            do
            {
                ConsoleKeyInfo ski;
                ski = Console.ReadKey(true);
                
                if(ski.Key == ConsoleKey.Tab)
                {
                    //Console.WriteLine("tab");
                    //Console.WriteLine();
                    comm =  AutoTeste(comm, RootOfTree);
                    Console.CursorLeft = 0;
                    Console.Write(comm);
                    //if(auto != null)
                    //{
                    //    comm = auto.Name;// peredelat'
                    //    break;
                    //}
                    continue;
                }
                if(ski.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if(ski.Key == ConsoleKey.Backspace)
                {
                    Console.Write("\b \b");
                    if (comm.Length != 0)
                    {
                        comm = comm.Remove(comm.Length - 1);
                    }
                }
                if (CompareTo(ski.KeyChar))
                {
                    Console.Write(ski.KeyChar);
                    comm += ski.KeyChar;
                }

            } while (true);
            Console.WriteLine();
            //Console.ReadLine();
            string[] command = comm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            TreeStructure codeCommand = RootOfTree;
            foreach (var com in command)
            {
                codeCommand = FindNodeInChild(com, codeCommand);
            }
            Console.WriteLine(codeCommand?.Name);
            codeCommand?.Funk();
        }


        private static string AutoTeste(string comm, TreeStructure nodeToFind)
        {
            string[] command = comm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(command.Length == 1)
            {
                TreeStructure[] findedNode = FindNameInChild(command[0], nodeToFind);
                if (findedNode.Length == 0)
                {
                    return null;
                }//dodelat
                if (findedNode.Length == 1)
                {
                    return findedNode[0].Name;
                }
                foreach (var item in findedNode)
                {
                    PrintTree(item);
                }
            }
            else
            {
                //TreeStructure[] findedNode = FindNameInChild(command[0], nodeToFind);
                TreeStructure codeCommand = nodeToFind;
                foreach (var com in command)
                {
                    codeCommand = FindNodeInChild(com, codeCommand);
                }

            }
            return null;
        }
        private static TreeStructure AutoPaste(string comm, TreeStructure nodeToFind)
        {
            static string returnComm(TreeStructure node)
            {

                return null;
            }
            string[] command = comm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var partOfComm in command)
            {


                TreeStructure[] findedNode = FindNameInChild(partOfComm, nodeToFind);
                if (findedNode.Length == 0)
                {
                    return null;
                }//dodelat
                if (findedNode.Length == 1)
                {

                    return findedNode[0];
                }
                foreach (var item in findedNode)
                {
                    PrintTree(item);
                }
            }
            return null;
        }

        public static void PrintTree(TreeStructure firstNode,int PositionCursor = 0)
        {
            Console.CursorLeft = PositionCursor;
            Console.Write(firstNode.Name + " ");
            PositionCursor += RootOfTree.Name.Length + 3;
           
            //Console.CursorLeft = PositionCursor;
            foreach (var child in firstNode.children)
            {
                
                //Console.Write(child.Name + "\t");
                PrintTree(child,PositionCursor);
                Console.WriteLine();
                
            }
        }

        //public TreeStructure this[int index]
        //{
        //    get
        //    {
        //        return children[index];
        //    }
        //}
           
    }
}
