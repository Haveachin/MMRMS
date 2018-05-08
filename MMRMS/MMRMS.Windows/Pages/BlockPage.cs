using MMRMS.Windows.Models.Categories;

namespace MMRMS.Windows.Pages
{
    public class BlockPage : CreationPage<Block>
    {
        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="block">The block object (optional)</param>
        public BlockPage(Block block = null) : base("New Block", block)
        {
            if (block != null)
                Caption = "Edit Block";
        }

        #endregion
    }
}
