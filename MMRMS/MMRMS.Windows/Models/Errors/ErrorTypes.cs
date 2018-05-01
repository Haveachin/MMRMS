namespace MMRMS.Windows.Models.Errors
{
    /// <summary>
    /// All error types that can happen
    /// </summary>
    public enum ErrorTypes
    {
        /// <summary>
        /// A file was not found
        /// </summary>
        FileNotFound,
        /// <summary>
        /// A file that is critical is missing
        /// </summary>
        FileNotFoundFatal,
        /// <summary>
        /// A path that is unvalid
        /// </summary>
        InvalidPath,
        /// <summary>
        /// A file has the wrong format
        /// </summary>
        WrongFileFormat
    }
}
