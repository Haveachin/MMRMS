using MMRMS.Windows.Models;
using MMRMS.Windows.Models.Categories.Interfaces;
using MMRMS.Windows.Utilities.Errors;
using MMRMS.Windows.Utilities;
using System.Windows;

namespace MMRMS.Windows.Pages
{
    /// <summary>
    /// A default creation page for objects
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    public class CreationPage<T> : CreationGUI where T : class, IHasRegistryName, IHasTexture, IHasModel, new()
    {
        #region Properties

        /// <summary>
        /// <getter> Gets the caption of the page
        /// <setter> Sets the caption of the page
        /// </summary>
        protected string Caption
        {
            get => _tbTitle.Text;
            set => _tbTitle.Text = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="caption">The caption of the creation page</param>
        /// <param name="obj">The object that should be displayed [null -> 'new object']</param>
        public CreationPage(string caption, T obj = null)
        {
            _tbTitle.Text = caption;

            _btnLoadImage.Click += OnLoadImage;
            _btnSubmit.Click += OnSubmit;

            if (obj == null)
                return;

            _tbRegistryName.Text = obj.RegistryName;
            _tfImagePath.Text = obj.PathTexture;

            _btnSubmit.Content = "Save";
        }

        #endregion

        #region Events

        /// <summary>
        /// An event that loads an image path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnLoadImage(object sender, RoutedEventArgs e)
        {
            var path = await PathRequester.GetFromFile();

            if (string.IsNullOrEmpty(path))
            {
                await Error.Throw(ErrorTypes.InvalidPath, path);
                return;
            }

            _tfImagePath.Text = path;
        }

        /// <summary>
        /// An event that creates an object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var regitryName = _tbRegistryName.Text;

            if (string.IsNullOrEmpty(regitryName))
            {
                await Error.Throw(ErrorTypes.MissingParameter, "register name");
                return;
            }

            var pathTexture = _tfImagePath.Text;

            if (string.IsNullOrEmpty(pathTexture))
            {
                await Error.Throw(ErrorTypes.MissingParameter, "image");
                return;
            }

            Project.Create<T>(regitryName, pathTexture);

            _tbRegistryName.Text = string.Empty;
            _tfImagePath.Text = string.Empty;
        }

        #endregion
    }
}
