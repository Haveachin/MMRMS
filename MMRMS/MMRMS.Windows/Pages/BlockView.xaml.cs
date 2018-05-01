using MMRMS.Windows.Models;
using MMRMS.Windows.Models.Categories;
using MMRMS.Windows.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace MMRMS.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class BlockView : Page
    {
        public BlockView(Block block = null)
        {
            InitializeComponent();

            if (block == null)
                return;

            tbRegistryName.Text = block.RegistryName;
            tfImagePath.Text = block.PathTexture;

            btnSubmit.Content = "Save";
        }

        private async void OnLoadImage(object sender, RoutedEventArgs e)
        {
            var path = await PathRequester.GetFromFile();

            if (string.IsNullOrEmpty(path))
                return;

            tfImagePath.Text = path;
        }

        private void OnSubmit(object sender, RoutedEventArgs e)
        {
            var regitryName = tbRegistryName.Text;

            if (string.IsNullOrEmpty(regitryName))
                return; // Error??!?!

            var pathTexture = tfImagePath.Text;

            if (string.IsNullOrEmpty(pathTexture))
                return;

            Project.Create<Block>(regitryName, pathTexture);
        }
    }
}
