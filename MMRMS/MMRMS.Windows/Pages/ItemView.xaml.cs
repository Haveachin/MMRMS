using MMRMS.Windows.Models.Categories;

namespace MMRMS.Windows.Pages
{
    public class ItemCreationView : CreationView<Item> { }

    public partial class ItemView : ItemCreationView
    {
        public ItemView(Item item = null)
        {
            InitializeComponent();
            InitializeView(tbRegistryName, tfImagePath, btnLoadImage, btnSubmit, item);
        }
    }
}
