using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMRMS.Windows.Models;
using MMRMS.Windows.Models.Categories.Interfaces;
using MMRMS.Windows.Utilities;
using System.Windows.Controls;

namespace MMRMS.Windows.Pages
{
    /// <summary>
    /// Interaction logic for CreationView.xaml
    /// </summary>
    public abstract partial class CreationGUI : Page
    {
        public CreationGUI()
        {
            InitializeComponent();
        }
    }
}
