using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using Clipboard = System.Windows.Forms.Clipboard;

namespace NiceScreenCapturer
{
    public partial class CaptureClass
    {
        #region Exported WIN APIs

        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, int hdcSrc,
            int nXSrc, int nYSrc, int dwRop);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(int hObject);


        [DllImport("gdi32.dll")]
        private static extern int CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int hdc, int nIndex);

        [DllImport("GDI32.dll")]
        public static extern int SelectObject(int hdc, int hgdiobj);

        [DllImport("User32.dll")]
        public static extern int GetDesktopWindow();

        [DllImport("User32.dll")]
        public static extern int GetWindowDC(int hWnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(int hWnd, int hDC);

        
        #endregion


        //function to capture screen section       
        public static void CaptureScreentoClipboard(int x, int y, int wid, int hei, int startX, int startY, int isSaveToClipboard)
        {
            try
            {
                //create DC for the entire virtual screen
                var hdcSrc = CreateDC("DISPLAY", null, null, IntPtr.Zero);
                var hdcDest = CreateCompatibleDC(hdcSrc);

                var hBitmap = CreateCompatibleBitmap(hdcSrc, wid, hei);
                SelectObject(hdcDest, hBitmap);
                BitBlt(hdcDest, x, y, wid, hei, hdcSrc, startX, startY, 0x40000000 | 0x00CC0020); //SRCCOPY AND CAPTUREBLT

                //var hBitmap = CreateCompatibleBitmap(hdcSrc, 100, 100);
                //SelectObject(hdcDest, hBitmap);

                //BitBlt(hdcDest, 0, 0, 1440, 900, hdcSrc, 1920, 0, 0x40000000 | 0x00CC0020); //SRCCOPY AND CAPTUREBLT
                //BitBlt(hdcDest, 0, 0, 100, 100, hdcSrc, 160, 160, 0x40000000 | 0x00CC0020); //SRCCOPY AND CAPTUREBLT

                // send image to clipboard
                Image imf = Image.FromHbitmap(new IntPtr(hBitmap));
                if (isSaveToClipboard == 1)
                {
                    Clipboard.SetImage(imf);
                }
                else
                {
                    var dlg = new SaveFileDialog
                    {
                        DefaultExt = "jpg",
                        Filter = @"Jpeg Files|*.jpg|PNG Files|*.png"
                    };
                    var res = dlg.ShowDialog();
                    if (res == DialogResult.OK)
                        imf.Save(dlg.FileName, ImageFormat.Jpeg);

                }
                DeleteDC(hdcSrc);
                DeleteDC(hdcDest);
                DeleteObject(hBitmap);
                imf.Dispose();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }


        }

        public static void CaptureScreenWithMarktoClipboard(int x, int y, int wid, int hei, int startX, int startY, List<ZoneInfo> markInfo, int isSaveToClipboard)
        {
            try
            {
                //create DC for the entire virtual screen
                var hdcSrc = CreateDC("DISPLAY", null, null, IntPtr.Zero);
                var hdcDest = CreateCompatibleDC(hdcSrc);

                var hBitmap = CreateCompatibleBitmap(hdcSrc, wid, hei);
                SelectObject(hdcDest, hBitmap);
                BitBlt(hdcDest, x, y, wid, hei, hdcSrc, startX, startY, 0x40000000 | 0x00CC0020); //SRCCOPY AND CAPTUREBLT

                Image imf = Image.FromHbitmap(new IntPtr(hBitmap));

                if (markInfo != null && markInfo.Count > 0)
                {
                    using (var g = Graphics.FromImage(imf))
                    {
                        //set the resize quality modes to high quality
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        var pen = new Pen(markInfo[0].BorderColor, markInfo[0].Thinkness);
                        foreach (var zoneInfo in markInfo)
                        {
                            g.DrawRectangle(pen, (float)zoneInfo.ZoneX, (float)zoneInfo.ZoneY, (float)zoneInfo.ZoneWidth, (float)zoneInfo.ZoneHeight);
                        }
                    }
                }

                if (isSaveToClipboard == 1)
                {
                    // send image to clipboard
                    Clipboard.SetImage(imf);
                }
                else
                {
                    var dlg = new SaveFileDialog
                    {
                        DefaultExt = "jpg",
                        Filter = @"Jpeg Files|*.jpg|PNG Files|*.png"
                    };
                    var res = dlg.ShowDialog();
                    if (res == DialogResult.OK)
                        imf.Save(dlg.FileName, ImageFormat.Jpeg);

                }
                DeleteDC(hdcSrc);
                DeleteDC(hdcDest);
                DeleteObject(hBitmap);
                imf.Dispose();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }


        }


    }

}
