using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.ImageUtility.Enums;
using Fhs.Bulletin.E_Utility.VideoUtility.Enums;
using Fhs.Bulletin.E_Utility.VideoUtility.HelperClasses;

namespace Fhs.Bulletin.E_Utility.VideoUtility
{
    public class VideoUtilities
    {
        #region variables
        private MediaHandler _mhandler = null;

        private Exception _ex = null;
        public bool HasError => _ex != null;
        public Exception GetError() => _ex;
        public void SetException(Exception ex)
        {
            _ex = ex;
        }
        public void SetException(string message)
        {
            _ex = new Exception(message);
        }

        #endregion

        #region Constructors
        public VideoUtilities()
        {
            _mhandler = ConfigMediaHandler();
        }
        #endregion

        #region  Methods

        /// <summary>
        /// Getting Videoes Meta Data
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public VideoInfo GetFileMetaData(string inputPath, string filePath)
        {
            try
            {
                if (!File.Exists(inputPath + filePath))
                {
                    SetException("File Not Found");
                    return null;
                }
                _mhandler.InputPath = inputPath;
                _mhandler.FileName = filePath;
                VideoInfo info = _mhandler.Get_Info();
                if (info.ErrorCode > 0 && info.ErrorCode != 121)
                {
                    SetException(MediaHandlerErrorHelper.GetErrorMessage(info.ErrorCode));
                }
                return info;
            }
            catch (Exception ex)
            {
                SetException(ex);
                return null;
            }
        }

        /// <summary>
        /// Adding Watermark On Videoes
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="watermarkFilePath"></param>
        /// <param name="position"></param>
        /// <param name="exc"></param>
        public string AddWatermarkOnVideo(string filePath, string watermarkFilePath, WatermarkPositionEnum position, string outputFilePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFilePath) || string.IsNullOrWhiteSpace(outputFilePath)) outputFilePath = filePath;
                if (!File.Exists(_mhandler.InputPath + filePath))
                {
                    SetException("File Not Found");
                    return "";
                }

                if (!File.Exists(_mhandler.InputPath + watermarkFilePath))
                {
                    SetException("Watermark File Not Found");
                    return "";
                }


                string ffmpeg = MediaConfigPath.PathFFMPEG;

                System.Diagnostics.Process ffmpegProcess = new System.Diagnostics.Process();

                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.CreateNoWindow = true;
                //startInfo.UseShellExecute = false;
                //startInfo.RedirectStandardError = true;
                //startInfo.RedirectStandardOutput = true;

                string overlay = "10:10";
                switch (position)
                {
                    case WatermarkPositionEnum.TopLeft:
                        overlay = "10:10";
                        break;
                    case WatermarkPositionEnum.TopRight:
                        overlay = "main_w-overlay_w-10:10";
                        break;
                    case WatermarkPositionEnum.BottomLeft:
                        overlay = "10:main_h-overlay_h-10";
                        break;
                    case WatermarkPositionEnum.BottomRight:
                        overlay = "main_w-overlay_w-10:main_h-overlay_h-10";
                        break;
                    default:
                        overlay = "10:10";
                        break;
                }

                startInfo.Arguments = $"-y -i {MediaConfigPath.PathInputs + filePath} -i {MediaConfigPath.PathWatermarks + watermarkFilePath} -filter_complex \"overlay = {overlay}\" {MediaConfigPath.PathOutputs + outputFilePath}";

                var rs = System.Diagnostics.Process.Start(startInfo);
                rs.WaitForExit();
                //string outut = rs.StandardOutput.ReadToEnd();
                //string error = rs.StandardError.ReadToEnd();
                //int exitCode = rs.ExitCode;
                //if(!string.IsNullOrEmpty(error) && !string.IsNullOrWhiteSpace(error))
                //{
                //    throw new Exception(error);
                //}
                //return outut;
                return MediaConfigPath.PathOutputs + outputFilePath;
            }
            catch (Exception ex)
            {
                SetException(ex);
                return "";
            }
        }

        /// <summary>
        /// Resizing Video
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public VideoInfo ResizeVideo(string fileName, int width, int height, string outputFileName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFileName) || string.IsNullOrWhiteSpace(outputFileName)) outputFileName = fileName;
                if (!File.Exists(_mhandler.InputPath + fileName))
                {
                    SetException("File Not Found");
                    return null;
                }

                string ext = ".mp4";
                int dotIndex = fileName.LastIndexOf(".");
                if (dotIndex >= 0)
                {
                    ext = fileName.Substring(dotIndex, fileName.Length - dotIndex);
                }

                _mhandler.FileName = fileName;

                _mhandler.OutputExtension = ext;
                _mhandler.OutputFileName = outputFileName;
                _mhandler.VCodec = "libx264";
                //_mhandler.ACodec = "libvo_aacenc";
                _mhandler.Channel = 2;
                _mhandler.Audio_SamplingRate = 48000;
                _mhandler.Audio_Bitrate = 192;
                _mhandler.Video_Bitrate = 500;
                _mhandler.FrameRate = 29.97;
                _mhandler.Width = width;
                _mhandler.Height = height;
                _mhandler.Scale_Quality = 1;

                _mhandler.Parameters = $" -s {width}x{height}";// -fpre {MediaConfigPath.PathPresets}libvpx-360p.ffpreset";

                VideoInfo info = _mhandler.Process();

                if (info.ErrorCode > 0 && info.ErrorCode != 121)
                {
                    SetException(MediaHandlerErrorHelper.GetErrorMessage(info.ErrorCode));
                }

                return info;

            }
            catch (Exception ex)
            {
                SetException(ex);
                return null;
            }
        }

        /// <summary>
        /// Extracting Audio From Video File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public VideoInfo ExtractAudio(string fileName, string outputFileName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFileName) || string.IsNullOrWhiteSpace(outputFileName)) outputFileName = fileName;
                if (!File.Exists(_mhandler.InputPath + fileName))
                {
                    SetException("File Not Found");
                    return null;
                }

                _mhandler.FileName = fileName;
                _mhandler.OutputExtension = ".mp3";
                _mhandler.OutputFileName = outputFileName;

                _mhandler.Parameters = $"-vn -ar 44100 -ac 2 -ab 192k -f mp3";

                VideoInfo info = _mhandler.Process();

                if (info.ErrorCode > 0 && info.ErrorCode != 121)
                {
                    SetException(MediaHandlerErrorHelper.GetErrorMessage(info.ErrorCode));
                }

                return info;

            }
            catch (Exception ex)
            {
                SetException(ex);
                return null;
            }
        }

        /// <summary>
        /// Trim and cut video
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public VideoInfo TrimVideo(string filePath, int start, int length, string outputFileName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFileName) || string.IsNullOrWhiteSpace(outputFileName)) outputFileName = filePath;
                if (!File.Exists(_mhandler.InputPath + filePath))
                {
                    SetException("File Not Found");
                    return null;
                }

                _mhandler.FileName = filePath;

                string ext = ".mp4";
                int dotIndex = filePath.LastIndexOf(".");
                if (dotIndex >= 0)
                {
                    ext = filePath.Substring(dotIndex, filePath.Length - dotIndex);
                }

                _mhandler.OutputExtension = ext;
                _mhandler.OutputFileName = outputFileName;

                _mhandler.Parameters = $"-ss {start} -codec copy -t {length}";

                VideoInfo info = _mhandler.Process();

                if (info.ErrorCode > 0 && info.ErrorCode != 121)
                {
                    SetException(MediaHandlerErrorHelper.GetErrorMessage(info.ErrorCode));
                }

                return info;

            }
            catch (Exception ex)
            {
                SetException(ex);
                return null;
            }
        }

        /// <summary>
        /// Merge Audio and Video
        /// </summary>
        /// <param name="videoFilePath"></param>
        /// <param name="audioFilePath"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public VideoInfo MergeAudioAndVideo(string videoFilePath, string audioFilePath, string outputFilePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFilePath) || string.IsNullOrWhiteSpace(outputFilePath)) outputFilePath = videoFilePath;
                if (!File.Exists(videoFilePath))
                {
                    SetException("Video File Not Found");
                    return null;
                }
                if (!File.Exists(audioFilePath))
                {
                    SetException("Audio File Not Found");
                    return null;
                }

                _mhandler.FileName = audioFilePath;
                _mhandler.OutputExtension = ".mp4";
                _mhandler.OutputFileName = outputFilePath;

                _mhandler.Parameters = $"-i {MediaConfigPath.PathInputs + videoFilePath} -c:v copy -c:a aac -strict experimental";

                VideoInfo info = _mhandler.Process();

                if (info.ErrorCode > 0 && info.ErrorCode != 121)
                {
                    SetException(MediaHandlerErrorHelper.GetErrorMessage(info.ErrorCode));
                }

                return info;

            }
            catch (Exception ex)
            {
                SetException(ex);
                return null;
            }
        }

        /// <summary>
        /// Change Video Encode 240/360/480/720/1080
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encode"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public VideoInfo ChangeVideoEncode(string fileName, VideoEncodeEnum encode, string outputFileName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFileName) || string.IsNullOrWhiteSpace(outputFileName)) outputFileName = fileName;
                if (!File.Exists(_mhandler.InputPath + fileName))
                {
                    SetException("File Not Found");
                    return null;
                }

                int width = 0;
                int height = 0;
                int video_bitrate = 0;
                int audio_bitrate = 0;
                int audio_channel = 2;
                switch (encode)
                {
                    case VideoEncodeEnum.Encode240:
                        width = 400;
                        height = 224;
                        video_bitrate = 350;
                        audio_bitrate = 131;
                        audio_channel = 1;
                        break;
                    case VideoEncodeEnum.Encode360:
                        width = 640;
                        height = 360;
                        video_bitrate = 676;
                        audio_bitrate = 131;
                        audio_channel = 1;
                        break;
                    case VideoEncodeEnum.Encode480:
                        width = 640;
                        height = 360;
                        video_bitrate = 667;
                        audio_bitrate = 96;
                        audio_channel = 2;
                        break;
                    case VideoEncodeEnum.Encode720:
                        width = 1280;
                        height = 720;
                        video_bitrate = 2668;
                        audio_bitrate = 96;
                        audio_channel = 2;
                        break;
                    case VideoEncodeEnum.Encode1080:
                        width = 1920;
                        height = 1080;
                        video_bitrate = 4997;
                        audio_bitrate = 96;
                        audio_channel = 2;
                        break;
                    default:
                        width = 640;
                        height = 360;
                        video_bitrate = 667;
                        audio_bitrate = 96;
                        audio_channel = 2;
                        break;
                }


                string ext = ".mp4";
                int dotIndex = fileName.LastIndexOf(".");
                if (dotIndex >= 0)
                {
                    ext = fileName.Substring(dotIndex, fileName.Length - dotIndex);
                }


                _mhandler.FileName = fileName;
                _mhandler.OutputExtension = ext;
                _mhandler.OutputFileName = outputFileName;
                _mhandler.VCodec = "libx264";
                _mhandler.Parameters = $"-strict experimental -s {width}x{height}";// -fpre " + presetpath + "";
                _mhandler.ACodec = "aac";
                _mhandler.Video_Bitrate = video_bitrate;
                _mhandler.Channel = audio_channel;
                _mhandler.Audio_SamplingRate = 44100;
                _mhandler.Audio_Bitrate = audio_bitrate;
                _mhandler.FrameRate = 29.97;

                VideoInfo info = _mhandler.Process();

                if (info.ErrorCode > 0 && info.ErrorCode != 121)
                {
                    SetException(MediaHandlerErrorHelper.GetErrorMessage(info.ErrorCode));
                }

                return info;

            }
            catch (Exception ex)
            {
                SetException(ex);
                return null;
            }
        }

        #endregion

        #region Helper Methods
        private static MediaHandler ConfigMediaHandler()
        {
            try
            {
                MediaHandler _mhandler = new MediaHandler();
                if (!MediaConfigPath.CheckBasicIsFills())
                    throw new Exception("Required Executable Files Not Found. Please Check MediaConfigPath Class.");
                _mhandler.FFMPEGPath = MediaConfigPath.PathFFMPEG;
                _mhandler.InputPath = MediaConfigPath.PathInputs;
                _mhandler.OutputPath = MediaConfigPath.PathOutputs;
                _mhandler.WaterMarkPath = MediaConfigPath.PathWatermarks;
                _mhandler.FLVToolPath = MediaConfigPath.PathFLVTOOL;
                _mhandler.MP4BoxPath = MediaConfigPath.PathMP4Box;

                return _mhandler;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}