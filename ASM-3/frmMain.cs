using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductLibrary;

namespace ASM_3 {
    public partial class frmMain : Form {
        ProductDB productDB = new ProductDB();
        List<Product> listProduct = null;
        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            LoadBook();
        }

        public void LoadBook() {
            listProduct = productDB.GetProductList();
            dgvProduct.DataSource = listProduct;

            //binding data to textbox
            txtProductID.DataBindings.Clear();
            txtProductName.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtUnitPrice.DataBindings.Clear();

            txtProductID.DataBindings.Add("Text", listProduct, "ProductID");
            txtProductName.DataBindings.Add("Text", listProduct, "ProductName");
            txtQuantity.DataBindings.Add("Text", listProduct, "Quantity");
            txtUnitPrice.DataBindings.Add("Text", listProduct, "UnitPrice");
        }

        public bool CheckTextBoxInput(string productName, float unitPrice, int quantity) {
            bool isValid = true;
            if (productName.Length == 0) {
                MessageBox.Show("Product name can't be empty", "Error");
                isValid = false;
            }
            else if (unitPrice < 0) {
                MessageBox.Show("Price can't less than 0", "Error");
                isValid = false;
            }
            else if (quantity < 0) {
                MessageBox.Show("Quantity can't less than 0", "Error");
                isValid = false;
            }
            return isValid;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (btnAdd.Text.Equals("New")) {
                btnAdd.Text = "Add";
                btnCancel.Visible = true;
                //clear binding
                txtProductID.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtQuantity.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();

                txtProductID.ReadOnly = false;
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtQuantity.Text = "";
                txtUnitPrice.Text = "";
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else {
                try {
                    int productID = int.Parse(txtProductID.Text);
                    if (productID > 0) {
                        Product result = productDB.FindProduct(productID);

                        if (result == null) {
                            string productName = txtProductName.Text;
                            float unitPrice = float.Parse(txtUnitPrice.Text);
                            int quantity = int.Parse(txtQuantity.Text);

                            bool isValidInput = CheckTextBoxInput(productName, unitPrice, quantity);
                            if (isValidInput) {
                                Product product = new Product(productID, productName, quantity, unitPrice);
                                productDB.AddNewProduct(product);

                                txtProductID.ReadOnly = true;
                                btnAdd.Text = "New";
                                btnDelete.Enabled = true;
                                btnUpdate.Enabled = true;
                                LoadBook();
                            }
                        }
                        else {
                            MessageBox.Show("Duplicate ProductID", "Error");
                        }
                    }
                    else {
                        MessageBox.Show("ProductID must be positive", "Error");
                    }
                }
                catch (FormatException ex) {
                    MessageBox.Show("ProductID, Quantity and Price must be number", "Error");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            int productID = 0;
            try {
                productID = int.Parse(txtProductID.Text);
            }
            catch (FormatException ex) {
                MessageBox.Show("ProductID, Quantity and Price must be number", "Error");
                throw new Exception(ex.Message);
            }
            if (productID > 0) {
                Product result = productDB.FindProduct(productID);

                if (result != null) {
                    string productName = txtProductName.Text;
                    float unitPrice = float.Parse(txtUnitPrice.Text);
                    int quantity = int.Parse(txtQuantity.Text);

                    Product product = new Product(productID, productName, quantity, unitPrice);
                    productDB.RemoveProduct(product);
                    LoadBook();
                }
                else {
                    MessageBox.Show("No product match ProductID", "Error");
                }
            }
            else {
                MessageBox.Show("ProductID must be positive", "Error");
            }         
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            try {
                int productID = int.Parse(txtProductID.Text);
                if (productID > 0) {
                    Product result = productDB.FindProduct(productID);

                    if (result != null) {
                        string productName = txtProductName.Text;
                        float unitPrice = float.Parse(txtUnitPrice.Text);
                        int quantity = int.Parse(txtQuantity.Text);

                        bool isValidInput = CheckTextBoxInput(productName, unitPrice, quantity);
                        if (isValidInput) {
                            Product product = new Product(productID, productName, quantity, unitPrice);
                            productDB.UpdateProduct(product);
                            LoadBook();
                        }
                    }
                    else {
                        btnAdd_Click(sender, e);
                    }
                }
                else {
                    MessageBox.Show("ProductID must be positive", "Error");
                }
            }
            catch (FormatException ex) {
                throw new Exception(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            frmSearchProduct searchProduct = new frmSearchProduct {
                ProductID = int.Parse(txtProductID.Text)
            };
            searchProduct.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtProductID.ReadOnly = true;
            btnAdd.Text = "New";
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnCancel.Visible = false;
            LoadBook();
        }
    }
}
