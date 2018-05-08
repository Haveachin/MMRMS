using System.Windows.Controls;

namespace MMRMS.Windows.Utilities.Errors
{
    /// <summary>
    /// Interaction logic for ErrorView.xaml
    /// </summary>
    public partial class ErrorPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="caption">The cation of the error message</param>
        /// <param name="message">The message of the error message</param>
        public ErrorPage(string caption, string message)
        {
            InitializeComponent();

            Caption.Text = caption;
            Message.Text = message;
        }

        #endregion
    }
}
