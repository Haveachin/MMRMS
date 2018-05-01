namespace MMRMS.Windows.Models.Categories.Interfaces
{
    /// <summary>
    /// If it has a model
    /// </summary>
    public interface IHasModel
    {
        /// <summary>
        /// Path to the model file
        /// </summary>
        string PathModel { get; set; }
    }
}
