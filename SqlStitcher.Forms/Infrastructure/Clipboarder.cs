using System.Windows.Forms;
using SqlStitcher.Forms.Contracts;

namespace SqlStitcher.Forms.Infrastructure
{
    public class Clipboarder : IClipboarder
    {
        /// <summary>
        /// Copy the given text (<paramref name="text"/>) to the clipboard.
        /// </summary>
        /// <param name="text">Text to copy.</param>
        public void CopyText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                Clipboard.Clear();
            else
                Clipboard.SetText(text);
        }
    }
}