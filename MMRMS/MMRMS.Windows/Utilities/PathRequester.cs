using MMRMS.Windows.Models.Errors;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMRMS.Windows.Utilities
{
    public static class PathRequester
    {
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
    }
}
