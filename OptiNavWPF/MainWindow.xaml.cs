using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using OptiNavLibrary;

namespace OptiNavWPF
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _fileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Title = "Select a picture to open.",
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg; *.jpeg; *.bmp; *.png",
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openDialog.ShowDialog() ?? false)
            {
                _fileName = openDialog.FileName;
                OriginalImage.Source = new BitmapImage(new Uri(_fileName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var originalBitmap = ImageProcessing.LoadImage(_fileName);
            var stopwatch = Stopwatch.StartNew();
            var newBitmap = ImageProcessing.Grayscale(originalBitmap);
            stopwatch.Stop();
            var newFileName = _fileName + "temp";
            ImageProcessing.SaveImage(newBitmap, newFileName);
            GrayscaledImage.Source = new BitmapImage(new Uri(newFileName));
            TextBlock.Text = $"Grayscale processing time: {stopwatch.ElapsedMilliseconds} ms";
        }
    }
}