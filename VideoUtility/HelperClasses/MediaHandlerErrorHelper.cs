namespace Fhs.Bulletin.E_Utility.VideoUtility.HelperClasses
{
    public class MediaHandlerErrorHelper
    {
        public static string GetErrorMessage(int code)
        {
            switch (code)
            {
                case 100: return "ffmpeg.exe path is incorrect or not found, please make sure that you download and put complete folder of ffmpeg on root of your web application.";
                case 101: return "Source video path is incorrect or not found.";
                case 102: return "Destination directory path is incorrect or not found.";
                case 104: return "Unknown options or video->Main Reasons-> \"Unknown format, Unsupported codec, Unknown encoder\"";
                case 105: return "No such file or directory found.";
                case 106: return "Error occured while opening codecs.";
                case 107: return "Video processing failed, main reason \"Unknown Encoder\".";
                case 108: return "Watermark image not found. in case of posting watermark on video.";
                case 109: return "Source video extension not found. the variable sending through filename must have a valid video extension.e.g FileName = sample.mpg, not sample.";
                case 110: return "Can't process current request.";
                case 111: return "Invalid video format -format not supported";
                case 112: return "Thumb directory not found";
                case 113: return "Flvtool2.exe not found";
                case 114: return "Custom ffmpeg command not supplied";
                case 115: return "Unrecognized option, most used while using static build instead of shared build";
                case 116: return "Could not open source video, occurs while replacing old video with newly encoded video";
                case 117: return "Permission denied, occurs in case of setting Meta Information for flv videos.";
                case 118: return "Video codec is not compatible with encoded video format.";
                case 119: return "Error while retrieving audio information from video.It arises while parsing audio data returned from video.";
                case 120: return "Error while retrieving video information from video.It arises while parsing video data returned from video.";
                case 121: return "General video processing or video information processing error.Occures while processing video and can't detect by normal parsing script. Its warning message, still video will be encoded properly and information to be retrieved in proper way.";
                case 122: return "Total time of all clips plus clip time exceeds from original video time.Returned while spliting video to many clips.";
                case 123: return "You must setup time transition between two thumbs while capturing multiple thumbs as 1,2,3,4,5,6,7,8,9,10,20 seconds.";
                case 124: return "your thumb settings exceeds from available video duration, make sure that thumb starting position +no of thumbs *transition time not exceed total duration of video in seconds.";
                case 125: return "Bad parameter set, please verify all parameters that you set.";
                case 127: return "Video encoded successfully but failed to add meta information.";
                case 128: return "Error while opening output stream. Maybe incorrect parameters such as bit_rate, rate, width or height.";
                case 129: return "Media Handler Pro component license expired.";
                case 130: return "Incorrect Parameters(when some parameters provided invalide with media type).";
                case 131: return "Unknown video format.";
                case 132: return "Unknown video encoder.";
                case 133: return "Unknown codec.";
                case 134: return "incorrect parameters";
                case 135: return "vhook information invalid, may be space in watermark image path, or watermark.dll path.";
                case 136: return "vhook / watermark.dll path invalid. make sure that you use shared ffmpeg build with vhook support.";
                case 137: return "space not allowed in watermark image path.";
                case 138: return "Join Video: Please specify filenames.";
                case 139: return "Join Video: Input file path validation failed, please make sure that all joining clips / files must be located in input path directory.";
                case 140: return "Must specify output media type (This.OutputExtension).";
                case 141: return "Output File Name must specifiy, e.g \"sample.avi\" or \"sample\", extension will skip if specified.";
                case 142: return "Join Video: Failed to create temp mpg files for attaching videos.";
                case 143: return "Join Video: Must contain two or more clips.";
                case 144: return "preset file: ffpreset file not found";
                case 145: return "Unable to parse value -/ undefined constant or missing value.occurs mostly while sending custom parameters through _obj.Parameters property.";
                case 146: return "Incorrect codec parameters e.g creating .gif file from.avi file.";
                default:
                    return "UNKNOWN_ERROR";
            }
        }
    }
}
