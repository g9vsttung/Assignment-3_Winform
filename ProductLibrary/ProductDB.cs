using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLibrary {
    public class ProductDB {
        string strConnection;

        public ProductDB() {
            GetConnectionString();
        }

        public string GetConnectionString() {
            strConnection = @"server=SE140240\SQLEXPRESS;database=SaleDB;uid=sa;pwd=123456";
            return strConnection;
        }

        public bool AddNewProduct(Product p) {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);

            string SQL = "INSERT Products(ProductID, ProductName, UnitPrice, Quantity) " +
                "VALUES(@ID, @Name, @Price, @Quantity)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
            

            try { 
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex) {
                throw new Exception(ex.Message);
            }
            finally {
                cnn.Close();
            }
            return false;
        }

        public bool RemoveProduct(Product p) {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "DELETE Products  WHERE ProductID=@ID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException se) {
                throw new Exception(se.Message);
            }
            finally {
                cnn.Close();
            }
            return result;
        }

        public bool UpdateProduct(Product p) {
            bool result;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "UPDATE Products SET ProductName=@Name,UnitPrice=@Price," +
                "Quantity=@Quantity WHERE ProductID=@ID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException se) {
                throw new Exception(se.Message);
            }
            finally {
                cnn.Close();
            }
            return result;
        }

        public Product FindProduct(string name) {
            //string SQL = "SELECT ProductName, UnitPrice, Quantity " +
               // "FROM Products " +
            //    "WHERE ProductID=@ID";
            string SQL = "SELECT ProductID, UnitPrice, Quantity " +
                "FROM Products " +
                "WHERE Productname like @name";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataReader dataReader = null;
            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    if (dataReader.Read()) {
                        int ProductID = int.Parse(dataReader["ProductID"].ToString());
                        float UnitPrice = float.Parse(dataReader["UnitPrice"].ToString());
                        int Quantity = int.Parse(dataReader["Quantity"].ToString());

                        Product product = new Product(ProductID, name, Quantity, UnitPrice);
                        return product;
                    }
                }
            }
            catch (SqlException ex) {
                throw new Exception(ex.Message);
            }
            finally {
                if (dataReader != null) { 
                    dataReader.Close();
                }
                cnn.Close();
            }
            return null;
        }

        public List<Product> GetProductList() {
            List<Product> listProduct = null;
            string SQL = "SELECT ProductID, ProductName, UnitPrice, Quantity " +
                "FROM Products";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataReader dataReader = null;
            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        int ProductID = int.Parse(dataReader["ProductID"].ToString());
                        string ProductName = dataReader["ProductName"].ToString();
                        float UnitPrice = float.Parse(dataReader["UnitPrice"].ToString());
                        int Quantity = int.Parse(dataReader["Quantity"].ToString());

                        Product product = new Product(ProductID, ProductName, Quantity, UnitPrice);
                        if (listProduct == null) {
                            listProduct = new List<Product>();
                        }

                        listProduct.Add(product);
                    }
                }
            }
            catch (SqlException ex) {
                throw new Exception(ex.Message);
            }
            finally {
                if (dataReader != null) {
                    dataReader.Close();
                }
                cnn.Close();
            }
            return listProduct;
        }
    }
}
