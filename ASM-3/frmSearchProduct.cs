using ProductLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_3 {
    public partial class frmSearchProduct : Form {
        public int ProductID { get; set; }
        public frmSearchProduct() {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            ProductDB productDB = new ProductDB();
            try {
                //int productID = int.Parse(txtProductID.Text);
                Product product = productDB.FindProduct(txtProductID.Text);

                string name = txtProductName.Text;

                if (product != null) {
                    txtProductName.Text = product.ProductName;
                    txtQuantity.Text = product.Quantity.ToString();
                    txtUnitPrice.Text = product.UnitPrice.ToString();
                }
                else {
                    MessageBox.Show("No product found!", "Error");
                }
            }
            catch (FormatException ex) {
                MessageBox.Show("ProductID must be number", "Error");
                throw new Exception(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void frmProductDetail_Load(object sender, EventArgs e) {
            txtProductID.Text = ProductID.ToString();
            btnSearch_Click(sender, e);
        }
    }
}
