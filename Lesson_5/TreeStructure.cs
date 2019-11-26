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
                    Console.WriteLine("tab");
                    continue;
                }
                if(ski.Key == ConsoleKey.Enter)
                {
                    
                    break;
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



        //public TreeStructure this[int index]
        //{
        //    get
        //    {
        //        return children[index];
        //    }
        //}
           
    }
}
