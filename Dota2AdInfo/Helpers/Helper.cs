using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static Dota2AdInfo.Win32Delegates;

namespace Dota2AdInfo.Helpers
{
    internal static class Helper
    {

        internal static Bitmap LoadBitmap(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return new Bitmap(fs);
            }
        }

        internal static Bitmap GetBitmap(int ScreenPositionX, int ScreenPositionY, int width, int height)
        {
            using (Bitmap bmpScreenCapture = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(ScreenPositionX, ScreenPositionY, 0, 0, bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
                }
                return bmpScreenCapture.Clone() as Bitmap;
            }
        }

        internal static Color GetColor(this byte[,,] data, int x, int y)
        {
            try
            {
                return Color.FromArgb(data[0, y, x], data[1, y, x], data[2, y, x]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return Color.FromArgb(data[0, y, x], data[1, y, x], data[2, y, x]);
        }

        internal unsafe static byte[,,] BitmapToByteRgbQ(this Bitmap bmp)
        {
            var width = bmp.Width;
            var height = bmp.Height;
            byte[,,] res = new byte[3, height, width];
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                byte* curpos;
                fixed (byte* _res = res)
                {
                    byte* _r = _res, _g = _res + width * height, _b = _res + 2 * width * height;
                    for (int h = 0; h < height; h++)
                    {
                        curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                        for (int w = 0; w < width; w++)
                        {
                            *_b = *(curpos++); ++_b;
                            *_g = *(curpos++); ++_g;
                            *_r = *(curpos++); ++_r;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bd);
            }
            return res;
        }

        internal static MousePoint GetCursorPosition()
        {
            MousePoint mousePoint;
            var gotPoint = GetCursorPos(out mousePoint);
            if (!gotPoint)
            {
                mousePoint = new MousePoint(0, 0);
            }
            return mousePoint;
        }
    }
}
