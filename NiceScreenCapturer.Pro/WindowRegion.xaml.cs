using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using LthColorPicker;


namespace NiceScreenCapturer
{
    /// <summary>
    /// Interaction logic for WindowRegion.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public bool IsEnableDraw = false;
        public bool IsMouseDown = false;

        public bool IsEnableDrawMark = false;
        public bool IsDrawMarkMouseDown = false;
        public bool IsDrawMark { get; set; }
        public Screen CurrentScreen { get; set; }
        public bool IsCaptureScreen { get; set; }
        public bool CapturedComplete { get; set; }
        public bool IsQuitByEscape { get; set; }
        public ZoneInfo ZoneFirst { get; set; }
        public List<ZoneInfo> ZoneMark { get; set; }
        public ZoneInfo ZoneMarkItem { get; set; }
        public Window1()
        {
            InitializeComponent();
            CapturedComplete = false;
            ZoneFirst = new ZoneInfo {ScaleFactor = 1};
            ZoneMark = new List<ZoneInfo>();
            IsEnableDrawMark = false;
            IsEnableDraw = true;
            IsQuitByEscape = false;
        }

        private void Window_OnContentRendered(object sender, EventArgs e)
        {
            try
            {
                var mainWindowPresentationSource = PresentationSource.FromVisual(this);
                if (mainWindowPresentationSource?.CompositionTarget != null)
                {
                    var m = mainWindowPresentationSource.CompositionTarget.TransformToDevice;
                    ZoneFirst.ScaleFactor = m.M11;
                }

                var scaleScreen = Screen.FromPoint(new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
                CurrentScreen = scaleScreen;
                if (IsCaptureScreen)
                {
                    System.Threading.Thread.Sleep(200);
                    Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Window_OnContentRendered: " + ex.Message);
                throw;
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                {
                    IsQuitByEscape = true;
                    Close();
                }

                if (e.Key == Key.Enter && CapturedComplete)
                    Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Window_KeyDown: " + ex.Message);
                Close();
                throw;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsEnableDrawMark)
                {
                    IsDrawMarkMouseDown = true;
                    ZoneMarkItem = new ZoneInfo
                    {
                        ZoneX = e.GetPosition(null).X,
                        ZoneY = e.GetPosition(null).Y
                    };

                    //If over rectangle first
                    if (ZoneMarkItem.ZoneX < ZoneFirst.ZoneX | ZoneMarkItem.ZoneX > ZoneFirst.ZoneX + ZoneFirst.ZoneWidth | ZoneMarkItem.ZoneY < ZoneFirst.ZoneY | ZoneMarkItem.ZoneY > ZoneFirst.ZoneY + ZoneFirst.ZoneHeight)
                        IsDrawMarkMouseDown = false;
                }

                if (IsEnableDraw)
                {
                    IsMouseDown = true;
                    ZoneFirst.ZoneX = e.GetPosition(null).X;
                    ZoneFirst.ZoneY = e.GetPosition(null).Y;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Window_MouseDown: " + ex.Message);
                throw;
            }
        }

        private bool _isDrawed = false;
        private int _drawIndex = -1;
        private int _rectangleIndex = 0;
        private ColorPicker _colorPicker;
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (IsMouseDown && IsEnableDraw)
                {
                    var curx = e.GetPosition(this).X;
                    var cury = e.GetPosition(this).Y;

                    var r = new System.Windows.Shapes.Rectangle();
                    var brush = new SolidColorBrush(Colors.White) { Opacity = 0.5 };
                    r.Stroke = brush;
                    r.Fill = brush;
                    r.Opacity = 0.5;
                    r.StrokeThickness = 1;
                    r.Width = Math.Abs(curx - ZoneFirst.ZoneX);
                    r.Height = Math.Abs(cury - ZoneFirst.ZoneY);
                    r.MouseEnter += (s, ev) => Mouse.OverrideCursor = System.Windows.Input.Cursors.Cross;
                    r.MouseLeave += (s, ev) => Mouse.OverrideCursor = System.Windows.Input.Cursors.Hand;
                    r.KeyDown += ZoneRectangleKeyDown;

                    Cnv.Children.Clear();
                    Cnv.Children.Add(r);

                    var zoneX = ZoneFirst.ZoneX;
                    var zoneY = ZoneFirst.ZoneY;
                    if (curx > ZoneFirst.ZoneX && cury > ZoneFirst.ZoneY)
                    {
                        Canvas.SetLeft(r, ZoneFirst.ZoneX);
                        Canvas.SetTop(r, ZoneFirst.ZoneY);
                    }

                    if (curx < ZoneFirst.ZoneX && cury < ZoneFirst.ZoneY)
                    {
                        Canvas.SetLeft(r, curx);
                        Canvas.SetTop(r, cury);

                        zoneX = curx;
                        zoneY = cury;
                    }

                    if (curx > ZoneFirst.ZoneX && cury < ZoneFirst.ZoneY)
                    {
                        Canvas.SetLeft(r, ZoneFirst.ZoneX);
                        Canvas.SetTop(r, cury);
                        zoneY = cury;
                    }

                    if (curx < ZoneFirst.ZoneX && cury > ZoneFirst.ZoneY)
                    {
                        Canvas.SetLeft(r, curx);
                        Canvas.SetTop(r, ZoneFirst.ZoneY);
                        zoneX = curx;
                    }

                    if (e.LeftButton == MouseButtonState.Released)
                    {
                        ZoneFirst.ZoneWidth = Math.Abs(e.GetPosition(this).X - ZoneFirst.ZoneX);
                        ZoneFirst.ZoneHeight = Math.Abs(e.GetPosition(this).Y - ZoneFirst.ZoneY);
                        ZoneFirst.ZoneX = zoneX;
                        ZoneFirst.ZoneY = zoneY;
                        IsEnableDraw = false;
                        CapturedComplete = true;

                        if (IsDrawMark)
                        {
                            ////Add button Close
                            var buttonOk = new System.Windows.Controls.Button
                            {
                                Name = "ButtonOk",
                                Width = 70,
                                Height = 30,
                                Content = "OK",
                                CommandParameter = "",
                                Opacity = 1,

                            };
                            buttonOk.MouseEnter += (s, ev) => Mouse.OverrideCursor = System.Windows.Input.Cursors.Hand;
                            buttonOk.MouseLeave += (s, ev) => Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                            buttonOk.PreviewMouseDown += OkButtonClick;
                            buttonOk.KeyDown += ZoneRectangleKeyDown;
                            var peer = new ButtonAutomationPeer(buttonOk);
                            var invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                            invokeProv?.Invoke();

                            Cnv.Children.Add(buttonOk);
                            Canvas.SetLeft(buttonOk, ZoneFirst.ZoneX + ZoneFirst.ZoneWidth - 70);

                            //Setting top position
                            var buttonTop = ZoneFirst.ZoneY + ZoneFirst.ZoneHeight;
                            if (buttonTop * ZoneFirst.ScaleFactor > CurrentScreen.Bounds.Y + CurrentScreen.Bounds.Height)
                                buttonTop = buttonTop - 35;
                            Canvas.SetTop(buttonOk, buttonTop);

                            //Add color picker
                            var item = new SysColorItem
                            {
                                Name = "Red",
                                Color = Colors.Red
                            };
                            _colorPicker = new ColorPicker()
                            {
                                FillColor = ColorPicker.SourceColorType.LitteColors,
                                SelectedColor = item,
                                Width = 110,
                                Height = 30
                            };
                            _colorPicker.MouseEnter += (s, ev) => Mouse.OverrideCursor = System.Windows.Input.Cursors.Hand;
                            _colorPicker.MouseLeave += (s, ev) => Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                            _colorPicker.PreviewMouseDown += ColorPickerClick;
                            _colorPicker.KeyDown += ZoneRectangleKeyDown;
                            Cnv.Children.Add(_colorPicker);
                            Canvas.SetLeft(_colorPicker, ZoneFirst.ZoneX + ZoneFirst.ZoneWidth - 180);
                            Canvas.SetTop(_colorPicker, buttonTop);


                            IsMouseDown = false;

                            //Enable drawing mark
                            IsEnableDrawMark = true;
                        }
                        else
                        {
                            IsMouseDown = false;
                            Cnv.Children.Clear();
                            Close();
                        }
                    }
                }

                if (IsEnableDrawMark && IsDrawMarkMouseDown)
                {
                    var curMarkx = e.GetPosition(this).X;
                    var curMarky = e.GetPosition(this).Y;

                    //If over rectangle first
                    if (curMarkx < ZoneFirst.ZoneX)
                        curMarkx = ZoneFirst.ZoneX;
                    if (curMarkx > ZoneFirst.ZoneX + ZoneFirst.ZoneWidth)
                        curMarkx = ZoneFirst.ZoneX + ZoneFirst.ZoneWidth;
                    if (curMarky < ZoneFirst.ZoneY)
                        curMarky = ZoneFirst.ZoneY;
                    if (curMarky > ZoneFirst.ZoneY + ZoneFirst.ZoneHeight)
                        curMarky = ZoneFirst.ZoneY + ZoneFirst.ZoneHeight;

                    //Convert System.Window.Media.Color to System.Drawing.Color
                    System.Drawing.Color newColor = System.Drawing.Color.FromName(_colorPicker.SelectedColor.Name);
                    ZoneMarkItem.BorderColor = newColor;
                    ZoneMarkItem.Thinkness = 2;

                    var rMark = new System.Windows.Shapes.Rectangle
                    {
                        Stroke = new SolidColorBrush(_colorPicker.SelectedColor.Color),
                        Fill = new SolidColorBrush(Colors.Transparent),
                        Opacity = 1,
                        StrokeThickness = 2,
                        Width = Math.Abs(curMarkx - ZoneMarkItem.ZoneX),
                        Height = Math.Abs(curMarky - ZoneMarkItem.ZoneY)
                    };
                    if (!_isDrawed)
                    {
                        _drawIndex = Cnv.Children.Add(rMark);
                        _isDrawed = true;
                    }
                    else
                    {
                        Cnv.Children.RemoveAt(_drawIndex);
                        _drawIndex = Cnv.Children.Add(rMark);
                    }

                    var zoneMarkX = ZoneMarkItem.ZoneX;
                    var zoneMarkY = ZoneMarkItem.ZoneY;
                    if (curMarkx > ZoneMarkItem.ZoneX && curMarky > ZoneMarkItem.ZoneY)
                    {
                        Canvas.SetLeft(rMark, ZoneMarkItem.ZoneX);
                        Canvas.SetTop(rMark, ZoneMarkItem.ZoneY);
                    }

                    if (curMarkx < ZoneMarkItem.ZoneX && curMarky < ZoneMarkItem.ZoneY)
                    {
                        Canvas.SetLeft(rMark, curMarkx);
                        Canvas.SetTop(rMark, curMarky);

                        zoneMarkX = curMarkx;
                        zoneMarkY = curMarky;
                    }

                    if (curMarkx > ZoneMarkItem.ZoneX && curMarky < ZoneMarkItem.ZoneY)
                    {
                        Canvas.SetLeft(rMark, ZoneMarkItem.ZoneX);
                        Canvas.SetTop(rMark, curMarky);
                        zoneMarkY = curMarky;
                    }

                    if (curMarkx < ZoneMarkItem.ZoneX && curMarky > ZoneMarkItem.ZoneY)
                    {
                        Canvas.SetLeft(rMark, curMarkx);
                        Canvas.SetTop(rMark, ZoneMarkItem.ZoneY);
                        zoneMarkX = curMarkx;
                    }
                    if (e.LeftButton == MouseButtonState.Released)
                    {
                        ZoneMarkItem.ZoneWidth = Math.Abs(e.GetPosition(this).X - ZoneMarkItem.ZoneX);
                        ZoneMarkItem.ZoneHeight = Math.Abs(e.GetPosition(this).Y - ZoneMarkItem.ZoneY);
                        ZoneMarkItem.ZoneX = zoneMarkX;
                        ZoneMarkItem.ZoneY = zoneMarkY;
                        ZoneMark.Add(ZoneMarkItem);

                        IsDrawMarkMouseDown = false;
                        _isDrawed = false;
                        _rectangleIndex += 1;

                        //Enable number of Red Mark Rectangle is 1, if Pro version is 10
                        if (_rectangleIndex > 11)
                            IsEnableDrawMark = false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Window_MouseMove: " + ex.Message);
                throw;
            }

        }

        private void ZoneRectangleKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                IsQuitByEscape = true;
                Close();
            }
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            Cnv.Children.Clear();
            Close();
        }

        private void ColorPickerClick(object sender, RoutedEventArgs e)
        {
            if (_rectangleIndex > 1)
                _rectangleIndex -= 1;
        }
    }

    public class ZoneInfo
    {
        public double ZoneX { get; set; }
        public double ZoneY { get; set; }
        public double ZoneWidth { get; set; }
        public double ZoneHeight { get; set; }
        public double ScaleFactor { get; set; }
        public System.Drawing.Color BorderColor { get; set; }
        public int Thinkness { get; set; }
    }
    
}
