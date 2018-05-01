using MMRMS.Windows.Models.Categories.Interfaces;
using System.Collections.Generic;

namespace MMRMS.Windows.Models.Categories
{
    /// <summary>
    /// An item of a the Item category
    /// </summary>
    public class Item : IHasRegistryName, IHasModel, IHasTexture
    {
        /// <summary>
        /// The name in the game registry
        /// </summary>
        public string RegistryName { get; set; }
        /// <summary>
        /// Path to the model file of the item
        /// </summary>
        public string PathModel { get; set; }

        /// <summary>
        /// Path to the texture of the item
        /// </summary>
        public string PathTexture { get; set; }

        /// <summary>
        /// Names in the lang file of the item
        /// </summary>
        public Dictionary<string, string> LangName { get; set; }
    }
}
