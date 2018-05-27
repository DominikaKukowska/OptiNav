using System;
using System.ComponentModel;
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

        private string _grayscaled_fileName;

        private long _grayscaled_time_elapsed;

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
            RunGrayscale();
            GrayscaledImage.Source = new BitmapImage(new Uri(_grayscaled_fileName));
            TextBlock.Text = $"Grayscale processing time: {_grayscaled_time_elapsed} ms";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (o, args) => RunGrayscale();
            backgroundWorker.RunWorkerCompleted += (o, args) =>
            {
                GrayscaledImage.Source = new BitmapImage(new Uri(_grayscaled_fileName));
                TextBlock.Text = $"Grayscale processing time: {_grayscaled_time_elapsed} ms";
            };
            backgroundWorker.RunWorkerAsync();
        }

        private void RunGrayscale()
        {
            var originalBitmap = ImageProcessing.LoadImage(_fileName);
            var stopwatch = Stopwatch.StartNew();
            var newBitmap = ImageProcessing.Grayscale(originalBitmap);
            stopwatch.Stop();
            _grayscaled_fileName = _fileName + "temp";
            _grayscaled_time_elapsed = stopwatch.ElapsedMilliseconds;
            ImageProcessing.SaveImage(newBitmap, _grayscaled_fileName);
        }
    }
}