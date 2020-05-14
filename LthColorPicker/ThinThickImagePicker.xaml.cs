using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ThinThickImagePicker.xaml
    /// </summary>
    public partial class ThinThickImagePicker : UserControl
    {
        public ThinThickImagePicker()
        {
            InitializeComponent();
        }

        private void ThinThickImagePickerComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageItem
    {
        public string Name { get; set; }
        public Image Image { get; set; }
    }
}
