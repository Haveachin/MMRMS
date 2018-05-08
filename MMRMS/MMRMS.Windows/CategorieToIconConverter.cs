﻿using MMRMS.Windows.Models.Categories.Interfaces;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MMRMS.Windows
{
    /// <summary>
    /// Converts a path or specific name in an image type
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class CategorieToIconConverter : IValueConverter
    {

        #region Properties

        /// <summary>
        /// This exists for easy use in xaml (e.g. MainWindow.xaml)
        /// </summary>
        public static CategorieToIconConverter Instance = new CategorieToIconConverter();

        #endregion

        #region Converting

        /// <summary>
        /// Converts an object in an image path
        /// </summary>
        /// <param name="value">In this case a predefined string</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>The coresponding image</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = null;

            if (value is string)
            {
                switch ((string)value)
                {
                    case "block": path = "Images/block_grass.png"; break;
                    case "item": path = "Images/sword_diamond.png"; break;
                    default: return null;
                }

                path = $"pack://application:,,,/{path}";
            }
            else if (value is IHasTexture)
            {
                path = ((IHasTexture)value).PathTexture;
            }

            if (string.IsNullOrEmpty(path))
                return null;

            return new BitmapImage(new Uri(path));
        }

        /// <summary>
        /// Not implemented but needed for IValueConverter
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
