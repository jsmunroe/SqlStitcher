using System.ComponentModel;

namespace SqlStitcher
{
    public class BuildOptions
    {
        /// <summary>
        /// Insert a GO command after each script.
        /// </summary>
        public bool InsertGoCommand { get; set; } = true;

        /// <summary>
        /// Prepend a description at the top of each script.
        /// </summary>
        public PrependDescription PrependScriptDescription { get; set; }
    }

    public enum PrependDescription
    {
        None = 0,

        [Description("Script File Name")]
        FileName,

        [Description("Full Script File Path")]
        FileFullPath,
    }
}