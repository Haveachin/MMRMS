using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows;

namespace MMRMS.Windows.Models.Errors
{
    /// <summary>
    /// A helper class that handles errors
    /// </summary>
    public static class Error
    {
        #region Utilities

        /// <summary>
        /// Display an error as a messagebox and handles consequences
        /// </summary>
        /// <param name="errorType">The type of the error</param>
        /// <param name="cause">The name of the file that is causing it</param>
        public static async Task Throw(ErrorTypes errorType, string cause)
        {
            string caption = "Error";
            string message = "An undefined Error occurred!";

            switch (errorType)
            {
                case ErrorTypes.FileNotFound:
                    caption = "404";
                    message = $"Could not find {cause}.";
                    break;
                case ErrorTypes.FileNotFoundFatal:
                    caption = "Fatal Error";
                    message = $"{cause} does not exist!";
                    break;
                case ErrorTypes.InvalidPath:
                    caption = "Invalid Path";
                    message = $"The path '{cause}' is invalid.";
                    break;
                case ErrorTypes.WrongFileFormat:
                    caption = "Wrong file format";
                    message = $"The file '{cause}' as the wrong format.";
                    break;
            }

            await DialogHost.Show(new ErrorView(caption, message), "Main");
        }

        #endregion
    }
}
