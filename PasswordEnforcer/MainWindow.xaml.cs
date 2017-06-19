using PasswordEnforcer.model;
using PasswordEnforcer.viewModel;
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

namespace PasswordEnforcer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainModel();
        }
       
        private void cb_enforced_Loaded(object sender, RoutedEventArgs e)
        {
            cb_enforced.SelectedIndex = 0;
        }

        private void cb_notallowed_Loaded(object sender, RoutedEventArgs e)
        {
            cb_notallowed.SelectedIndex = 0;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            util.Util.writeRegistryKey();
        }

        private void btn_apply_Click(object sender, RoutedEventArgs e)
        {
            Topology t = (Topology)cb_enforced.SelectedItem;
            Console.WriteLine("Selektovan u prvom combu: \n" + t.toString());
        }

        private void cb_notallowed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = cb_notallowed.SelectedIndex;
            if(ind != 0)
            {
                cb_enforced.IsEnabled = false;
            }
            else
            {
                cb_enforced.IsEnabled = true;
            }
            
        }

        private void cb_enforced_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = cb_enforced.SelectedIndex;
            if (ind != 0)
            {
                cb_notallowed.IsEnabled = false;
            }
            else
            {
                cb_notallowed.IsEnabled = true;
            }
        }
    }
}
