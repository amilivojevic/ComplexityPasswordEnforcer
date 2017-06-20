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
using System.Windows.Shapes;

namespace PasswordEnforcer
{
    /// <summary>
    /// Interaction logic for JSONPreview.xaml
    /// </summary>
    public partial class JSONPreview : Window
    {
        public JSONPreview(String json)
        {
            InitializeComponent();
            tb_json.Text = json;
        }
    }
}
