﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Image = System.Drawing.Image;

namespace Qryptoimage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap bitmap;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Image Files|*.bmp;*.png;*.jpg)"
            };
            
            if (openFileDialog.ShowDialog() == true)
            {
                bitmap = (Bitmap) Image.FromFile(openFileDialog.FileName);
                MainImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void Decrypt(object sender, RoutedEventArgs routedEventArgs)
        {
            if (bitmap != null)
            {
                TextInput.Text = LSB.Decode(bitmap);
            }
            //var text = Crypter.Encrypt("hello", Guid.Parse("7ab9bc21-227c-4207-86bc-32f6e95388fe"));

            //TextInput.Text = Crypter.Decrypt(text, Guid.Parse("7ab9bc21-227c-4207-86bc-32f6e95388fe")); //7ab9bc21-227c-4207-86bc-32f6e95388fe
        }

        private void GenerateKey(object sender, RoutedEventArgs e)
        {
            KeyInput.Text = Guid.NewGuid().ToString();
        }
        
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            LSB.Encode(bitmap, TextInput.Text);
            MainImage.Source = Convert(bitmap);

            return;
            
            
            if (MainImage.Source == null)
            {
                MessageBox.Show("No image selected");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(KeyInput.Text))
            {
                MessageBox.Show("Empty key");
                return;
            }

            if (!Guid.TryParse(KeyInput.Text.Trim(), out var guid))
            {
                MessageBox.Show("Incorrect UUID");
                return;
            }

            // var decryptedText = (BitmapSource) MainImage.Source.Clone();
            // decryptedText.
            //
            // try
            // {
            //     TextInput.Text = Crypter.
            // }
            // catch (Exception exception)
            // {
            //     Console.WriteLine(exception);
            //     throw;
            // }
        }
    }
}