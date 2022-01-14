using System.Drawing;

namespace SqlStitcher.Forms.Contracts
{
    public interface ISettings
    {
        string LastNewProjectRoot { get; set; }
        Size FMain_Size { get; set; }
        bool FMain_IsMaximized { get; set; }
        Point FMain_Location { get; set; }
        Size FScripts_Size { get; set; }
        bool FScripts_IsMaximized { get; set; }
        Point FScripts_Location { get; set; }
        string ProjectsDirectoryPath { get; set; }
        int RecentListLength { get; set; }
        string RecentList { get; set; }

        bool InsertGoCommand { get; set; }
        PrependDescription PrependScriptDescription { get; set; }


        void Save();
    }
}