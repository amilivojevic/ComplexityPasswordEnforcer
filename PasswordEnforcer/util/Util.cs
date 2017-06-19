﻿using Microsoft.Win32;
using Newtonsoft.Json;
using PasswordEnforcer.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordEnforcer.util
{
    class Util
    {
        public static String path = "C:\\Users\\Korisnik\\Desktop\\F\\4.godina\\8.semestar\\Bezbednost u sistemima elektronskog poslovanja\\PROJEKAT\\password_topologies.json";
        public static String REGKEY1 = "DevX";
        public static String REGKEY2 = "PasswordFilter";
        public static String REGKEY3 = "RegEx";

        private static List<Topology> loadJson(String path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                //Console.Write("Procitano iz fajla " + path + "\n " + json);
                List<Topology> items = JsonConvert.DeserializeObject<List<Topology>>(json);
                return items;
            }
        }

        public static void makeListOfTopologies(List<Topology> topologyItems)
        {
            List<Topology> items = loadJson(path);

            foreach (Topology t in items)
            {
                topologyItems.Add(t);
            }
        }

        public static List<String> makeListOfTopologyNames(List<String> topologyNames)
        {
            List<Topology> items = loadJson(path);
            //List<String> topologyNames = new List<String>();
            
            foreach(Topology t in items)
            {
                topologyNames.Add(t.name);
            }
            return topologyNames;
        }

        public static void writeRegistryKey()
        {
           
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);

            key.CreateSubKey(REGKEY1);
            key = key.OpenSubKey(REGKEY1, true);

            key.CreateSubKey(REGKEY2);
            key = key.OpenSubKey(REGKEY2, true);

            key.SetValue(REGKEY3, "sandra kraljica");
            key.Close();

        }

        public static void readRegistryKey()
        {
           
            Console.WriteLine("Pocetak");
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
            key = key.OpenSubKey("DevX");
            key = key.OpenSubKey("PasswordFilter");
            String data = key.GetValue("RegEx").ToString();
            Console.WriteLine("Procitano: " + data);
            key.Close();

        }

        public static String makeRegEx(String enfTopology, String notAllowedTopology)
        {
            //Uzima u obzir samo not allowed!!!
            return "^((?!" + notAllowedTopology + ").){2}$";
        }
    }
}
