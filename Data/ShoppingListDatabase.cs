using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MotMariusLab7.Models;


namespace MotMariusLab7.Data
{
    public class ShopListDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ShopListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }

        // Operații pentru ShopList
        public Task<int> SaveShopListAsync(ShopList list)
        {
            if (list.ID != 0)
            {
                return _database.UpdateAsync(list);
            }
            else
            {
                return _database.InsertAsync(list);
            }
        }

        public Task<int> DeleteShopListAsync(ShopList list)
        {
            return _database.DeleteAsync(list);
        }

        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }

        // Operații pentru Product
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        // Operații pentru ListProduct
        public Task<int> SaveListProductAsync(ListProduct listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }

        public Task<List<Product>> GetListProductsAsync(int shoplistid)
        {
            return _database.QueryAsync<Product>(
                "select P.ID, P.Description from Product P" +
                " inner join ListProduct LP" +
                " on P.ID = LP.ProductID where LP.ShopListID = ?",
                shoplistid);
        }

        public Task<int> DeleteListProductAsync(int shopListId, int productId)
        {
            return _database.ExecuteAsync(
                "DELETE FROM ListProduct WHERE ShopListID = ? AND ProductID = ?",
                shopListId, productId);
        }
    }
}