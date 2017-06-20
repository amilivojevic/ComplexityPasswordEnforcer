using Microsoft.Win32;
using PasswordEnforcer.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace PasswordEnforcer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Topology> cb_data { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            cb_data = new ObservableCollection<Topology>();
            cb_data.Add(new Topology("", "none", false, false, 0));
            if (util.Util.makeListOfTopologies(cb_data, util.Util.path) == false)
            {
                MessageBox.Show("Impossible to import default topologies!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        public void importDataForCombos(String file_path)
        {
            
            //cb_data.Clear();
            if (util.Util.makeListOfTopologies(cb_data, file_path) == false)
            {
                MessageBox.Show("Impossible to import default topologies!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

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
            util.Util.readRegistryKey();
        }

        private void btn_apply_Click(object sender, RoutedEventArgs e)
        {
            Topology t;

            if (cb_notallowed.IsEnabled)
            {
                t = (Topology)cb_notallowed.SelectedItem;
            }
            else if (cb_enforced.IsEnabled)
            {
                t = (Topology)cb_notallowed.SelectedItem;
            }
            else
            {
                MessageBox.Show("Select some topology!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            util.Util.makeRegExNotAllowed(t.regex, t.length);
            util.Util.writeRegistryKey(t.regex);
            return;
        }

        private void cb_notallowed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Topology selected = (Topology)cb_notallowed.SelectedItem;
            if (selected.name == "")
            {
                cb_notallowed.IsEnabled = true;
            }
            else
            {
                cb_notallowed.IsEnabled = false;
            }

        }

        private void cb_enforced_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Topology selected = (Topology)cb_enforced.SelectedItem;
            if (selected.name == "")
            {
                cb_notallowed.IsEnabled = true;
            }
            else
            {
                cb_notallowed.IsEnabled = false;
            }
        }

        private void btn_cust_enf_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("MAIN: Size: pre " + cb_data.Count);
            CustomizeEnforcedTopology customEnfWindow = new CustomizeEnforcedTopology(cb_data);
            customEnfWindow.ShowDialog();

        }

        private void btn_cust_notallow_Click(object sender, RoutedEventArgs e)
        {
            CustomizeNotAllowedTopology customNotallowedWindow = new CustomizeNotAllowedTopology(cb_data);
            customNotallowedWindow.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON file (*.json) | *.json";
            if (openFileDialog.ShowDialog() == true) {
                String path = util.Util.fixPath(openFileDialog.FileName);
                Console.WriteLine("selektovan fajl (nadam se da je putanja??): \n" + 
                    util.Util.fixPath(path));
                cb_data.Clear();
                importDataForCombos(path);

                foreach (Topology t in cb_data)
                {
                    Console.WriteLine("*  " + t.toString());
                }
            }
        }

        private void mi_export_Click(object sender, RoutedEventArgs e)
        {

            String json = util.Util.previewJson(cb_data);

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "JSON file (*.json) | *.json"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, json);
            }

        }

        private void mi_preview_Click(object sender, RoutedEventArgs e)
        {
            String json = util.Util.previewJson(cb_data);
            JSONPreview jsonPreviewDialog = new JSONPreview(json);
            jsonPreviewDialog.ShowDialog();
        }

        private void mi_readreg_Click(object sender, RoutedEventArgs e)
        {

            String message = "Register: HKEY_LOCAL_MACHINE\\SOFTWARE\\DevX\\PasswordFilter \n "
                + "Key: RegEx\n" +
                "Value: " + util.Util.readRegistryKey();
            MessageBox.Show(message, "Registar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void mi_top_export_Click(object sender, RoutedEventArgs e)
        {

            Topology t;

            if(cb_notallowed.IsEnabled && cb_enforced.IsEnabled)
            {
                MessageBox.Show("Select some topology!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (cb_notallowed.IsEnabled)
            {
                t = (Topology)cb_notallowed.SelectedItem;
            }
            else if (cb_enforced.IsEnabled)
            {
                t = (Topology)cb_enforced.SelectedItem;
            }
            else
            {
                MessageBox.Show("Something is wrong!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            String json = util.Util.previewJson(t);

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "JSON file (*.json) | *.json"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, json);
            }
        }

        private void mi_top_import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON file (*.json) | *.json";
            if (openFileDialog.ShowDialog() == true)
            {
                String path = util.Util.fixPath(openFileDialog.FileName);
                Topology t_imported = util.Util.loadJsonTopology(path);
                cb_data.Add(t_imported);

                //frekventna topologija, treba da bude zabranjena
                if (t_imported.common_topology)
                {
                    cb_notallowed.SelectedItem = t_imported;
                }
                else
                {
                    cb_enforced.SelectedItem = t_imported;
                }
                
            }
        }
    }
}
