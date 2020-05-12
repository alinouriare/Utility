using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Fhs.Bulletin.E_Utility.ImageUtility.Enums;
using Fhs.Bulletin.E_Utility.ImageUtility.Models;
using Fhs.Bulletin.E_Utility.PublicUtility;
using QRCoder;

namespace Fhs.Bulletin.E_Utility.ImageUtility
{
    public static class ImageUtilities
    {
        #region Variables
        #endregion

        #region Methods

        public static void FitImageToForm(string originalFile, string newFileAddress, int frameWidth, int frameHeight)
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(originalFile);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            int newWidth, newHeight;

            if (FullsizeImage.Height < FullsizeImage.Width)
            {
                newHeight = frameHeight;
                newWidth = FullsizeImage.Width * Convert.ToInt32((float)newHeight / (float)FullsizeImage.Height);
            }
            else
            {
                newWidth = frameWidth;
                newHeight = FullsizeImage.Height * Convert.ToInt32((float)newWidth / (float)FullsizeImage.Width);
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();
            // Save resized picture
            NewImage.Save(newFileAddress);
        }
        public static void FitFormToImage(string originalFile, string newFileAddress, int frameWidth, int frameHeight,bool flag)
        {
            System.IO.FileInfo imagefileinfo = new FileInfo(originalFile);

            if (!imagefileinfo.Exists)
                return;

            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(originalFile);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            int newWidth, newHeight;

            if (FullsizeImage.Height > FullsizeImage.Width)
            {
                if (flag && FullsizeImage.Height < frameHeight)
                {
                    FullsizeImage.Save(newFileAddress);
                    return;
                }
                newHeight = frameHeight;
                newWidth = (int)(FullsizeImage.Width * ((float)newHeight / (float)FullsizeImage.Height));
            }
            else
            {
                if (flag && FullsizeImage.Width < frameWidth)
                {
                    FullsizeImage.Save(newFileAddress);
                    return;
                }
                newWidth = frameWidth;
                newHeight = (int)(FullsizeImage.Height * ((float)newWidth / (float)FullsizeImage.Width));
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            NewImage.Save(newFileAddress);

            NewImage.Dispose();
        }
        public static int[] ReturnImageSize(String address)
        {
            int w = 0;
            int h = 1;
            int[] imagesize = new int[2];
            imagesize[w] = 0;
            imagesize[h] = 0;
            //We Must Find Image Size
            FileInfo fileinfo = new FileInfo(address);
            if (!fileinfo.Exists) return imagesize;

            System.Drawing.Image imagefile = System.Drawing.Image.FromFile(address);

            imagesize[w] = imagefile.Width;
            imagesize[h] = imagefile.Height;

            return imagesize;
        }
        public static int[] ReturnImageSize(byte[] fileContent)
        {
            try
            {
                int w = 0;
                int h = 1;
                int[] imagesize = new int[2];
                imagesize[w] = 0;
                imagesize[h] = 0;
                using (var memStreamMain = new MemoryStream(fileContent))
                using (Image image = Image.FromStream(memStreamMain))
                {
                    imagesize[w] = image.Width;
                    imagesize[h] = image.Height;
                }

                return imagesize;
            }
            catch (Exception ex)
            {
                return new int[0];
            }
        }

        public static int[] CalculateImageSize(int imageWidth, int imageHeight, int frameWidth, int frameHeight)
        {
            var size = new int[2];

            if (imageWidth > imageHeight)
            {
                size[0] = frameWidth;
                size[1] = (int)(imageHeight * ((float)frameWidth / (float)imageWidth));
            }
            else
            {
                size[1] = frameHeight;
                size[0] = (int)(imageWidth * ((float)frameHeight / (float)imageHeight));
            }

            return size;
        }
        public static int[] CalculateImageSize2(int imageWidth, int imageHeight, int frameWidth, int frameHeight)
        {
            int[] size = CalculateImageSize(imageWidth, imageHeight, frameWidth, frameHeight);

            if (size[0] > size[1])
            {
                if (size[1] > frameHeight)
                {
                    size[0] = (int)(size[0] * ((float)frameHeight / (float)size[1]));
                    size[1] = frameHeight;
                }
            }
            else
            {
                if (size[0] > frameWidth)
                {
                    size[1] = (int)(size[1] * ((float)frameWidth / (float)size[0]));
                    size[0] = frameWidth;
                }
            }


            return size;
        }
        public static void DeleteImageFromHdd(string address) //Create By Ali Mohit
        {
            FileInfo file = new FileInfo(address);
            if (!file.IsReadOnly && file.Exists)
            {
                try
                {
                    File.Delete(address);
                }
                catch
                {
                    return;
                }
            }
        }
        public static double[] ReturnNeedSize(int widthfram, int heightfram, double aspPic)
        {

            if (aspPic == 0) return null;
            int _WIDTH = 0;
            int _HEIGH = 0;
            double[] array = new double[2];

            if (aspPic >= 1)
            {
                array[_WIDTH] = widthfram;
                array[_HEIGH] = widthfram / aspPic;
            }
            else
            {
                array[_WIDTH] = widthfram * aspPic;
                array[_HEIGH] = heightfram;
            }

            return array;
        }
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public static Bitmap GenerateQRCodeImage(string qrcode, int w)
        {
            try
            {
                var qrGenerator = new QRCodeGenerator();
                var qrCodeImage = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);


                return qrCodeImage.GetGraphic(w);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static String GenerateQRCodeAndConvertToStringBase64(String qrCodeString)
        {
            try
            {
                var qrGenerator = new QRCodeGenerator();
                var qrCode = qrGenerator.CreateQrCode(qrCodeString, QRCodeGenerator.ECCLevel.Q);
                var result = "";

                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        result = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static bool IsImageFile(string fileExtention)
        {
            try
            {
                var ext = fileExtention.ToLower().Replace(".", "");

                if (ext == "jpeg" || ext == "jpg" ||
                    ext == "bmp" || ext == "png" || ext == "gif")
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool IsImageFileByMimeType(string mimeType)
        {
            try
            {
                var ext = mimeType.ToLower();

                if (ext == "image/bmp" || ext == "image/gif" ||
                    ext == "image/jpeg" || ext == "image/svg+xml" || ext == "image/tiff" || ext == "image/x-icon")
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string GetMimeType(string extension)
        {
            return MimeAssistantUtilities.GetMIMEType(extension);
        }

        /// <summary>
        /// Adding Watermark on Images
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="watermarkFileName"></param>
        /// <param name="position"></param>
        /// <param name="exc"></param>
        public static string AddWatermarkOnImage(string filePath, string watermarkFilePath, WatermarkPositionEnum position, out Exception exc, string outputFilePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFilePath) || string.IsNullOrWhiteSpace(outputFilePath)) outputFilePath = filePath;
                //if (!FileHelper.ExistFileInInput(fileName))
                //{
                //    exc = new Exception("File Not Found");
                //    return "";
                //}

                //if (!FileHelper.ExistFileInInput(fileName))
                //{
                //    exc = new Exception("Watermark File Not Found");
                //    return "";
                //}

                using (Image image = Image.FromFile(filePath))
                using (Image watermarkImage = Image.FromFile(watermarkFilePath))
                using (Graphics imageGraphics = Graphics.FromImage(image))
                using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
                {
                    int x = 10;
                    int y = 10;
                    switch (position)
                    {
                        case WatermarkPositionEnum.TopLeft:
                            x = 10;
                            y = 10;
                            break;
                        case WatermarkPositionEnum.TopRight:
                            x = image.Width - watermarkImage.Width - 10;
                            y = 10;
                            break;
                        case WatermarkPositionEnum.BottomLeft:
                            x = 10;
                            y = image.Height - watermarkImage.Height - 10;
                            break;
                        case WatermarkPositionEnum.BottomRight:
                            x = image.Width - watermarkImage.Width - 10;
                            y = image.Height - watermarkImage.Height - 10;
                            break;
                        default:
                            break;
                    }
                    watermarkBrush.TranslateTransform(x, y);
                    imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));
                    image.Save(outputFilePath);
                }
                exc = null;
                return outputFilePath;
            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }

        /// <summary>
        /// Adding Watermark on Images
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="watermarkFileName"></param>
        /// <param name="position"></param>
        /// <param name="exc"></param>
        public static string AddWatermarkOnImage(byte[] mainImage, byte[] watermarkFileImage, WatermarkPositionEnum position, out Exception exc, string outputFilePath = "")
        {
            try
            {
                //if (!FileHelper.ExistFileInInput(fileName))
                //{
                //    exc = new Exception("File Not Found");
                //    return "";
                //}

                //if (!FileHelper.ExistFileInInput(fileName))
                //{
                //    exc = new Exception("Watermark File Not Found");
                //    return "";
                //}

                using (var memStreamMain = new MemoryStream(mainImage))
                using (var memStreamWater = new MemoryStream(watermarkFileImage))
                using (Image image = Image.FromStream(memStreamMain))
                using (Image watermarkImage = Image.FromStream(memStreamWater))
                using (Graphics imageGraphics = Graphics.FromImage(image))
                using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
                {
                    int x = 10;
                    int y = 10;
                    int wRepeat = 1;
                    int hRepeat = 1;
                    switch (position)
                    {
                        case WatermarkPositionEnum.TopLeft:
                            x = 10;
                            y = 10;
                            break;
                        case WatermarkPositionEnum.TopRight:
                            x = image.Width - watermarkImage.Width - 10;
                            y = 10;
                            break;
                        case WatermarkPositionEnum.BottomLeft:
                            x = 10;
                            y = image.Height - watermarkImage.Height - 10;
                            break;
                        case WatermarkPositionEnum.BottomRight:
                            x = image.Width - watermarkImage.Width - 10;
                            y = image.Height - watermarkImage.Height - 10;
                            break;
                        case WatermarkPositionEnum.Repeat:
                            x = image.Width - watermarkImage.Width;
                            y = 0;
                            wRepeat = (image.Width / watermarkImage.Width) + 1 + 1;
                            hRepeat = (image.Height / watermarkImage.Height) + 1 + 1;
                            break;
                        default:
                            break;
                    }
                    watermarkBrush.TranslateTransform(x, y);
                    for (int i = 0; i < hRepeat; i++)
                    {
                        for (int j = 0; j < wRepeat; j++)
                        {
                            imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));
                            x -= watermarkImage.Width;
                        }
                        x = image.Width - watermarkImage.Width;
                        y += watermarkImage.Height;
                    }
                    image.Save(outputFilePath);
                }
                exc = null;
                return outputFilePath;
            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }

        public static string AddWatermarkOnImage(byte[] mainImage, byte[] watermarkFileImage, float watermarkOpacity, WatermarkPositionEnum position, out Exception exc, string outputFilePath = "")
        {
            try
            {
                if (watermarkOpacity >= 0.0 && watermarkOpacity < 1.0)
                {
                    Exception exceptionWM = null;
                    Exception exceptionWMC = null;

                    Image watermarkImage = SetImageOpacity(watermarkFileImage, watermarkOpacity, out exceptionWM);
                    if (exceptionWM != null) throw exceptionWM;
                    var watermarkContent = ImageToByteArray(watermarkImage, out exceptionWMC);
                    if (exceptionWMC != null) throw exceptionWMC;
                    return AddWatermarkOnImage(mainImage, watermarkContent, position, out exc, outputFilePath);
                }
                return AddWatermarkOnImage(mainImage, watermarkFileImage, position, out exc, outputFilePath);
            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }

        /// <summary>
        /// Crop Image
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="cropArea"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="exc"></param>
        /// <returns></returns>
        public static string CropImage(byte[] fileContent, Rectangle cropArea, out Exception exc, string outputFilePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
                {
                    throw new Exception("آدرس خروجی فایل را وارد نمایید");
                }

                using (var memStreamMain = new MemoryStream(fileContent))
                using (Image image = Image.FromStream(memStreamMain))
                {

                    Bitmap bmp = new Bitmap(image);

                    if (bmp == null)
                        throw new ArgumentException("No valid bitmap");
                    Bitmap cropBmp;
                    cropBmp = bmp.Clone(cropArea, bmp.PixelFormat);

                    image.Dispose();

                    cropBmp.Save(outputFilePath);
                    exc = null;
                    return outputFilePath;
                }


            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }
        /// <summary>
        /// Crop Image
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="cropArea"></param>
        /// <param name="exc"></param>
        public static string CropImage(byte[] fileContent, Point point, Size size, out Exception exc, string outputFilePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
                {
                    throw new Exception("آدرس خروجی فایل را وارد نمایید");
                }
                Rectangle cropArea = new Rectangle(point, size);
                return CropImage(fileContent, cropArea, out exc, outputFilePath);
            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }
        /// <summary>
        /// Crop Image
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="cropArea"></param>
        /// <param name="exc"></param>
        public static string CropImage(byte[] fileContent, int x, int y, int width, int height, out Exception exc, string outputFilePath = "")
        {
            try
            {

                if (string.IsNullOrEmpty(outputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
                {
                    throw new Exception("آدرس خروجی فایل را وارد نمایید");
                }
                Rectangle cropArea = new Rectangle(x, y, width, height);
                return CropImage(fileContent, cropArea, out exc, outputFilePath);

            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }
        public static string CropImage(byte[] fileContent, CropImageModel model, out Exception exc, string outputFilePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(outputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
                {
                    throw new Exception("آدرس خروجی فایل را وارد نمایید");
                }
                Rectangle cropArea = new Rectangle(model.X, model.Y, model.Width, model.Height);
                return CropImage(fileContent, cropArea, out exc, outputFilePath);

            }
            catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }

        /// <param name="image">image to set opacity on</param>  
        /// <param name="opacity">percentage of opacity</param>  
        /// <returns></returns>  
        public static Image SetImageOpacity(Image image, float opacity, out Exception exception)
        {
            try
            {
                exception = null;

                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        public static Image SetImageOpacity(byte[] fileContent, float opacity, out Exception exception)
        {
            try
            {
                using (var memStreamMain = new MemoryStream(fileContent))
                using (Image image = Image.FromStream(memStreamMain))
                {
                    return SetImageOpacity(image, opacity, out exception);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        public static byte[] ImageToByteArray(Image imageIn, out Exception exception)
        {
            try
            {
                exception = null;
                //using (var ms = new MemoryStream())
                //{
                //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //    return ms.ToArray();
                //}
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(imageIn, typeof(byte[]));
                return xByte;
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }


        #endregion

        #region Helper Methods
        #endregion
    }
}