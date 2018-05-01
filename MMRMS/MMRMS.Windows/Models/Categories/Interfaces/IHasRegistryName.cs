namespace MMRMS.Windows.Models.Categories.Interfaces
{
    /// <summary>
    /// If it has a registry name
    /// </summary>
    public interface IHasRegistryName
    {
        /// <summary>
        /// The name in the game registry name
        /// </summary>
        string RegistryName { get; set; }
    }
}
