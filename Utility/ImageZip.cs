    using System.IO;  
    using System.Drawing;  
    using System.Drawing.Imaging;  
    using System;  

    namespace UnileverCAS.UnileverFun
    {  
        /// <summary>  
        /// ͼƬ������  
        /// 1����������ͼƬ���ձ����ı�ͼƬ�Ĵ�С�ͻ���  
        /// 2�������ɵ�����ͼ�ŵ�ָ����Ŀ¼��  
        /// </summary>  
        public class ImageHepler  
        {  
            public Image ResourceImage, ReducedImage;  
            private int ImageWidth;  
            private int ImageHeight;  
            public string ErrMessage;  
      
            /// <summary>  
            /// ��Ĺ��캯��  
            /// </summary>  
            /// <param name="ImageFileName">ͼƬ�ļ���ȫ·������</param>  
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
            /// ��������ͼ����������ͼ��Image����  
            /// </summary>  
            /// <param name="Width">����ͼ�Ŀ��</param>  
            /// <param name="Height">����ͼ�ĸ߶�</param>  
            /// <returns>����ͼ��Image����</returns>  
            public Image GetReducedImage(int Width, int Height)  
            {  
                double LengthLong;          //�洢�����Ϳ��У��϶̵ĳ���  
                int widthOK, heightOK;      //�洢ʵ��Ҫ���ɵ�ͼƬ�ĳ���  
                if (Width < Height)         //�ж�����ĳ��Ϳ��Ǹ��϶�  
                {  
                    LengthLong = Width;     //�ѽ϶̵Ĵ洢�� LengthLonh ���ڼ���  
                }  
                else  
                {  
                    LengthLong = Height;  
                }  
                try  
                {  
                    //�ж�ԭͼƬ ���Ϳ�   
                    //ԭͼ�Ƚϳ���һ����Ҫ������ͼ�ı����  
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
            /// ��������ͼ��������ͼ�ļ����浽ָ����·��  
            /// </summary>  
            /// <param name="Width">����ͼ�Ŀ��</param>  
            /// <param name="Height">����ͼ�ĸ߶�</param>  
            /// <param name="targetFilePath">����ͼ�����ȫ�ļ�����(��·��)��������ʽ��D:\Images\filename.jpg</param>  
            /// <returns>�ɹ�����true�����򷵻�false</returns>  
            public bool GetReducedImage(int Width, int Height, string targetFilePath)  
            {  
                double LengthLong;          //�洢�����Ϳ��У��϶̵ĳ���  
                int widthOK, heightOK;      //�洢ʵ��Ҫ���ɵ�ͼƬ�ĳ���  
                if (Width < Height)         //�ж�����ĳ��Ϳ��Ǹ��϶�  
                {  
                    LengthLong = Width;     //�ѽ϶̵Ĵ洢�� LengthLonh ���ڼ���  
                }  
                else  
                {  
                    LengthLong = Height;  
                }  
                try  
                {  
                    //�ж�ԭͼƬ ���Ϳ�   
                    //ԭͼ�Ƚϳ���һ����Ҫ������ͼ�ı����  
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
            /// ��������ͼ����������ͼ��Image����  
            /// </summary>  
            /// <param name="Percent">����ͼ�Ŀ�Ȱٷֱ� �磺��Ҫ�ٷ�֮80������0.8</param>    
            /// <returns>����ͼ��Image����</returns>  
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
            /// ��������ͼ����������ͼ��Image����  
            /// </summary>  
            /// <param name="Percent">����ͼ�Ŀ�Ȱٷֱ� �磺��Ҫ�ٷ�֮80������0.8</param>    
            /// <param name="targetFilePath">����ͼ�����ȫ�ļ�����(��·��)��������ʽ��D:\Images\filename.jpg</param>  
            /// <returns>�ɹ�����true,���򷵻�false</returns>  
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
    