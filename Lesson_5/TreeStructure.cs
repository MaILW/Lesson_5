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
        


        public override string ToString()
        {
            return this.Name;
        }
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
        //public TreeStructure(string name, Function func) : this(name)
        //{
        //    Funk = func;
        //}
        public TreeStructure AddChildren (string child)
        {
            if (child.IndexOf(' ') != -1)
                throw new Exception("Имя узла содержит пробел!");
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

        [Obsolete("frtgyhujik")]
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
        public static TreeStructure FindNodeInChild(out bool sucsess, string node, TreeStructure startNode)
        {
            TreeStructure findingNode = startNode;
            sucsess = false;
            foreach (var child in startNode.children)
            {
                if (child?.Name == node)
                {
                    findingNode = child;
                    sucsess = true;
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
            if (startNode == null)
                return findingNode;
            foreach (var child in startNode?.children)
            {
                if (child?.Name == node)
                {
                    findingNode = child;
                    break;
                }
            }
            //if (findingNode == null)
                //Console.WriteLine("Узел не найден.");
            return findingNode;
        }
        public static TreeStructure[] FindNameInChild(string node, TreeStructure startNode)
        {
            TreeStructure[] findingNode = new TreeStructure[0];
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
                Console.WriteLine("\nУзел не найден.");
            return findingNode;
        }
        public static void EnterCommand()
        {
            //FindNode("asd");
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
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("vvod com\nVvod: ");
            Console.ResetColor();
            string comm = "";
            do
            {
                ConsoleKeyInfo ski;
                ski = Console.ReadKey(true);
                
                if(ski.Key == ConsoleKey.Tab)
                {
                    
                    while(Console.CursorLeft != 0)
                    {
                        Console.Write("\b \b");
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tab work:");
                    Console.ResetColor();
                    comm =  AutoTeste(comm, RootOfTree);
                    comm = comm.Trim();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Vvod: " + comm);
                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(ski.KeyChar);
                    comm += ski.KeyChar;
                    Console.ResetColor();
                }

            } while (true);
            Console.WriteLine();
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
            if (String.IsNullOrWhiteSpace(comm))
            {
                foreach (var item in RootOfTree.children)
                {
                    Console.WriteLine(item.Name);
                }
            }
            if(command.Length == 1)
            {
                TreeStructure[] findedNode = FindNameInChild(command[0], nodeToFind);
                if (findedNode.Length == 0)
                {
                    //Console.Write(comm);
                    return comm;
                }
                if (findedNode.Length == 1)
                {
                    if (findedNode[0].children != null)
                    {
                        foreach (var item in findedNode)
                        {
                            PrintTree(item);
                        }
                    }
                    //Console.CursorLeft = 0;
                    //Console.Write(findedNode[0].Name);
                    return findedNode[0].Name;
                }
                foreach (var item in findedNode)
                {
                    PrintTree(item);
                }
                //Console.Write(comm);
                return comm;
            }
            else
            {
                TreeStructure codeCommand = nodeToFind;
                bool sucsess = false;
                foreach (var com in command)
                {
                    if(FindNameInChild(com, codeCommand).Length == 1)
                    codeCommand = FindNodeInChild(out sucsess, com, codeCommand);
                }
                if(sucsess && (FindNameInChild(command[^1], codeCommand).Length == 1))
                {
                    Console.WriteLine();
                    if (codeCommand.children != null)
                    {
                       
                            PrintTree(codeCommand);
                        
                    }
                    //Console.Write(comm);
                    return comm;
                }
                else
                {
                    if (command.Length == 0)
                        return comm;
                    TreeStructure[] findedNode = FindNameInChild(command[^1], codeCommand);
                    if (findedNode.Length == 0)
                    {
                        return comm;
                    }
                    if (findedNode.Length == 1)
                    {
                        if (findedNode[0].children != null)
                        {
                            Console.WriteLine();
                            foreach (var item in findedNode)
                            {
                                PrintTree(item);
                            }
                        }
                        //Console.Write(comm);
                        //Console.CursorLeft -= command[^1].Length;
                        //Console.Write(findedNode[0].Name);
                        return comm.Remove(comm.LastIndexOf(command[^1])) + findedNode[0].Name;
                    }
                    Console.WriteLine();
                    foreach (var item in findedNode)
                    {
                        PrintTree(item);
                    }
                    //Console.Write(comm);
                    return comm;
                }
            }
        }

        public static void PrintTree(TreeStructure firstNode,int PositionCursor = 0)
        {
            
            Console.CursorLeft = PositionCursor;
            Console.Write(firstNode.Name + " ");
            PositionCursor += RootOfTree.Name.Length + 3;
            foreach (var child in firstNode.children)
            {
                PrintTree(child,PositionCursor);
                Console.WriteLine();
                
            }
            if(firstNode.children.Length == 0)
                Console.WriteLine();
        }
           
    }
}
