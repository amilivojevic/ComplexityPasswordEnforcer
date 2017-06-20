using PasswordEnforcer.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PasswordEnforcer
{
    /// <summary>
    /// Interaction logic for CustomizeNotAllowedTopology.xaml
    /// </summary>
    public partial class CustomizeNotAllowedTopology : Window
    {
        public CustomizeNotAllowedTopology(ObservableCollection<Topology> cb_data)
        {
            InitializeComponent();
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            double len;
            if (double.TryParse(tb_length.Text, out len))
            {
                // broj!

                //nije u trazenom opsegu
                if(len<1 || len > 14)
                {
                    MessageBox.Show("Regular expession length must be in range [1-14]!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tb_length.Text = "";
                } else
                {
                    /*
                    Topology t = new Topology(tb_name.Text, tb_regex.Text, false, false, 0);
                    Console.WriteLine("Size: pre " + MainWindow.cb_data_notallowed.Count);
                    MainWindow.cb_data_notallowed.Add(
                        new Topology(
                            t.name,
                            t.regex,
                            t.default_topology,
                            t.common_topology,
                            t.length
                        ));
                    Console.WriteLine("Size: posle" + MainWindow.cb_data_notallowed.Count);
                    this.Close();
                    */
                }
            }
            else
            {
                //nije uopste broj!
                MessageBox.Show("Regular expession length must be a number in range [1-14]!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                tb_length.Text = "";
            }
            
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
