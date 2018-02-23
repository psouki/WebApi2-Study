
using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace BeerDev.Handlers
{
    public class ImageConvertHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            using (Bitmap bmap = new Bitmap(context.Request.PhysicalPath))
            {
                var image = WriteTextOnImage(bmap);

                var isMobile = context.Request.Browser.IsMobileDevice;

                if (isMobile)
                {
                    context.Response.ContentType = "image/gif";
                    image.Save(context.Response.OutputStream, ImageFormat.Gif);
                }
                else
                {
                    context.Response.ContentType = "image/png";
                    image.Save(context.Response.OutputStream, ImageFormat.Png);
                }
            }
        }

        public bool IsReusable => false;

        private static Bitmap WriteTextOnImage(Bitmap image)
        {

            bool useWatermark = Convert.ToBoolean(ConfigurationManager.AppSettings["UseWatermark"]);
            string watermarkText = ConfigurationManager.AppSettings["WatermarkText"] ?? "copyright";

            if (!useWatermark)
                return image;

            Bitmap tempBitmap = new Bitmap(image.Width, image.Height);

            using (Graphics gphx = Graphics.FromImage(tempBitmap))
            {
                gphx.DrawImage(image, 0, 0);

                Font fontWatermark = new Font("Arial", 9, FontStyle.Underline);

                SizeF measuredSize = gphx.MeasureString(watermarkText, fontWatermark);
                Hashtable dimenssions = GetDimessions(image, measuredSize);

                gphx.DrawString(watermarkText, fontWatermark,
                    Brushes.Orange,
                    new Rectangle(Convert.ToInt32(dimenssions["x"]),
                                  Convert.ToInt32(dimenssions["y"]),
                                  image.Width - 10, image.Height - 10));

                return tempBitmap;
            }
        }

        private static Hashtable GetDimessions(Bitmap image, SizeF measuredSize)
        {
            double minX = image.Width - (image.Width * 0.05);
            double minY = image.Height - (image.Height * 0.02);

            double x = minX - measuredSize.Width;
            double y = minY - measuredSize.Height;

            Hashtable result = new Hashtable
            {
                {"x", Convert.ToInt32(Math.Round(x))},
                {"y", Convert.ToInt32(Math.Round(y))}
            };

            return result;
        }
    }
}