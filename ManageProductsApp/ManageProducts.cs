using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManageProductsApp
{
    public record Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
    public class ManageProducts
    {
        string fileName = "ProductList.json";
        List<Product> products = new List<Product>();
        public List<Product> GetProducts()
        {
            GetDataFromFile();
            return products;
        }
        public void StoreToFile()
        {
            try
            {
                //Serialize the object graph into a string
                string jsonData = JsonSerializer.Serialize(products,
                    new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, jsonData);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GetDataFromFile()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string jsonData = File.ReadAllText(fileName);
                    //Deserialize object graph into a List of Product
                    products = JsonSerializer.Deserialize<List<Product>>(jsonData);
                }               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public void InsertProduct (Product product)
        {
            try
            {
                Product p = products.SingleOrDefault(p => p.ProductID == product.ProductID);
                if (p != null)
                {
                    throw new Exception("This product already existed");
                }
                products.Add(product);
                StoreToFile();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdatetProduct (Product product)
        {
            try
            {
                Product p = products.SingleOrDefault(p => p.ProductID == product.ProductID);
                if (p != null)
                {
                    p.ProductName = product.ProductName;
                    StoreToFile();
                } else
                {
                    throw new Exception("This product did not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteProduct (Product product)
        {
            try
            {
                Product p = products.SingleOrDefault(p => p.ProductID == product.ProductID);
                if (p != null)
                {
                    products.Remove(p);
                    StoreToFile();
                }
                else
                {
                    throw new Exception("This product did not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
