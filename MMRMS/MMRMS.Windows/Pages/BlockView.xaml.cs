using MMRMS.Windows.Models.Categories;

namespace MMRMS.Windows.Pages
{
    public class BlockCreationView : CreationView<Block> { }

    public partial class BlockView : BlockCreationView
    {
        public BlockView(Block block = null)
        {
            InitializeComponent();
            InitializeView(tbRegistryName, tfImagePath, btnLoadImage, btnSubmit, block);
        }
    }
}
