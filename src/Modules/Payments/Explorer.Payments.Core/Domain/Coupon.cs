using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Explorer.Payments.Core.Domain
{
    public class Coupon : Entity
    {
        public string Code { get; private set; }
        public int Discount { get; private set; }
        public DateTime ExpirationDate { get; set; }
        public int TourId { get; private set; }
        public int TouristId { get; private set; }
        public int AuthorId { get; private set; }

        public Coupon(string code, int discount, DateTime expirationDate, int tourId, int touristId, int authorId) 
        {
            Code = code;
            Discount = discount;
            ExpirationDate = expirationDate;
            TourId = tourId;
            TouristId = touristId;
            AuthorId = authorId;
            Validate();
        }

        public Coupon() { }

        public bool IsExpired()
        {
            return ExpirationDate < DateTime.Now;
        }

        public void GenerateCode()
        {
            /*Random random = new Random();
            char[] randomChars = new char[8];

            for (int i = 0; i < 8; i++)
            {
                randomChars[i] = Convert.ToChar(random.Next(35, 123));
            }*/
            char[] randomChars = new char[8];
            int asciiValue;
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                if (rnd.Next(1, 3) == 1)
                {
                    asciiValue = rnd.Next(48, 58);
                }
                else
                {
                    asciiValue = rnd.Next(65, 91);
                }
                randomChars[i] = Convert.ToChar(asciiValue);
            }

            Code = new string(randomChars);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Code)) throw new ArgumentException("Invalid Code");
            if (Code.Length != 8) throw new ArgumentException("Code must be 8 characters long");
            if (Discount <= 0 || Discount > 100) throw new ArgumentException("Discount must 1-100");
            if (IsExpired()) throw new ArgumentException("Invalid Expiration Date");
        }


    }
}
