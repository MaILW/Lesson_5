using System;


namespace Lesson_5
{
    class Program
    {

        static void test_1()
        {
            Console.WriteLine("keklol");
        }

        static void Main(string[] args)
        {
            
            TreeStructure tree = new TreeStructure();
            var get = tree.AddChildren("get");
            tree.AddChildren("help");
            tree.AddChildren("test_1").AddChildren("tt").SetNodeFunc(test_1);
            tree.AddChildren("test_1").AddChildren("tt").SetNodeFunc(test_1);
            tree.AddChildren("get_2").AddChildren("get_22_1").AddChildren("get_2_1_1");
            tree.AddChildren("get_3");
            tree.AddChildren("get_4");
            tree.FindNodeInAllChild("get_2").AddChildren("get_2_2");
            tree.FindNodeInAllChild("get_22_1").AddChildren("get_2_1_2");
            tree.FindNodeInAllChild("help").AddChildren("FullHelp");
            tree.FindNodeInAllChild("help").SetNodeFunc(cww);
            get.AddChildren("get_1");
            Console.WriteLine();
            TreeStructure.PrintTree(tree, 0);
            tree.FindNodeInAllChild("get_22_1").SetNodeFunc(cww);
       

 

            Console.WriteLine();
            do
            {
                TreeStructure.EnterCommand();
            } while (true);
        }
        static public int cw()
        {
            Console.WriteLine("method cw");
            return 11;
        }
        static public void cww()
        {
            Console.WriteLine("method cw");
            return ;
        }
    }
}
