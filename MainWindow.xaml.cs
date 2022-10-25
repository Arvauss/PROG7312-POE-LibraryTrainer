using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void title_Click(object sender, MouseButtonEventArgs e)
        {
            this.MainFrame.Content = new MainMenu();
        }


        /* private void title_Click(object sender, MouseButtonEventArgs e)
         {
             MainFrame.Content = null;
             MainFrame.Content = new MenuItem();
         }*/
        //On click, shows the screen of the first game.
        /* private void btnGame1_Click(object sender, RoutedEventArgs e)
         {
             Game1_Replace window1 = new Game1_Replace();
             window1.Show();
             this.Close();
         }*/

    }
}
