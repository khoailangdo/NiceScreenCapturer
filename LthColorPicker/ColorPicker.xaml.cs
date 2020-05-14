using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LthColorPicker
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public enum SourceColorType
        {
            FullColors, LitteColors
        }

        private SourceColorType _fillColor = SourceColorType.LitteColors;
        public SourceColorType FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                if (value == SourceColorType.FullColors)
                {
                    ColorPickerComboBox.ItemsSource = LoadSystemColors();//typeof(Colors).GetProperties();
                }
                else
                {
                    var itemSource = new List<SysColorItem>();
                    var colorItem = new SysColorItem()
                    {
                        Name = "Red",
                        Color = Colors.Red
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "ForestGreen",
                        Color = Colors.ForestGreen
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "Crimson",
                        Color = Colors.Crimson
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "Blue",
                        Color = Colors.Blue
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "Black",
                        Color = Colors.Black
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "Orange",
                        Color = Colors.Orange
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "White",
                        Color = Colors.White
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "Brown",
                        Color = Colors.Brown
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "Aqua",
                        Color = Colors.Aqua
                    };
                    itemSource.Add(colorItem);

                    colorItem = new SysColorItem()
                    {
                        Name = "Pink",
                        Color = Colors.Pink
                    };
                    itemSource.Add(colorItem);
                    ColorPickerComboBox.ItemsSource = itemSource;
                }
            }
        }

        public SysColorItem SelectedColor
        {
            get { return (SysColorItem)ColorPickerComboBox.SelectedItem; }
            set
            {
                foreach (SysColorItem item in ColorPickerComboBox.Items)
                {
                    if (item.Name.Equals(value.Name.Trim()))
                    {
                        ColorPickerComboBox.SelectedItem = item;
                        break;
                    }

                }
            }
        }

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);

        public event ValueChangedEventHandler ValueChanged;

        private void ColorPickerComboBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public ColorPicker()
        {
            InitializeComponent();
        }

        public IList<SysColorItem> LoadSystemColors()
        {
            var sysColorList = new List<SysColorItem>();
            var t = typeof(Colors);
            var propInfo = t.GetProperties();
            foreach (var p in propInfo)
            {
                if (p.PropertyType == typeof(Color))
                {
                    var list = new SysColorItem
                    {
                        Color = (Color)p.GetValue(new Color(), BindingFlags.GetProperty, null, null, null),
                        Name = p.Name
                    };

                    sysColorList.Add(list);
                }
                else if (p.PropertyType == typeof(SolidColorBrush))
                {
                    var list = new SysColorItem
                    {
                        Color = ((SolidColorBrush)p.GetValue(new SolidColorBrush(), BindingFlags.GetProperty, null, null, null)).Color,
                        Name = p.Name
                    };

                    sysColorList.Add(list);
                }
            }
            return sysColorList;
        }

    }
    public class SysColorItem
    {
        public string Name { get; set; }
        public Color Color { get; set; }
    }

}
