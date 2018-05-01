using MMRMS.Windows.Models.Categories;
using MMRMS.Windows.Models.Categories.Interfaces;
using MMRMS.Windows.Models.Errors;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MMRMS.Windows.Models
{
    public static class Project
    {
        #region Properties

        public static string RootPath { get; private set; }
        public static string AssetPath { get; private set; }
        public static bool IsOK { get; private set; } = false;
        public static McModInfo McModInfo { get; private set; }

        #endregion

        #region Attributes

        private static ObservableCollection<Block> _blocks = new ObservableCollection<Block>();
        private static ObservableCollection<Item> _items = new ObservableCollection<Item>();
        private static ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();
        private static ObservableCollection<LootTable> _lootTables = new ObservableCollection<LootTable>();
        private static ObservableCollection<Lang> _langs = new ObservableCollection<Lang>();

        #endregion

        #region Utilities

        #region Categories

        /// <summary>
        /// Creates all the Categories for the treeview
        /// </summary>
        /// <returns>The categories</returns>
        public static ObservableCollection<TreeViewItem> GetCategories()
        {
            RemoveBlocksFromItems();

            return new ObservableCollection<TreeViewItem>()
            {
                AddCategorie(_blocks, "Blocks", "block"),
                AddCategorie(_items, "Items", "item")
            };
        }

        /// <summary>
        /// Adds all data classes to it's corresponding categorie
        /// </summary>
        /// <typeparam name="T">The Type of data class</typeparam>
        /// <param name="list">A list with all data classes of a type</param>
        /// <param name="name">The display name of the categorie</param>
        /// <param name="tag">The icon of the </param>
        /// <returns></returns>
        private static TreeViewItem AddCategorie<T>(ObservableCollection<T> list, string name, string tag) where T : IHasRegistryName
        {
            var categorie = new TreeViewItem() { Header = name, Tag = tag };

            foreach (var item in list)
            {
                categorie.Items.Add(new TreeViewItem() { Header = item.RegistryName, Tag = item });
            }

            return categorie;
        }

        /// <summary>
        /// Removes all blocks from the list of items
        /// </summary>
        private static void RemoveBlocksFromItems()
        {
            var blocksInItems = new List<Item>();

            foreach (var block in _blocks)
            {
                foreach (var item in _items)
                {
                    if (block.RegistryName == item.RegistryName)
                        blocksInItems.Add(item);
                }
            }

            blocksInItems.ForEach(i => _items.Remove(i));
        }

        #endregion

        #region Loading

        /// <summary>
        /// Creates all folders needed
        /// </summary>
        /// <returns>The folder structure</returns>
        public static async Task LoadProject(string rootPath)
        {
            RootPath = rootPath;
            McModInfo = await LoadFile<McModInfo>("mcmod.info");

            if (McModInfo == null)
            {
                await Error.Throw(ErrorTypes.FileNotFoundFatal, "mcmod.info");
                return;
            }

            AssetPath = $"{RootPath}\\assets\\{McModInfo.Modid}";

            var dirs = JArray.Parse(File.ReadAllText("Data/FolderStructure.json")).ToObject<List<string>>();

            foreach (var dir in dirs)
            {
                var fullDir = $"{AssetPath}\\{dir}";

                if (!File.Exists(dir))
                    Directory.CreateDirectory(fullDir);

                LoadExistingData(fullDir);
            }

            IsOK = true;
        }

        /// <summary>
        /// Loads all files that exist from the directories
        /// </summary>
        /// <param name="dirs">The directories</param>
        private static void LoadExistingData(string dir)
        {
            dir = RegistryHelper.NormalizePath(dir);
            var files = Directory.GetFiles(dir);
            var dirs = dir.Split('\\');

            foreach (var file in files)
            {
                if (file.EndsWith(".json"))
                {
                    switch (dirs[dirs.Length - 1])
                    {
                        case "blockstates": RegistryHelper.RegisterBlockState(_blocks, file); break;
                        case "block": RegistryHelper.RegisterModel(_blocks, file); break;
                        case "item": RegistryHelper.RegisterModel(_items, file); break;
                    }
                }
                else if (file.EndsWith(".png"))
                {
                    switch (dirs[dirs.Length - 1])
                    {
                        case "blocks": RegistryHelper.RegisterTexture(_blocks, file);  break;
                        case "items": RegistryHelper.RegisterTexture(_items, file);  break;
                    }
                }
            }
        }

        /// <summary>
        /// Loads a given JSON file and returns an object with that data
        /// </summary>
        /// <typeparam name="T">The object</typeparam>
        /// <param name="path">Path to the JSON file</param>
        /// <returns>The object with data</returns>
        private static async Task<T> LoadFile<T>(string path) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            var normalizedPath = RegistryHelper.NormalizePath(path);

            if (!normalizedPath.StartsWith("\\"))
                normalizedPath = $"\\{normalizedPath}";

            var absolutePath = $"{RootPath}{normalizedPath}";

            if (!File.Exists(absolutePath))
            {
                await Error.Throw(ErrorTypes.FileNotFound, normalizedPath);
                return null;
            }

            string json = File.ReadAllText(absolutePath);

            T obj = null;

            try
            {
                obj = JArray.Parse(json).ToObject<T>();
            }
            catch
            {
                try
                {
                    obj = JArray.Parse(json)[0].ToObject<T>();
                }
                catch (System.Exception)
                {
                    await Error.Throw(ErrorTypes.InvalidPath, normalizedPath);
                }
            }

            return obj;
        }

        #endregion

        #region Creating

        /// <summary>
        /// Creates a new object of type T
        /// </summary>
        /// <typeparam name="T">A data class like block or item</typeparam>
        /// <param name="registryName">The registry name of the data class</param>
        /// <param name="pathTexture">The texture path of the data class</param>
        public static void Create<T>(string registryName, string pathTexture) where T : class, IHasRegistryName, IHasModel, IHasTexture, new()
        {
            JObject jsonTmp = null;

            #region Block

            if (typeof(T).IsAssignableFrom(typeof(Block)))
            {
                #region Blockstate
                jsonTmp = JObject.Parse(File.ReadAllText("Data/blockstate.json"));
                jsonTmp["variants"]["normal"]["model"] = $"{McModInfo.Modid}:{registryName}";

                var path = $"{AssetPath}\\blockstates\\{registryName}.json";

                File.WriteAllText(path, jsonTmp.ToString(Newtonsoft.Json.Formatting.None));
                RegistryHelper.RegisterBlockState(_blocks, path);
                #endregion

                #region Model Block
                jsonTmp = JObject.Parse(File.ReadAllText("Data/block.json"));
                jsonTmp["textures"]["all"] = $"{McModInfo.Modid}:blocks/{registryName}";

                path = $"{AssetPath}\\models\\block\\{registryName}.json";

                File.WriteAllText(path, jsonTmp.ToString(Newtonsoft.Json.Formatting.None));
                RegistryHelper.RegisterModel(_blocks, path);
                #endregion

                #region Model Item
                jsonTmp = JObject.Parse(File.ReadAllText("Data/block_item.json"));
                jsonTmp["parent"] = $"{McModInfo.Modid}:blocks/{registryName}";

                File.WriteAllText($"{AssetPath}\\models\\item\\{registryName}.json", jsonTmp.ToString(Newtonsoft.Json.Formatting.None));
                #endregion

                #region Texture
                path = $"{AssetPath}\\textures\\blocks\\{registryName}.png";

                File.Copy(pathTexture, path);
                RegistryHelper.RegisterTexture(_blocks, path);
                #endregion
            }

            #endregion

            #region Item

            else if (typeof(T).IsAssignableFrom(typeof(Item)))
            {
                #region Model Item
                jsonTmp = JObject.Parse(File.ReadAllText("Data/item.json"));
                jsonTmp["textures"]["layer0"] = $"{McModInfo.Modid}:items/{registryName}";

                var path = $"{AssetPath}\\models\\item\\{registryName}.json";

                File.WriteAllText(path, jsonTmp.ToString(Newtonsoft.Json.Formatting.None));
                RegistryHelper.RegisterModel(_items, path);
                #endregion

                #region Texture

                path = $"{AssetPath}\\textures\\items\\{registryName}.png";

                File.Copy(pathTexture, path);
                RegistryHelper.RegisterTexture(_items, path);

                #endregion
            }

            #endregion
        }

        #endregion

        #endregion
    }
}
