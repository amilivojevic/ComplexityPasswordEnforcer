﻿using Microsoft.Win32;
using PasswordEnforcer.model;
using PasswordEnforcer.viewModel;
using System;
using System.Collections.Generic;
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

        public static List<Topology> cb_data_enf { get; set; }
        public static List<Topology> cb_data_notallowed { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            cb_data_notallowed = new List<Topology>();
            cb_data_notallowed.Add(new Topology("None", "none", false, false, 0));
            if (util.Util.makeListOfTopologies(cb_data_notallowed, util.Util.path) == false)
            {
                MessageBox.Show("Impossible to import default topologies!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            cb_data_enf = new List<Topology>();
            cb_data_enf.Add(new Topology("None", "none", false, false, 0));
            if (util.Util.makeListOfTopologies(cb_data_enf, util.Util.path) == false)
            {
                MessageBox.Show("Impossible to import default topologies!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void importDataForCombos(String file_path)
        {
            cb_data_notallowed = new List<Topology>();
            cb_data_notallowed.Add(new Topology("None", "none", false, false, 0));
            if (util.Util.makeListOfTopologies(cb_data_notallowed, file_path) == false)
            {
                MessageBox.Show("Impossible to import default topologies!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            cb_data_enf = new List<Topology>();
            cb_data_enf.Add(new Topology("None", "none", false, false, 0));
            if (util.Util.makeListOfTopologies(cb_data_enf, file_path) == false)
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
            Topology t = (Topology)cb_enforced.SelectedItem;
            util.Util.writeRegistryKey(t.regex);
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

        private void btn_cust_enf_Click(object sender, RoutedEventArgs e)
        {

            CustomizeEnforcedTopology customEnfWindow = new CustomizeEnforcedTopology();
            customEnfWindow.ShowDialog();

        }

        private void btn_cust_notallow_Click(object sender, RoutedEventArgs e)
        {
            CustomizeNotAllowedTopology customNotallowedWindow = new CustomizeNotAllowedTopology();
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
            }
        }
    }
}
