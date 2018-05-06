using MMRMS.Windows.Models.Categories.Interfaces;
using MMRMS.Windows.Models.Errors;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MMRMS.Windows.Models.Categories
{
    /// <summary>
    /// A helper class to get the registry names from an item or a block
    /// </summary>
    public static class RegistryHelper
    {
        #region Register Methods

        /// <summary>
        /// Registers the block state to the corresponding block in the list if it doesn't exist create one with that block state
        /// </summary>
        /// <param name="blocks">A list of block classes</param>
        /// <param name="blockStateFile">The path to the block state file</param>
        public static async void RegisterBlockState(List<Block> blocks, string blockStateFile)
        {
            var registryName = await GetRegistryName(blockStateFile);
            var blcks = blocks.Where(i => i.RegistryName == registryName).ToList();

            if (blcks.Count > 0)
                blcks[0].PathModel = blockStateFile;
            else
                blocks.Add(new Block() { RegistryName = registryName, PathModel = blockStateFile });
        }

        /// <summary>
        /// Registers the model to the corresponding item in the list if it doesn't exist create one with that model
        /// </summary>
        /// <typeparam name="T">A data class that has a model</typeparam>
        /// <param name="list">A list of data classes with a model</param>
        /// <param name="modelFile">The path to the model file</param>
        public static async void RegisterModel<T>(List<T> list, string modelFile) where T : IHasRegistryName, IHasModel, new()
        {
            var registryName = await GetRegistryName(modelFile);
            var items = list.Where(i => i.RegistryName == registryName).ToList();
            
            if (items.Count > 0)
                items[0].PathModel = modelFile;
            else
                list.Add(new T() { RegistryName = registryName, PathModel = modelFile });
        }

        /// <summary>
        /// Registers the texture to the corresponding item in the list if it doesn't exist create one with that texture
        /// </summary>
        /// <typeparam name="T">A data class that has a texture</typeparam>
        /// <param name="list"></param>
        /// <param name="textureFile">The path to the texture file</param>
        public static void RegisterTexture<T>(List<T> list, string textureFile) where T : IHasRegistryName, IHasTexture, new()
        {
            var dirs = textureFile.Split('\\');
            var registryName = dirs[dirs.Length - 1].Split('.')[0];
            var items = list.Where(i => i.RegistryName == registryName).ToList();

            if (items.Count > 0)
                items[0].PathTexture = textureFile;
            else
                list.Add(new T() { RegistryName = registryName, PathTexture = textureFile });
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets the registry name of an item or block
        /// </summary>
        /// <param name="file">The path to a file that contains the registry name</param>
        /// <returns>The registry name</returns>
        public static async Task<string> GetRegistryName(string file)
        {
            file = NormalizePath(file);

            if (!File.Exists(file))
            {
                await Error.Throw(ErrorTypes.InvalidPath, file);
                return string.Empty;
            }

            string registryName = "";
            var dirs = file.Split('\\');

            switch (dirs[dirs.Length - 2])
            {
                case "blockstates": registryName = ((string)JObject.Parse(File.ReadAllText(file))["variants"]["normal"]["model"]).Split(':')[1]; break;
                case "block": registryName = ((string)JObject.Parse(File.ReadAllText(file))["textures"]["all"]).Split('/')[1]; break;
                case "item":
                    try
                    {
                        registryName = ((string)JObject.Parse(File.ReadAllText(file))["textures"]["layer0"]).Split('/')[1];
                    }
                    catch
                    {
                        registryName = ((string)JObject.Parse(File.ReadAllText(file))["parent"]).Split('/')[1];
                    }
                    break;
            }

            return registryName;
        }

        /// <summary>
        /// Normalizes a path
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The normalized path</returns>
        public static string NormalizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            return path.Replace('/', '\\'); ;
        }

        #endregion
    }
}
