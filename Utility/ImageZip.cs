    using System.IO;  
    using System.Drawing;  
    using System.Drawing.Imaging;  
    using System;  

    namespace UnileverCAS.UnileverFun
    {  
        /// <summary>  
        /// 图片处理类  
        /// 1、生成缩略图片或按照比例改变图片的大小和画质  
        /// 2、将生成的缩略图放到指定的目录下  
        /// </summary>  
        public class ImageHepler  
        {  
            public Image ResourceImage, ReducedImage;  
            private int ImageWidth;  
            private int ImageHeight;  
            public string ErrMessage;  
      
            /// <summary>  
            /// 类的构造函数  
            /// </summary>  
            /// <param name="ImageFileName">图片文件的全路径名称</param>  
            public ImageHepler(string ImageFileName)  
            {  
                ResourceImage = Image.FromFile(ImageFileName);  
                ErrMessage = "";  
            }  
      
            public bool ThumbnailCallback()  
            {  
                return false;  
            }  
      
            /// <summary>  
            /// 生成缩略图，返回缩略图的Image对象  
            /// </summary>  
            /// <param name="Width">缩略图的宽度</param>  
            /// <param name="Height">缩略图的高度</param>  
            /// <returns>缩略图的Image对象</returns>  
            public Image GetReducedImage(int Width, int Height)  
            {  
                double LengthLong;          //存储（长和宽中）较短的长度  
                int widthOK, heightOK;      //存储实际要生成的图片的长宽  
                if (Width < Height)         //判断输入的长和宽那个较短  
                {  
                    LengthLong = Width;     //把较短的存储在 LengthLonh 用于计算  
                }  
                else  
                {  
                    LengthLong = Height;  
                }  
                try  
                {  
                    //判断原图片 长和宽   
                    //原图比较长的一个边要和缩略图的边相等  
                    if (ResourceImage.Width > ResourceImage.Height)  
                    {  
                        widthOK = (int)LengthLong;  
                        heightOK = (int)(LengthLong / ResourceImage.Width * ResourceImage.Height);  
                    }  
                    else  
                    {  
                        heightOK = (int)LengthLong;  
                        widthOK = (int)LengthLong / ResourceImage.Height * ResourceImage.Width;  
      
                    }  
                    Image ReducedImage;  
                    Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);  
                    ReducedImage = ResourceImage.GetThumbnailImage(widthOK, heightOK, callb, IntPtr.Zero);  
                    return ReducedImage;  
                }  
                catch (Exception e)  
                {  
                    ErrMessage = e.Message;  
                    return null;  
                }  
            }  
      
            /// <summary>  
            /// 生成缩略图，将缩略图文件保存到指定的路径  
            /// </summary>  
            /// <param name="Width">缩略图的宽度</param>  
            /// <param name="Height">缩略图的高度</param>  
            /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:\Images\filename.jpg</param>  
            /// <returns>成功返回true，否则返回false</returns>  
            public bool GetReducedImage(int Width, int Height, string targetFilePath)  
            {  
                double LengthLong;          //存储（长和宽中）较短的长度  
                int widthOK, heightOK;      //存储实际要生成的图片的长宽  
                if (Width < Height)         //判断输入的长和宽那个较短  
                {  
                    LengthLong = Width;     //把较短的存储在 LengthLonh 用于计算  
                }  
                else  
                {  
                    LengthLong = Height;  
                }  
                try  
                {  
                    //判断原图片 长和宽   
                    //原图比较长的一个边要和缩略图的边相等  
                    if (ResourceImage.Width > ResourceImage.Height)  
                    {  
                        widthOK = (int)LengthLong;  
                        heightOK = (int)(LengthLong / ResourceImage.Width * ResourceImage.Height);  
                    }  
                    else  
                    {  
                        heightOK = (int)LengthLong;  
                        widthOK = (int)LengthLong / ResourceImage.Height * ResourceImage.Width;  
                    }  
                    Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);  
                    ReducedImage = ResourceImage.GetThumbnailImage(widthOK, heightOK, callb, IntPtr.Zero);  
                    ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);  
                    //ReducedImage.Dispose();  
                    return true;  
                }  
                catch (Exception e)  
                {  
                    ErrMessage = e.Message;  
                    return false;  
                }  
            }  
      
            /// <summary>  
            /// 生成缩略图，返回缩略图的Image对象  
            /// </summary>  
            /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>    
            /// <returns>缩略图的Image对象</returns>  
            public Image GetReducedImage(double Percent)  
            {  
                try  
                {  
                    Image ReducedImage;  
                    Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);  
                    ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);  
                    ImageHeight = Convert.ToInt32(ResourceImage.Width * Percent);  
                    ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);  
                    return ReducedImage;  
                }  
                catch (Exception e)  
                {  
                    ErrMessage = e.Message;  
                    return null;  
                }  
            }  
      
            /// <summary>  
            /// 生成缩略图，返回缩略图的Image对象  
            /// </summary>  
            /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>    
            /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:\Images\filename.jpg</param>  
            /// <returns>成功返回true,否则返回false</returns>  
            public bool GetReducedImage(double Percent, string targetFilePath)  
            {  
                try  
                {  
                    Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);  
                    ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);  
                    ImageHeight = Convert.ToInt32(ResourceImage.Width * Percent);  
                    ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);  
                    ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);  
                    //ReducedImage.Dispose();  
                    return true;  
                }  
                catch (Exception e)  
                {  
                    ErrMessage = e.Message;  
                    return false;  
                }  
            }  
        }  
    }  
    