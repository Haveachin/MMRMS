using MMRMS.Windows.Utilities.Errors;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMRMS.Windows.Utilities
{
    /// <summary>
    /// A static class that simplifies the user interface
    /// </summary>
    public static class PathRequester
    {
        #region Utilities

        /// <summary>
        /// Opens a folder dialog
        /// </summary>
        /// <returns>The path to the folder</returns>
        public static async Task<string> GetFromFile()
        {
            string path = null;

            using (var ofd = new OpenFileDialog())
            {
                DialogResult result = ofd.ShowDialog();

                if (result == DialogResult.Abort || result == DialogResult.Cancel)
                    return string.Empty;

                path = ofd.FileName;

                if (result != DialogResult.OK || string.IsNullOrWhiteSpace(path))
                {
                    await Error.Throw(ErrorTypes.InvalidPath, path);
                    return string.Empty; ;
                }
            }

            return path;
        }

        /// <summary>
        /// Opens a file dialog
        /// </summary>
        /// <returns>The path to the file</returns>
        public static async Task<string> GetFromFolder()
        {
            string path = null;

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.Abort || result == DialogResult.Cancel)
                    return string.Empty;
                
                path = fbd.SelectedPath;

                if (result != DialogResult.OK || string.IsNullOrWhiteSpace(path))
                {
                    await Error.Throw(ErrorTypes.InvalidPath, path);
                    return string.Empty;
                }
            }

            return path;
        }

        #endregion
    }
}
