using Microsoft.AspNetCore.Http;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Operations.Statics
{
    public static class QrCodeGenerator
    {
        public static (string, string) GenerateQrCode(string text, string path)
        {
            QRCodeGenerator qrGenerator = new();
            var hashedText = Hasher.ComputeSha256Hash(text);
            var qrCodeData = qrGenerator.CreateQrCode(hashedText, QRCodeGenerator.ECCLevel.Q);

            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return (hashedText, SaveQRCodeImage(qrCodeImage, path));
        }
        public static string SaveQRCodeImage(byte[] qrCodeBytes, string path)
        {
            string qrCodePath = $"{path}/{Guid.NewGuid()}.jpg";
            using (MemoryStream ms = new MemoryStream(qrCodeBytes))
            {
                Image image = Image.FromStream(ms);
                image.Save(qrCodePath);
            }
            return qrCodePath;
        }
    }
}
