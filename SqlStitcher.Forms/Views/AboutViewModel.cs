using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Forms.Views
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            var version = GetType().Assembly.GetName().Version;
            Version = version.ToString();
        }

        public string Version { get; set; }
    }
}
