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
            tree.AddChildren("help");
            tree.FindNodeInAllChild("help")?.AddChildren("FullHelp")?.SetNodeFunc(cw);
            //get.Parent = tree;
            // help.Parent = tree;
            get.AddChildren("get_1");
            //Console.WriteLine(TreeStructure.FindNodeInChild("get", TreeStructure.FindNode("help"))?.Name);
            //Console.WriteLine(TreeStructure.FindNode("get_1")?.Name);
           //get.SetNodeFunc(cw);
            TreeStructure.EnterCommand();
        }
        static public void cw()
        {
            Console.WriteLine("method cw");
        }
    }
}
