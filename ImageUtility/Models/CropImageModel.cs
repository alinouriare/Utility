using System;
using System.Collections.Generic;

namespace Fhs.Bulletin.E_Utility.ImageUtility.Models
{
    public class CropImageModel
    {
        #region Variable
        public int Width
        {
            get
            {
                if (_width < 0)
                    return 0;
                return _width;
            }
            set { _width = value; }
        }
        public int Height
        {
            get
            {
                if (_height < 0)
                    return 0;
                return _height;
            }
            set { _height = value; }
        }
        public int X
        {
            get
            {
                if (_x < 0)
                    return 0;
                return _x;
            }
            set { _x = value; }
        }
        public int Y
        {
            get
            {
                if (_y < 0)
                    return 0;
                return _y;
            }
            set { _y = value; }
        }
        public int ImageViewWidth
        {
            get
            {
                if (_imgVW < 0)
                    return 0;
                return _imgVW;
            }
            set { _imgVW = value; }
        }
        public int ImageViewHeight
        {
            get
            {
                if (_imgVH < 0)
                    return 0;
                return _imgVH;
            }
            set { _imgVH = value; }
        }

        private int _width = 0;
        private int _height = 0;
        private int _x = 0;
        private int _y = 0;
        private int _imgVW = 0;
        private int _imgVH = 0;

        #endregion

        #region Constructors
        public CropImageModel()
        {
            MakeDefault();
        }
        #endregion

        #region Helper Methods
        public void MakeDefault()
        {
            Width = 0;
            Height = 0;
            X = 0;
            Y = 0;
            ImageViewHeight = 0;
            ImageViewWidth = 0;
        }
        public List<Exception> isValidModel()
        {
            var lst = new List<Exception>();
            try
            {
                if (ImageViewHeight == 0)
                    lst.Add(new Exception("ImageViewHeight could not be zero"));
                if (ImageViewWidth == 0)
                    lst.Add(new Exception("ImageViewWidth could not be zero"));
            }
            catch (Exception ex)
            {
                lst.Add(ex);
            }
            return lst;
        }

        public CropImageModel ConvertCropImageModel(int mainImageWidth, int mainImageHeight, out Exception exData)
        {
            exData = null;
            try
            {
                var result = new CropImageModel();

                if (ImageViewWidth == 0 || mainImageHeight == 0)
                    throw new Exception("ImageViewWidth or mainImageHeight was zero.");

                var rationX = ((double)mainImageWidth / ImageViewWidth);
                var rationY = ((double)mainImageHeight / ImageViewHeight);

                result.X = (int)(rationX * X);
                result.Y = (int)(rationY * Y);
                result.Width = (int)(rationX * Width);
                result.Height = (int)(rationY * Height);

                return result;
            }
            catch (Exception ex)
            {
                exData = ex;
                return null;
            }
        }

        #endregion

    }
}