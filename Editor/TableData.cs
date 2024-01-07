using System;

namespace AB_GoogleSheetImporter.Editor
{
    [Serializable]
    public class TableData
    {
        public bool Selected;
        public string DownloadUrlPath;
        public string LocalPath;
        public string Name;
        public FileFormat FileFormat;
    }
}