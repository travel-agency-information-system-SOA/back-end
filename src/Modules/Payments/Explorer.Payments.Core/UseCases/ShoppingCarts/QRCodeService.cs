using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Core.UseCases.ShoppingCarts
{
    public class QRCodeService
    {
        //private readonly EmailSenderService _emailSenderService;
        //private readonly AccountManagementService _accountManagementService;

        public QRCodeService()
        {
            // _emailSenderService = emailSenderService;
            
        }

        public void SendReceiptViaEmail(ShoppingCart shoppingCart)
        {
            string email;
            int userID = shoppingCart.TouristId;
            if(userID == 300)
            {
                email = "lukazelovic@gmail.com";
            }
            else
            {
                email = "spasoje.brboric@gmail.com";
            }
            string cartString = GenerateShoppingCartInfoString(shoppingCart);
            byte[] qrCode = GenerateQRCodeBytes(cartString);
            SendQRCodeByEmail(email, cartString, "Tour confirmation", "You have successfully purchased a spot on the tour."); 
        }

        public void SendQRCodeByEmail(string email, string qrCodeContent, string subject, string body)
        {
            try
            {
                // Generate QR code as byte array
                byte[] qrCodeBytes = GenerateQRCodeBytes(qrCodeContent);
                var _emailSenderService = new EmailSenderService();
                // Send email with QR code attachment
                _emailSenderService.SendEmailWithAttachment(email, subject, body, qrCodeBytes, "qrCodeImage.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending QR code email: {ex.Message}");
            }
        }

        private byte[] GenerateQRCodeBytes(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }





        public string GenerateShoppingCartInfoString(ShoppingCart shoppingCart)
        {
            double totalPrice = 0;
            if (shoppingCart == null)
            {
                throw new ArgumentNullException(nameof(shoppingCart));
            }

            StringBuilder infoStringBuilder = new StringBuilder();

            infoStringBuilder.AppendLine($"Tourist ID: {shoppingCart.TouristId}");
            
            infoStringBuilder.AppendLine($"Order Items:");

            foreach (var orderItem in shoppingCart.OrderItems)
            {
                infoStringBuilder.AppendLine($"- Tour: {orderItem.TourName}, Price: {orderItem.Price}, IdTour: {orderItem.IdTour}");
                totalPrice += orderItem.Price;
            }

            infoStringBuilder.AppendLine($"Total: {totalPrice}");

            return infoStringBuilder.ToString();
        }
    }
}
