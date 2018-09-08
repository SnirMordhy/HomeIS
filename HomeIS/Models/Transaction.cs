using System;

namespace HomeIS.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public int SalerID { get; set; }
        public string PurchaserEmail { get; set; }
        public DateTime TransactionDate { get; set; }
        public int BuyingPrice { get; set; }
        public virtual int ApartmentID { get; set; }

        public virtual ApplicationUser Saler { get; set; }
        public virtual ApplicationUser Purchaser { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}