using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Point = System.Drawing.Point;

namespace NiceScreenCapturer
{
    public partial class FrmSettings : Form
    {
        CultureInfo _culture;
        private readonly NotifyIcon _notifyIcon;
        public Screen[] AllScreen { get; set; }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int ModAlt = 0x0001;
        const int ModControl = 0x0002;
        const int ModShift = 0x0004;
        const int WmHotkey = 0x0312;
        public FrmSettings()
        {
            InitializeComponent();
            _notifyIcon = new NotifyIcon()
            {
                ContextMenuStrip = contextMenuStrip,
                BalloonTipIcon = ToolTipIcon.Info,
                BalloonTipText = @"Press [Ctrl + Alt + A] to capturing",
                BalloonTipTitle = @"Capture Screen Tools",
                Text = @"Nice Screen Capturer",
                Icon = Properties.Resources.podcaste_capture
            };

            AllScreen = Screen.AllScreens;

        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                //Capture region
                if (m.Msg == WmHotkey && (int)m.WParam == 1)
                {
                    CaptureRegion();
                }

                //Capture screen
                if (m.Msg == WmHotkey && (int)m.WParam == 2)
                {
                    CaptureScreen();
                }

                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"HookKeyDown: " + ex.Message);
                throw;
            }
        }

        private void CaptureRegion()
        {
            try
            {
                var enableDrawMark = Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\NiceScreenCapturer", "DrawMark", 0));
                var currScr = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));
                var scr = AllScreen.First(m => m.DeviceName == currScr.DeviceName);
                var win = new Window1
                {
                    Topmost = true,
                    Left = scr.WorkingArea.Left,
                    CurrentScreen = scr,
                    IsCaptureScreen = false,
                    IsDrawMark = enableDrawMark == 1
                };
                win.SourceInitialized += (snd, arg) => win.WindowState = System.Windows.WindowState.Maximized;
                win.ShowDialog();

                if (win.IsQuitByEscape)
                    return;

                //Starting Capture
                var scaleScreen = win.CurrentScreen;
                var scaleFactor = win.ZoneFirst.ScaleFactor;

                var rightScreenInfo = new ScreenInfo();
                var rightScreenMark = new List<ZoneInfo>();
                if (scaleScreen.Bounds.X == 0)
                {
                    //Get region of primary screen
                    rightScreenInfo.X = 0;
                    rightScreenInfo.Y = 0;
                    rightScreenInfo.Width = (int)(win.ZoneFirst.ZoneWidth * scaleFactor);
                    rightScreenInfo.Height = (int)(win.ZoneFirst.ZoneHeight * scaleFactor);
                    rightScreenInfo.StartX = (int)((win.ZoneFirst.ZoneX - 6.4) * scaleFactor);
                    rightScreenInfo.StartY = (int)((win.ZoneFirst.ZoneY - 6.4) * scaleFactor);
                }
                else
                {
                    //Get region of another screen
                    var scrCurrent = AllScreen.First(m => m.DeviceName == scaleScreen.DeviceName);
                    rightScreenInfo.X = 0;
                    rightScreenInfo.Y = 0;
                    rightScreenInfo.Width = (int)(win.ZoneFirst.ZoneWidth * scaleFactor);
                    rightScreenInfo.Height = (int)(win.ZoneFirst.ZoneHeight * scaleFactor);
                    rightScreenInfo.StartX = (int)((win.ZoneFirst.ZoneX - 6.4) * scaleFactor + scrCurrent.Bounds.X);
                    rightScreenInfo.StartY = (int)((win.ZoneFirst.ZoneY - 6.4) * scaleFactor);
                }

                if (win.ZoneMark != null && win.ZoneMark.Count > 0)
                {
                    //Get region of mark rectangle
                    foreach (var zoneInfo in win.ZoneMark)
                    {
                        var rightScreenMarkItem = new ZoneInfo
                        {
                            ZoneX = (double)(zoneInfo.ZoneX - win.ZoneFirst.ZoneX) * scaleFactor,
                            ZoneY = (double)(zoneInfo.ZoneY - win.ZoneFirst.ZoneY) * scaleFactor,
                            ZoneWidth = zoneInfo.ZoneWidth * scaleFactor,
                            ZoneHeight = zoneInfo.ZoneHeight * scaleFactor,
                            BorderColor = zoneInfo.BorderColor,
                            Thinkness = zoneInfo.Thinkness
                        };
                        rightScreenMark.Add(rightScreenMarkItem);
                    }
                }
                win.ZoneFirst = null;
                win.ZoneMark = null;

                var saveAsImageType = Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\NiceScreenCapturer", "SaveType", 1));
                CaptureClass.CaptureScreenWithMarktoClipboard(rightScreenInfo.X, rightScreenInfo.Y, rightScreenInfo.Width, rightScreenInfo.Height, rightScreenInfo.StartX, rightScreenInfo.StartY, rightScreenMark, saveAsImageType);

                //clear memory
                rightScreenInfo = null;
                rightScreenMark = null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private void CaptureScreen()
        {
            try
            {
                var currScr = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));
                var scr = AllScreen.First(m => m.DeviceName == currScr.DeviceName);
                var rightScreenInfo = new ScreenInfo();
                var win = new Window1
                {
                    Topmost = true,
                    Left = scr.WorkingArea.Left,
                    CurrentScreen = scr,
                    IsCaptureScreen = true,
                    IsDrawMark = false
                };
                win.SourceInitialized += (snd, arg) => win.WindowState = System.Windows.WindowState.Maximized;
                win.ShowDialog();

                if (win.IsQuitByEscape)
                    return;

                var scaleScreen = win.CurrentScreen;

                ////Screen1
                //CaptureClass.CaptureScreentoClipboard(0, 0, 1920, 1080, 0, scr);
                ////Screen2
                //CaptureClass.CaptureScreentoClipboard(0, 0, 1440, 900, 1920, scr);

                if (scaleScreen.Bounds.X == 0)
                {
                    rightScreenInfo.X = scaleScreen.Bounds.X;
                    rightScreenInfo.Y = scaleScreen.Bounds.Y;
                    rightScreenInfo.Width = scaleScreen.Bounds.Width;
                    rightScreenInfo.Height = scaleScreen.Bounds.Height;
                    rightScreenInfo.StartX = 0;
                    rightScreenInfo.StartY = 0;
                }
                else
                {
                    var scrCurrent = AllScreen.First(m => m.DeviceName == scaleScreen.DeviceName);
                    rightScreenInfo.X = 0;
                    rightScreenInfo.Y = scrCurrent.Bounds.Y;
                    rightScreenInfo.Width = scrCurrent.Bounds.Width;
                    rightScreenInfo.Height = scrCurrent.Bounds.Height;
                    rightScreenInfo.StartX = scrCurrent.Bounds.X;
                    rightScreenInfo.StartY = 0;
                }

                var saveAsImageType = Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\NiceScreenCapturer", "SaveType", 1));
                CaptureClass.CaptureScreentoClipboard(rightScreenInfo.X, rightScreenInfo.Y, rightScreenInfo.Width, rightScreenInfo.Height, rightScreenInfo.StartX, rightScreenInfo.StartY, saveAsImageType);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _notifyIcon.Visible = true;
                //_notifyIcon.ShowBalloonTip(500);
                Hide();
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"FrmSettings_FormClosing: " + ex.Message);
                throw;
            }
        }

        private void FrmSettings_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    if (_notifyIcon != null)
                    {
                        _notifyIcon.Visible = true;
                        _notifyIcon.ShowBalloonTip(500);
                    }
                    Hide();
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    _notifyIcon.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"FrmSettings_Resize: " + ex.Message);
                throw;
            }
        }

        private void CaptureRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CaptureRegion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"CaptureRegionToolStripMenuItem_Click: " + ex.Message);
                throw;
            }
        }

        private void CaptureScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CaptureScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"CaptureScreenToolStripMenuItem_Click: " + ex.Message);
                throw;
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _notifyIcon.Visible = true;
                Show();
                ShowInTaskbar = true;
                WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"SettingsToolStripMenuItem_Click: " + ex.Message);
                throw;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Copyright © 2017 LTH. Email: vutuandat@gmail.com", @"About");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UnregisterHotKey(this.Handle, 1);
                UnregisterHotKey(this.Handle, 2);

                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"ExitToolStripMenuItem_Click: " + ex.Message);
                throw;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                _notifyIcon.Visible = true;
                //_notifyIcon.ShowBalloonTip(500);
                Hide();
                WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"BtnCancel_Click: " + ex.Message);
                throw;
            }
        }
        

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (ChkStartWithWindows.Checked)
                    rk?.SetValue("NiceScreenCapturer", Application.ExecutablePath);
                else
                    rk?.DeleteValue("NiceScreenCapturer", false);

                var saveAsTypeRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\NiceScreenCapturer", true);
                if (saveAsTypeRegistry == null)
                {
                    saveAsTypeRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                    saveAsTypeRegistry?.CreateSubKey("NiceScreenCapturer");
                    saveAsTypeRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\NiceScreenCapturer", true);
                }
                //Draw mark rectangle
                saveAsTypeRegistry?.SetValue("DrawMark", ChkEnableDrawMarkRectangle.Checked ? 1 : 0);

                //Save image as?
                if (RbClipboard.Checked)
                    saveAsTypeRegistry?.SetValue("SaveType", 1);
                if (RbSaveAsImage.Checked)
                    saveAsTypeRegistry?.SetValue("SaveType", 0);

                if (RbEnglish.Checked)
                {
                    saveAsTypeRegistry?.SetValue("Lang", 0);
                    SetLanguage("en");
                }
                if (RbVietnamese.Checked)
                {
                    saveAsTypeRegistry?.SetValue("Lang", 1);
                    SetLanguage("vi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"BtnApply_Click: " + ex.Message);
                throw;
            }

        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (ChkStartWithWindows.Checked)
                    rk?.SetValue("NiceScreenCapturer", Application.ExecutablePath);
                else
                    rk?.DeleteValue("NiceScreenCapturer", false);

                var saveAsTypeRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\NiceScreenCapturer", true);
                if (saveAsTypeRegistry == null)
                {
                    saveAsTypeRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                    saveAsTypeRegistry?.CreateSubKey("NiceScreenCapturer");
                    saveAsTypeRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\NiceScreenCapturer", true);
                }
                //Draw mark rectangle
                saveAsTypeRegistry?.SetValue("DrawMark", ChkEnableDrawMarkRectangle.Checked ? 1 : 0);

                //Save image as?
                if (RbClipboard.Checked)
                    saveAsTypeRegistry?.SetValue("SaveType", 1);
                if (RbSaveAsImage.Checked)
                    saveAsTypeRegistry?.SetValue("SaveType", 0);

                if (RbEnglish.Checked)
                {
                    saveAsTypeRegistry?.SetValue("Lang", 0);
                    SetLanguage("en");
                }
                if (RbVietnamese.Checked)
                {
                    saveAsTypeRegistry?.SetValue("Lang", 1);
                    SetLanguage("vi");
                }

                _notifyIcon.Visible = true;
                //_notifyIcon.ShowBalloonTip(500);
                ShowInTaskbar = false;
                Hide();
                WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"BtnOk_Click: " + ex.Message);
                throw;
            }

        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            try
            {
                //Register hotkeys
                RegisterHotKey(this.Handle, 1, ModControl + ModAlt, (int)Keys.A);
                RegisterHotKey(this.Handle, 2, ModControl + ModAlt, (int)Keys.S);

                var startWithWindowsRegistry = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "NiceScreenCapturer", null);
                if (startWithWindowsRegistry != null)
                    ChkStartWithWindows.Checked = true;

                var enableDrawMarkRectangle = Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\NiceScreenCapturer", "DrawMark", 0));
                ChkEnableDrawMarkRectangle.Checked = enableDrawMarkRectangle == 1;

                var saveAsImageType = Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\NiceScreenCapturer", "SaveType", 1));
                if (saveAsImageType == 1)
                    RbClipboard.Checked = true;
                else
                    RbSaveAsImage.Checked = true;

                var languageType = Convert.ToInt32(Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\NiceScreenCapturer", "Lang", 0));
                if (languageType == 0)
                {
                    RbEnglish.Checked = true;
                    SetLanguage("en");
                }
                else //if (languageType == 1)
                {
                    RbVietnamese.Checked = true;
                    SetLanguage("vi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"FrmSettings_Load: " + ex.Message);
                throw;
            }
        }

        private void SetLanguage(string cultureName)
        {
            try
            {
                _culture = CultureInfo.CreateSpecificCulture(cultureName);
                var rm = new ResourceManager("NiceScreenCapturer.Resources.string", typeof(FrmSettings).Assembly);

                this.Text = rm.GetString("SettingTitle", _culture);

                tabControl1.TabPages[0].Text = rm.GetString("GeneralTab", _culture);
                ChkStartWithWindows.Text = rm.GetString("StartupWithWindows", _culture);
                ChkEnableDrawMarkRectangle.Text = rm.GetString("EnableDrawMark", _culture);

                groupBox1.Text = rm.GetString("StoredImageAs", _culture);
                RbClipboard.Text = rm.GetString("Clipboard", _culture);
                RbSaveAsImage.Text = rm.GetString("SaveAsFile", _culture);
                groupBox2.Text = rm.GetString("InterfaceLanguage", _culture);

                CaptureRegionToolStripMenuItem.Text = rm.GetString("CaptureRegionMenuItem", _culture);
                CaptureScreenToolStripMenuItem.Text = rm.GetString("CaptureScreenMenuItem", _culture);
                SettingsToolStripMenuItem.Text = rm.GetString("SettingMenuItem", _culture);
                AboutToolStripMenuItem.Text = rm.GetString("AboutMenuItem", _culture);
                ExitToolStripMenuItem.Text = rm.GetString("ExitMenuItem", _culture);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }
    }

    public class ScreenInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
    }
}
