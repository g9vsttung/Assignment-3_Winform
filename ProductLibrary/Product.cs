using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLibrary
{
    public class Product {
        public int ProductID { get; set; }
        public string ProductName{ get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        public float SubTotal { get => Quantity * UnitPrice; }

        public Product() {
            ProductID = 1;
            ProductName = "New Product";
            Quantity = 1;
            UnitPrice = 1.0f;
        }

        public Product(int productID, string productName, int quantity, float unitPrice) {
            ProductID = productID;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
