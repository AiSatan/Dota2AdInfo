using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Statistics.Visualizations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2AdInfo.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var bmpSource = LoadBitmap("test4.jpg");
            Grayscale filter1 = new Grayscale(0.2125, 0.2154, 0.0721);
            // apply the filter
            Bitmap bmp = filter1.Apply(bmpSource);
            // create filter
            SISThreshold filter2 = new SISThreshold();
            filter2.ApplyInPlace(bmp);

            for (var i = 0; i < 4; i++)
            {
                Crop filter3 = new Crop(new Rectangle(i * 92+20, 10, 92-27, bmp.Height-50));
                // apply the filter
                Bitmap newImage = filter3.Apply(bmp);
                ImageStatistics stat = new ImageStatistics(newImage);
                // get red channel's histogram
                Histogram red = stat.Gray;
                // check mean value of red channel
                if (red.Mean > 50)
                {
                    Console.WriteLine($"exist: {i}");
                }
                else
                {
                    Console.WriteLine($"not exist: {red.Mean}");
                }
                newImage.Save($"result4_{i}.jpg");
            }
            Console.ReadKey();
        }


        static Bitmap LoadBitmap(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return new Bitmap(fs);
            }
        }
    }
}
