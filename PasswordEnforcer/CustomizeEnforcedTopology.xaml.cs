using PasswordEnforcer.model;
using System;
using System.Windows;

namespace PasswordEnforcer
{
    /// <summary>
    /// Interaction logic for CustomizeEnforcedTopology.xaml
    /// </summary>
    public partial class CustomizeEnforcedTopology : Window
    {

        public CustomizeEnforcedTopology()
        {
            InitializeComponent();
            
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            Topology t = new Topology(tb_name.Text,tb_regex.Text,false,false,0);
            //Console.WriteLine("Created: " + MainWindow.new_top_enf.toString());
            MainWindow.cb_data_enf.Add(
                new Topology(
                    t.name,
                    t.regex,
                    t.default_topology,
                    t.common_topology,
                    t.length
                ));
            this.Close();
        }
    }
}
