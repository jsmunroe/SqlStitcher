using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlStitcher.Forms.Contracts
{
    public interface IClipboarder
    {
        void CopyText(string text);
    }
}
