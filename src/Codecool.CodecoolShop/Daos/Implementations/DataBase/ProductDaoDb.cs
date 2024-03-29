﻿using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoDb : IProductDao
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;
        private static ProductDaoDb instance = null;
        private ProductDaoDb()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }
        public static ProductDaoDb GetInstance()
        {
            if(instance == null)
            {
                instance = new ProductDaoDb();
            }
            return instance;
        }
        public void Add(Product item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Insert Into product ( name, supplier_id, description, price, category_id, currency ) Values ('{item.Name}', '{item.Supplier.Id}', '{item.Description}'), '{item.DefaultPrice}'), '{item.ProductCategory.Id}'), '{item.Currency}');";
                command.ExecuteNonQuery();
            }
        }

        public Product Get(int id)
        {
            var products = new List<Product>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select *,supplier.ID as 'supplier.ID', supplier.name as 'supplier.name', supplier.description as 'supplier.description', category.ID as 'category.ID', category.name as 'category.name', category.description as 'category.description', category.department as 'category.department' From product " +
                    "JOIN supplier ON product.supplier_id = supplier.ID " +
                    "JOIN category ON product.category_id = category.ID " +
                    $"WHERE product.ID = {id};";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier = new Supplier();
                        supplier.Id = (int)reader["supplier.ID"];
                        supplier.Name = (string)reader["supplier.name"];
                        supplier.Description = (string)reader["supplier.description"];
                        var category = new ProductCategory();
                        category.Id = (int)reader["category.ID"];
                        category.Name = (string)reader["category.name"];
                        category.Description = (string)reader["category.description"];
                        category.Department = (string)reader["category.department"];
                        var product = new Product
                        (
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["currency"],
                            (decimal)reader["price"],
                            category,
                            supplier
                        );
                        product.Id = (int)reader["ID"];
                        products.Add(product);
                    }
                }
            }
            return products[0];
        }

        
        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            var products = new List<Product>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select *,supplier.ID as 'supplier.ID', supplier.name as 'supplier.name', supplier.description as 'supplier.description', category.ID as 'category.ID', category.name as 'category.name', category.description as 'category.description', category.department as 'category.department' From product " +
                    "JOIN supplier ON product.supplier_id = supplier.ID " +
                    "JOIN category ON product.category_id = category.ID " +
                    $"WHERE supplier_id = {supplier.Id};";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier1 = new Supplier();
                        supplier1.Id = (int)reader["supplier.ID"];
                        supplier1.Name = (string)reader["supplier.name"];
                        supplier1.Description = (string)reader["supplier.description"];
                        var category = new ProductCategory();
                        category.Id = (int)reader["category.ID"];
                        category.Name = (string)reader["category.name"];
                        category.Description = (string)reader["category.description"];
                        category.Department = (string)reader["category.department"];
                        var product = new Product
                        (
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["currency"],
                            (decimal)reader["price"],
                            category,
                            supplier1
                        );
                        product.Id = (int)reader["ID"];
                        products.Add(product);

                    }
                }
            }
            return products;
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            var products = new List<Product>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select *,supplier.ID as 'supplier.ID', supplier.name as 'supplier.name', supplier.description as 'supplier.description', category.ID as 'category.ID', category.name as 'category.name', category.description as 'category.description', category.department as 'category.department' From product " +
                    "JOIN supplier ON product.supplier_id = supplier.ID " +
                    "JOIN category ON product.category_id = category.ID " +
                    $"WHERE supplier_id = {productCategory.Id};";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier1 = new Supplier();
                        supplier1.Id = (int)reader["supplier.ID"];
                        supplier1.Name = (string)reader["supplier.name"];
                        supplier1.Description = (string)reader["supplier.description"];
                        var category = new ProductCategory();
                        category.Id = (int)reader["category.ID"];
                        category.Name = (string)reader["category.name"];
                        category.Description = (string)reader["category.description"];
                        category.Department = (string)reader["category.department"];
                        var product = new Product
                        (
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["currency"],
                            (decimal)reader["price"],
                            category,
                            supplier1
                        );
                        product.Id = (int)reader["ID"];
                        products.Add(product);

                    }
                }
            }
            return products;
        }

        public void Remove(int id)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM product WHERE ID = {id}";
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select *,supplier.ID as 'supplier.ID', supplier.name as 'supplier.name', supplier.description as 'supplier.description', category.ID as 'category.ID', category.name as 'category.name', category.description as 'category.description', category.department as 'category.department' From product " +
                    "JOIN supplier ON product.supplier_id = supplier.ID " +
                    "JOIN category ON product.category_id = category.ID; ";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier = new Supplier();
                        supplier.Id = (int)reader["supplier.ID"];
                        supplier.Name = (string)reader["supplier.name"];
                        supplier.Description = (string)reader["supplier.description"];
                        var category = new ProductCategory();
                        category.Id = (int)reader["category.ID"];
                        category.Name = (string)reader["category.name"];
                        category.Description = (string)reader["category.description"];
                        category.Department = (string)reader["category.department"];
                        var product = new Product
                        (
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["currency"],
                            (decimal)reader["price"],
                            category,
                            supplier
                        );
                        product.Id = (int)reader["ID"];
                        products.Add(product);

                    }
                }
            }
            return products;
        }
    }
}
