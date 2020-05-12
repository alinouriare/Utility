namespace Fhs.Bulletin.E_Utility.CrawlUtility.Models
{
    public class ImageLink
    {

        #region Variables

        public string Link { get; set; }
        public string AddressInSite { get; set; }
        public string FileName { get; set; }
        public long FileSizeBytes { get; set; }
        public long FileSizeKB { get; set; }
        public string FileType { get; set; }
        public string LastModified { get; set; }
        public string Date { get; set; }
        #endregion

        #region Constructors
        public ImageLink()
        {
            MakeDefaults();
        }

        #endregion

        #region Methods
        #endregion

        #region Helper Methods
        private void MakeDefaults()
        {
            Link = "";
            AddressInSite = "";
            FileName = "";
            FileSizeBytes = 0;
            FileSizeKB = 0;
            FileType = "";
            LastModified = "";
            Date = "";
        }
        #endregion

    }
}
