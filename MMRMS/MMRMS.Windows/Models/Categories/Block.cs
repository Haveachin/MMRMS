using MMRMS.Windows.Models.Categories.Interfaces;
using System.Collections.Generic;

namespace MMRMS.Windows.Models.Categories
{
    /// <summary>
    /// An item of a the Block category
    /// </summary>
    public class Block : IHasRegistryName, IHasModel, IHasTexture
    {
        /// <summary>
        /// The name in the game registry
        /// </summary>
        public string RegistryName { get; set; }
        /// <summary>
        /// Path to the blockstate file of the block
        /// </summary>
        public string PathBlockState { get; set; }

        /// <summary>
        /// Path to the model file of the block
        /// </summary>
        public string PathModel { get; set; }

        /// <summary>
        /// Path to the texture of the block
        /// </summary>
        public string PathTexture { get; set; }

        /// <summary>
        /// Names in the lang file of the block
        /// </summary>
        public Dictionary<string, string> LangName { get; set; }
    }
}
