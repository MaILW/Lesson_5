using System;

namespace Lesson_5
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeStructure tree = new TreeStructure();
            //TreeStructure get = new TreeStructure("get");
            //TreeStructure help = new TreeStructure("help");
            var get = tree.AddChildren("get");
            get.SetNodeFunc(cw);
            tree.AddChildren("help");
            tree.AddChildren("get_2").AddChildren("get_22_1").AddChildren("get_2_1_1");
            tree.AddChildren("get_3");
            tree.AddChildren("get_4");
            tree.FindNodeInAllChild("get_2")?.AddChildren("get_2_2");
            tree.FindNodeInAllChild("get_22_1")?.AddChildren("get_2_1_2");
            tree.FindNodeInAllChild("help")?.AddChildren("FullHelp")?.SetNodeFunc(cw);
            tree.FindNodeInAllChild("help").SetNodeFunc(cw);
            //get.Parent = tree;
            // help.Parent = tree;
            get.AddChildren("get_1");
            //Console.WriteLine(TreeStructure.FindNodeInChild("get", TreeStructure.FindNode("help"))?.Name);
            //Console.WriteLine(TreeStructure.FindNode("get_1")?.Name);
            //get.SetNodeFunc(cw);
            Console.WriteLine();
            TreeStructure.PrintTree(tree, 0);
            Console.WriteLine();
            do
            {
                TreeStructure.EnterCommand();
            } while (true);
        }
        void ClearLine(int line)
        {
            Console.MoveBufferArea(0, line, Console.BufferWidth, 1, Console.BufferWidth, line, ' ', Console.ForegroundColor, Console.BackgroundColor);
        }
        static public void cw()
        {
            Console.WriteLine("method cw");
        }
    }
}
