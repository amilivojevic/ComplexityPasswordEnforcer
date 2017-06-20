using Microsoft.Win32;
using Newtonsoft.Json;
using PasswordEnforcer.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static ObservableCollection<Topology> loadJson(String path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                ObservableCollection<Topology> items = JsonConvert.DeserializeObject<ObservableCollection<Topology>>(json);
                return items;
            }
        }

        public static Topology loadJsonTopology(String path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                Topology item = JsonConvert.DeserializeObject<Topology>(json);
                return item;
            }
        }

        public static String previewJson(ObservableCollection<Topology> list)
        {
            return JsonConvert.SerializeObject(list.ToArray(), Formatting.Indented);
        }

        public static String previewJson(Topology top)
        {
            return JsonConvert.SerializeObject(top, Formatting.Indented);
        }


        public static void writeJsonFile(ObservableCollection<Topology> list, string path)
        {
            string json = JsonConvert.SerializeObject(list.ToArray());

            //upis u json fajl
            System.IO.File.WriteAllText(path, json);
        }

        public static bool makeListOfTopologies(ObservableCollection<Topology> topologyItems)
        {

            ObservableCollection<Topology> items = loadJson(path);
            if(items != null) { 
                foreach (Topology t in items)
                {
                    topologyItems.Add(t);
                }
                return true;
            }
            else
            {
                return false;
            }
        }



        public static bool makeListOfTopologies(ObservableCollection<Topology> topologyItems, String file_path)
        {

            ObservableCollection<Topology> items = loadJson(file_path);
            if (items != null)
            {
                foreach (Topology t in items)
                {
                    topologyItems.Add(t);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ObservableCollection<String> makeListOfTopologyNames(ObservableCollection<String> topologyNames)
        {
            ObservableCollection<Topology> items = loadJson(path);
            //List<String> topologyNames = new List<String>();
            
            foreach(Topology t in items)
            {
                topologyNames.Add(t.name);
            }
            return topologyNames;
        }

        public static void writeRegistryKey(String keyValue)
        {
           
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);

            //probaj da otvoris DevX
            key = key.OpenSubKey(REGKEY1, true);
            //ako ne postoji
            if(key == null)
            {
                key.CreateSubKey(REGKEY1);
                key = key.OpenSubKey(REGKEY1, true);
            }

            //probaj da otvoris PasswordFilter
            key = key.OpenSubKey(REGKEY2, true);
            if(key == null)
            {
                key.CreateSubKey(REGKEY2);
                key = key.OpenSubKey(REGKEY2, true);
            }

            //postavi vrednost u RegEx
            key.SetValue(REGKEY3, keyValue);
            key.Close();

        }

        public static String readRegistryKey()
        {
           
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
            key = key.OpenSubKey("DevX");
            key = key.OpenSubKey("PasswordFilter");
            String data = key.GetValue("RegEx").ToString();

            Console.WriteLine("Registar: " + data);
            key.Close();

            return data;

        }

        public static String makeRegExNotAllowed(String notAllowedTopology, int length)
        {
            //Uzima u obzir samo not allowed!!!
            return "^((?!" + notAllowedTopology + ").){" + length + "}$";
        }

        public static String makeRegExEnforced(String enforcedTopology)
        {
            //Uzima u obzir samo enforced!!!
            return "^(" + enforcedTopology + ")$";
        }

        public static String fixPath(String old_path)
        {
            return old_path.Replace("\\","\\\\");
        }
    }
}
