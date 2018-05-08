using MMRMS.Windows.Models.Categories;

namespace MMRMS.Windows.Pages
{
    public class ItemPage : CreationPage<Item>
    {
        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="item">The item object (optional)</param>
        public ItemPage(Item item = null) : base("New Item", item)
        {
            if (item != null)
                Caption = "Edit Item";
        }

        #endregion
    }
}
