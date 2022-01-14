using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Contracts;
using Helpers.IO;
using SqlStitcher.Models;
using SqlStitcher.Test.Forms;

namespace SqlStitcher.Test
{
    public static class TestState
    {
        public static readonly PathBuilder Root = PathBuilder.Create(AppDomain.CurrentDomain.BaseDirectory + @"\Files");

        public static MockContainerFactory MockContainerFactory;

        public static void SelectEntry(Project project, string relativePath)
        {
            GetEntry(project, relativePath).IsSelected = true;
        }

        public static ScriptEntry GetEntry(Project project, string relativePath)
        {
            return project.Entries.First(p => p.File.Path.ToString().EndsWith(relativePath, StringComparison.OrdinalIgnoreCase));
        }
    }
}
