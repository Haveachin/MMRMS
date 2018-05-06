using MMRMS.Windows.Models;
using MMRMS.Windows.Models.Categories.Interfaces;
using MMRMS.Windows.Utilities;
using System.Windows.Controls;

namespace MMRMS.Windows.Pages
{
    public class CreationView<T> : Page where T : class, IHasRegistryName, IHasTexture, IHasModel, new()
    {
        protected void InitializeView(TextBox tbRegistryName, TextBlock tfImagePath, Button btnImageLoad, Button btnSubmit, T obj = null)
        {
            btnImageLoad.Click += async (s, e) =>
            {
                var path = await PathRequester.GetFromFile();

                if (string.IsNullOrEmpty(path))
                    return;

                tfImagePath.Text = path;
            };

            btnSubmit.Click += (s, e) =>
            {
                var regitryName = tbRegistryName.Text;

                if (string.IsNullOrEmpty(regitryName))
                    return;

                var pathTexture = tfImagePath.Text;

                if (string.IsNullOrEmpty(pathTexture))
                    return;

                Project.Create<T>(regitryName, pathTexture);
            };

            if (obj == null)
                return;

            tbRegistryName.Text = obj.RegistryName;
            tfImagePath.Text = obj.PathTexture;

            btnSubmit.Content = "Save";
        }
    }
}
