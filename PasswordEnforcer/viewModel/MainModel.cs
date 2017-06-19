using PasswordEnforcer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordEnforcer.viewModel
{
    class MainModel
    {
        public List<Topology> cb_data { get; set; }

        public MainModel()
        {
            cb_data = new List<Topology>();
            cb_data.Add(new Topology("None", "none", false, false, 0));
            util.Util.makeListOfTopologies(cb_data);
        }

        public void addTop(Topology newTop)
        {
            cb_data.Add(new Topology(newTop.name, newTop.regex, newTop.default_topology, newTop.common_topology, newTop.length));
            
        }
    }
}
