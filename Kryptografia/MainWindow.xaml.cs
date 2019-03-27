using System;
using System.Collections.Generic;
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
using System.Drawing;
using System.Threading;
using Microsoft.Win32;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.IO;



namespace Kryptografia
{

    public partial class MainWindow : Window
    {

        private Bitmap obraz;
        private Bitmap obraz_2;
        private Bitmap obraz_3;
        private ImageSource obraz4;
        private ImageSource obraz5;
        private ImageSource obraz6;
        private int szerokosc_1;
        private int wysokosc_1;
        private string pikselformat_1;
        private string obrazformat_1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Open Image";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jpe; *.bmp; *.png"; ;

            if (ofd.ShowDialog() == true)
            {
                obraz = (Bitmap)System.Drawing.Image.FromFile(ofd.FileName);
                szerokosc_1 = obraz.Width;
                wysokosc_1 = obraz.Height;
                pikselformat_1 = obraz.PixelFormat.ToString();
                obrazformat_1 = obraz.RawFormat.ToString();
                obraz = Binarny(obraz);
                
               obraz4 = ImageSourceForBitmap(obraz);

                ImageInButton.Source = obraz4;
            }
      
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);


        public ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        public double Szerokosc()
        {
            return obraz.Width;
        }

        
        public double Wysokosc()
        {
            return obraz.Height;
        }

        private void Przetorz_Click(object sender, RoutedEventArgs e)
        {
            if (ImageInButton.Source != null)
            {
                obraz_2 = new Bitmap(Convert.ToInt32(2 * Szerokosc()), Convert.ToInt32(Wysokosc()), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                obraz_3 = new Bitmap(Convert.ToInt32(2 * Szerokosc()), Convert.ToInt32(Wysokosc()), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                //  ImageInButton2 = obraz_2;
                Wypelnienie_Wynikowych_21();
            }
            else {
                MessageBoxResult m = MessageBox.Show("Brak obrazu, załaduj", "Alert!");
            }


        }

        public void Wypelnienie_Wynikowych_21(
       )
            {
            int R, k = 0;
            System.Drawing.Color pixel;
            Random rand = new Random();
            for (int i = 0; i < Wysokosc(); i++)
            {
                for (int j = 0; j < Szerokosc(); j++)
                {
                    pixel = obraz.GetPixel(j, i);
                    R = pixel.R;
                    if (R == 255) 
                    {
                        Ustawianie_21(j, i, false, rand.Next(1, 3));
                    }
                    else
                    {
                        Ustawianie_21(j, i, true, rand.Next(1, 3));
                    }
                }
               
                k += Convert.ToInt32(Szerokosc());
            }
            obraz5 = ImageSourceForBitmap(obraz_2);
            obraz6 = ImageSourceForBitmap(obraz_3);
            ImageInButton2.Source = obraz5;
            ImageInButton3.Source = obraz6;

        }
        public void Ustawianie_21(int j, int i, bool czarny, int random)
        {
            if (czarny)
            {
                switch (random)
                {
                    case 1:
                        obraz_2.SetPixel((2 * j), (i), System.Drawing.Color.Black);
                        obraz_2.SetPixel((2 * j) + 1, (i), System.Drawing.Color.White);

                        obraz_3.SetPixel((2 * j), (i), System.Drawing.Color.White);
                        obraz_3.SetPixel((2 * j) + 1, (i), System.Drawing.Color.Black);
                        break;
                    case 2:
                        obraz_2.SetPixel((2 * j), (i), System.Drawing.Color.White);
                        obraz_2.SetPixel((2 * j) + 1, (i), System.Drawing.Color.Black);

                        obraz_3.SetPixel((2 * j), (i), System.Drawing.Color.Black);
                        obraz_3.SetPixel((2 * j) + 1, (i), System.Drawing.Color.White);
                        break;
                }
            }
            else
            {
                switch (random)
                {
                    case 1:
                        obraz_2.SetPixel((2 * j), (i), System.Drawing.Color.Black);
                        obraz_2.SetPixel((2 * j) + 1, (i), System.Drawing.Color.White);

                        obraz_3.SetPixel((2 * j), (i), System.Drawing.Color.Black);
                        obraz_3.SetPixel((2 * j) + 1, (i), System.Drawing.Color.White);
                        break;
                    case 2:
                        obraz_2.SetPixel((2 * j), (i), System.Drawing.Color.White);
                        obraz_2.SetPixel((2 * j) + 1, (i), System.Drawing.Color.Black);

                        obraz_3.SetPixel((2 * j), (i), System.Drawing.Color.White);
                        obraz_3.SetPixel((2 * j) + 1, (i), System.Drawing.Color.Black);
                        break;
                }
            }
        }



        public Bitmap Binarny(Bitmap bmp)
        {
            Bitmap pom = new Bitmap(szerokosc_1, wysokosc_1, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Color pixel;
            int S, R, G, B = 0;
            for (int i = 0; i < wysokosc_1; i++)
            {
                for (int j = 0; j < szerokosc_1; j++)
                {
                    pixel = obraz.GetPixel(j, i);
                    R = pixel.R;
                    G = pixel.G;
                    B = pixel.B;
                    S = (R + G + B) / 3;
                    if (S > 127) 
                        pom.SetPixel(j, i, System.Drawing.Color.White);
                    else
                        pom.SetPixel(j, i, System.Drawing.Color.Black);
                }
            }
            return pom;
        }


        private void ImageButton2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Open Image";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jpe; *.bmp; *.png"; ;

            if (ofd.ShowDialog() == true)
            {
                obraz_2 = (Bitmap)System.Drawing.Image.FromFile(ofd.FileName);
 
             
            //    obraz_2 = Binarny(obraz_2);

                obraz5 = ImageSourceForBitmap(obraz_2);

                ImageInButton2.Source = obraz5;
            }

        }

        private void ImageButton3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Open Image";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jpe; *.bmp; *.png"; ;

            if (ofd.ShowDialog() == true)
            {
                obraz_3 = (Bitmap)System.Drawing.Image.FromFile(ofd.FileName);

              
            //    obraz_3 = Binarny(obraz_3);

                obraz6 = ImageSourceForBitmap(obraz_3);

                ImageInButton3.Source = obraz6;
            }
        }

        private void Przywroc_Click(object sender, RoutedEventArgs e)
        {
            int k = 0;
            if (ImageInButton2.Source != null && ImageInButton3.Source != null && obraz_2.Width==obraz_3.Width && obraz_2.Height == obraz_3.Height)
            {
                Bitmap pom = new Bitmap(obraz_2.Width / 2, obraz_2.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                for (int i = 0; pom.Height > i; i++)
                {
                    for (int j = 0; pom.Width > j; j++)
                    {
                        if (obraz_2.GetPixel((j * 2), (i)) == obraz_3.GetPixel((j * 2), (i)) &&
                            obraz_2.GetPixel((2 * j) + 1, (i)) == obraz_3.GetPixel((j * 2) + 1, (i)))
                            pom.SetPixel(j, i, System.Drawing.Color.White);
                        else
                            pom.SetPixel(j, i, System.Drawing.Color.Black);
                    }

                    k += pom.Width;
                }
                obraz = pom;
                obraz4 = ImageSourceForBitmap(obraz);

                ImageInButton.Source = obraz4;
            }
            else {
                MessageBoxResult m = MessageBox.Show("Do przywracania obrazy w przyciskach nie mogą być puste lub obrazy maja rozne rozmiary", "Alert!");
            }
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".png";
            sfd.Filter = "PNG(*.png)|*.png|BMP(*.bmp)|*.bmp";
            if (sfd.ShowDialog() == true)
            {
                var extension = System.IO.Path.GetExtension(sfd.FileName);
                switch (extension.ToLower())
                {


                    case ".png": obraz_2.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png); break;
                    case ".bmp":
                        obraz_2.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    default: throw new ArgumentOutOfRangeException(extension);
                        
                }

            }
        }

        private void Zapisz2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".png";
            sfd.Filter = "PNG(*.png)|*.png|BMP(*.bmp)|*.bmp";
            if (sfd.ShowDialog() == true)
            {
                var extension = System.IO.Path.GetExtension(sfd.FileName);
                switch (extension.ToLower())
                {


                    case ".png": obraz_3.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png); break;
                    case ".bmp":
                        obraz_3.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    default: throw new ArgumentOutOfRangeException(extension);
                        
                }

            }
        }
    }
}
