using Accord.Imaging;
using Accord.Imaging.Filters;
using System.Drawing;
using System.Drawing.Imaging;
using static Dota2AdInfo.Helpers.Helper;

namespace Dota2AdInfo.SearchHero
{
    internal class HeroSkills
    {
        internal enum Skills : int
        {
            TheSwarm,
            Shukuchi,
            Geminate,
            TimeLapse
        }

        internal static void ActiveSkills(Graphics g)
        {
            var bmpSource = GetBitmap(660, 800, 370, 100);

            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.2154, 0.0721);
            var bmp = grayscaleFilter.Apply(bmpSource);

            SISThreshold SISThresholdFilter2 = new SISThreshold();
            SISThresholdFilter2.ApplyInPlace(bmp);
            bool[] data = new bool[4];
            for (var i = 0; i < 4; i++)
            {
                Crop cropFilter = new Crop(new Rectangle(i * 92 + 20, 10, 92 - 27, bmp.Height - 50));
                var cropImage = cropFilter.Apply(bmp);

                ImageStatistics stat = new ImageStatistics(cropImage);

                var red = stat.Gray;
                data[i] = red.Mean > 50;
            }


            g.DrawString($"{Skills.TheSwarm.ToString()}: {data[(int)Skills.TheSwarm]}", new Font("Console", 10), Brushes.Red, 660, 660);
            g.DrawString($"{Skills.Shukuchi.ToString()}: {data[(int)Skills.Shukuchi]}", new Font("Console", 10), Brushes.Red, 660, 680);
            g.DrawString($"{Skills.Geminate.ToString()}: {data[(int)Skills.Geminate]}", new Font("Console", 10), Brushes.Red, 660, 700);
            g.DrawString($"{Skills.TimeLapse.ToString()}: {data[(int)Skills.TimeLapse]}", new Font("Console", 10), Brushes.Red, 660, 720);
        }
    }
}
