using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryTrainer
{
    /// <summary>
    /// Interaction logic for Game3.xaml
    /// </summary>
    public partial class Game3 : Page
    {
        public static Random random = new Random();
        static TreeNode<Dewey> root = new TreeNode<Dewey>(new Dewey("root", "root"));

        public static int GameMode = 0;

        static List<Dewey> QA = new List<Dewey>();
        static List<int> QAindexes = new List<int>();

        public Game3()
        {
            InitializeComponent();
            btnEnd.IsEnabled = false;
            rbOne.Visibility = Visibility.Hidden;
            rbTwo.Visibility = Visibility.Hidden;
            rbThr.Visibility = Visibility.Hidden;
            rbFou.Visibility = Visibility.Hidden;

            InitTree();
            MessageBox.Show("Tree initialised");
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnEnd.IsEnabled = false;

            CalcQA();
            PopAnswers();


        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {

            CalcQA();
            
        }

        public void CalcQA()
        {
            QA.Clear();
            QAindexes.Clear();
            int lvl1i = GetRandomNumber(0, root.GetCount());
            TreeNode<Dewey> lvl1 = root[lvl1i];
            QA.Add(lvl1.Value);
            QAindexes.Add(lvl1i);

            int lvl2i = GetRandomNumber(0, lvl1.GetCount());
            TreeNode<Dewey> lvl2 = lvl1[lvl2i];
            QA.Add(lvl2.Value);

            int lvl3i = GetRandomNumber(0, lvl2.GetCount());
            TreeNode<Dewey> lvl3 = lvl2[lvl3i];
            QA.Add(lvl3.Value);

            lblQ.Content = QA[2].callDesc;

            /*string outp = "";
            foreach (var item in QA)
            {
                outp += $"\n{item.callNum} {item.callDesc}";
            }

            MessageBox.Show(outp);*/
        }

        public void PopAnswers()
        {
            List<int> indexes = new List<int>();
            List<Dewey> ans = new List<Dewey>();
            List<TreeNode<Dewey>> sourceL1;

            if (GameMode == 0)
            {
                ans.Add(QA[0]);
                sourceL1 = root.Children;
                for (int i = 0; i < 3; i++)
                {
                    int index = GetRandomNumber(0, sourceL1.Count());
                    if (index == QAindexes[0])
                    {
                        i--;
                        continue;
                    }

                    if (!indexes.Contains(index)) { 
                        indexes.Add(index);
                        ans.Add(sourceL1[index].Value);
                    } 
                    else i--;
                }

                

                ans = ans.OrderBy(x => x.callNum).ToList();
                QAindexes[0] = ans.IndexOf(QA[0]);
                MessageBox.Show(QAindexes[0].ToString());

                rbOne.Content = ans[0].ToString();
                rbTwo.Content = ans[1].ToString();
                rbThr.Content = ans[2].ToString();
                rbFou.Content = ans[3].ToString();

                rbOne.Visibility = Visibility.Visible;
                rbTwo.Visibility = Visibility.Visible;
                rbThr.Visibility = Visibility.Visible;
                rbFou.Visibility = Visibility.Visible;

            }
        }


        //generic method to get randomised number within specified parameters
        public static int GetRandomNumber(int min, int max)
        {
            // (Microsoft, 2022)
            int d = random.Next(min, max);
            return d;
        }

        public void InitTree()
        {
            string dir = Environment.CurrentDirectory;
            string filePath = Directory.GetParent(dir).Parent.Parent.FullName + "\\deweysystem.txt";
            string[] sysLines = File.ReadAllLines(filePath);

            List<String> tempL1 = new List<String>();
            List<String> tempL2 = new List<String>();

            


            foreach (string line in sysLines)
            {
                string[] objs = line.Split('/');

                Dewey call1 = splitString(objs[0]);
                TreeNode<Dewey> nodeLvl1;
                Console.WriteLine(call1.callNum + " " + call1.callDesc);

                if (root.Children.Count() == 0)
                {
                    Console.WriteLine("Node added becuase no children exist");
                    nodeLvl1 = root.AddChild(call1);
                    tempL1.Add(call1.callNum);

                }
                else
                {
                    if (tempL1.Contains(call1.callNum))
                    {
                        nodeLvl1 = FindNode(root, call1);
                        Console.WriteLine("Node found");
                    }
                    else
                    {
                        tempL1.Add(call1.callNum);
                        nodeLvl1 = root.AddChild(call1);
                        Console.WriteLine("Node added becuase does not exist");
                    }

                }

                Dewey call2 = splitString(objs[1]);
                TreeNode<Dewey> nodeLvl2;

                if (nodeLvl1 == null)
                {
                    nodeLvl1 = FindNode(root, call1);
                }

                if (!tempL2.Contains(call2.callNum))
                {
                    nodeLvl2 = nodeLvl1.AddChild(call2);
                    tempL2.Add(call2.callNum);
                }
                else
                {
                    nodeLvl2 = FindNode(nodeLvl1, call2);
                }

                Dewey call3 = splitString(objs[2]);
                TreeNode<Dewey> nodeLvl3;

                nodeLvl3 = nodeLvl2.AddChild(call3);
            }
        }
        public static Dewey splitString(string line)
        {
            string[] div = line.Split("-");

            return new Dewey(div[0], div[1]);
        }

        public static TreeNode<Dewey> FindNode(TreeNode<Dewey> parent, Dewey search)
        {
            foreach (var node in parent.Children)
            {
                if (node.Value.callNum.Equals(search.callNum))
                    return node;
            }

            return null;
        }
    }
}
