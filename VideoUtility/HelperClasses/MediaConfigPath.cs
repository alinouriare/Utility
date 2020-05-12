using System;

namespace Fhs.Bulletin.E_Utility.VideoUtility.HelperClasses
{
    public class MediaConfigPath
    {
        #region Variables
        public static string PathTHUMBS { get; set; }
        public static string PathFILE { get; set; }
        public static string PathFLV { get; set; }
        public static string PathFFMPEG { get; set; }
        public static string PathFFMPEGdir { get; set; }
        public static string PathFLVTOOL { get; set; }
        public static string PathInputs { get; set; }
        public static string PathOutputs { get; set; }
        public static string PathWatermarks { get; set; }
        public static string PathMP4Box { get; set; }
        public static string PathPresets { get; set; }
        #endregion

        #region Methods

        public static void InitiateMediaConfig(MediaConfigPathModel model)
        {
            try
            {
                PathTHUMBS = model.PathTHUMBS;
                PathFILE = model.PathFILE;
                PathFLV = model.PathFLV;
                PathFFMPEG = model.PathFFMPEG;
                PathFFMPEGdir = model.PathFFMPEGdir;
                PathFLVTOOL = model.PathFLVTOOL;
                PathInputs = model.PathInputs;
                PathOutputs = model.PathOutputs;
                PathWatermarks = model.PathWatermarks;
                PathMP4Box = model.PathMP4Box;
                PathPresets = model.PathPresets;
            }
            catch (Exception ex)
            {
            }
        }


        public static bool CheckBasicIsFills()
        {
            if (string.IsNullOrEmpty(PathFFMPEG) || string.IsNullOrWhiteSpace(PathFFMPEG))
                return false;
            if (string.IsNullOrEmpty(PathPresets) || string.IsNullOrWhiteSpace(PathPresets))
                return false;
            return true;
        }

        #endregion

    }
}
