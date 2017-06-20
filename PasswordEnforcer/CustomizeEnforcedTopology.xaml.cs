using PasswordEnforcer.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PasswordEnforcer
{
    /// <summary>
    /// Interaction logic for CustomizeEnforcedTopology.xaml
    /// </summary>
    public partial class CustomizeEnforcedTopology : Window
    {

        public ObservableCollection<Topology> list_data { get; set; }

        public CustomizeEnforcedTopology(ObservableCollection<Topology> cb_data)
        {
            InitializeComponent();
            list_data = cb_data;
            
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {

            Topology t = new Topology(tb_name.Text,tb_regex.Text,false,false,0);
            Console.WriteLine("Size: pre " + list_data.Count);
            list_data.Add(
                new Topology(
                    t.name,
                    t.regex,
                    t.default_topology,
                    t.common_topology,
                    t.length
                ));
            Console.WriteLine("Size: pre " + list_data.Count);
            //Console.WriteLine("Created: " + MainWindow.new_top_enf.toString());
            /*
            MainWindow.cb_data_enf.Add(
                new Topology(
                    t.name,
                    t.regex,
                    t.default_topology,
                    t.common_topology,
                    t.length
                ));
                */




            this.Close();
        }
    }
}
