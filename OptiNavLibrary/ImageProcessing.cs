using System.Drawing;

namespace OptiNavLibrary
{
    public static class ImageProcessing
    {
        /// <summary>
        /// Loads image file from the disk and returns it as a bitmap.
        /// </summary>
        /// <param name="imagePath">
        /// A path to image file as a string.
        /// </param>
        /// <returns>
        /// Loaded image as a bitmap object.
        /// </returns>
        public static Bitmap LoadImage(string imagePath)
        {
            var loadedImageAsBitmap = new Bitmap(imagePath);
            return loadedImageAsBitmap;
        }

        /// <summary>
        /// Apply greyscale to given bitmap and return a changed bitmap.
        /// </summary>
        /// <param name="originalBitmap">
        /// An image as a bitmap.
        /// </param>
        /// <returns>
        /// Grayscaled bitmap.
        /// </returns>
        public static Bitmap Grayscale(Bitmap originalBitmap)
        {
            const double redRatio = 0.21;
            const double greenRatio = 0.72;
            const double blueRatio = 0.07;

            var clone = new Bitmap(originalBitmap);

            for (var x = 0; x < clone.Width; x++)
            {
                for (var y = 0; y < clone.Height; y++)
                {
                    var originalColor = clone.GetPixel(x, y);
                    var newColor = Color.FromArgb((int) redRatio * originalColor.R, (int) greenRatio * originalColor.G, (int) blueRatio * originalColor.B);
                    clone.SetPixel(x, y, newColor);
                }
            }

            return clone;
        }

        /// <summary>
        /// Saves chosen bitmap to chosen path.
        /// </summary>
        /// <param name="image">
        /// Bitmap to save.
        /// </param>
        /// <param name="imagePath">
        /// Path.
        /// </param>
        public static void SaveImage(Bitmap image, string imagePath)
        {
            image.Save(imagePath);
        }
    }
}