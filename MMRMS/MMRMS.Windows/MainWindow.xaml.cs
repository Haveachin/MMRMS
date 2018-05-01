using System.Windows;
using MMRMS.Windows.Models;
using MMRMS.Windows.Pages;
using MMRMS.Windows.Utilities;

namespace MMRMS.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// The default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.ContentPage.Content = new WelcomeView();
        }

        #endregion

        #region Events

        /// <summary>
        /// Starts loading the project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnLoadProject(object sender, RoutedEventArgs e)
        {
            var path = await PathRequester.GetFromFolder();

            FolderView.Items.Clear();
                
            Project.LoadProject(path);

            if (!Project.IsOK)
                return;

            foreach (var categorie in Project.GetCategories())
            {
                FolderView.Items.Add(categorie);
            }

            CreateBlock.IsEnabled = true;
            CreateItem.IsEnabled = true;
        }

        /// <summary>
        /// Opens a new block creation screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreateBlock(object sender, RoutedEventArgs e)
            => this.ContentPage.Content = new BlockView();

        /// <summary>
        /// Opens a new item creation screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCreateItem(object sender, RoutedEventArgs e)
            => this.ContentPage.Content = new ItemView();

        #endregion
    }
}
