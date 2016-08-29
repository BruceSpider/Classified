using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class ClassifiedRepository : IClassifiedRepository
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["myConString"].ConnectionString;
        }

        public Listing Create(Listing item)
        {
            var query = "INSERT INTO [Classified].[dbo].[Listing] " + " ([ListingTypeId], [CategoryId], [Expiry], [Description], [Price], [Image] ) " + " VALUES " +
                            $"('{item.ListingTypeId}', '{item.CategoryId}', '{item.Expiry}', '{item.Description}', '{item.Price}', '{item.Image}'); SELECT SCOPE_IDENTITY();";

            var conString = GetConnectionString();
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    item.ClassifiedId = Convert.ToInt16(cmd.ExecuteScalar());
                    con.Close();
                }
            }

            return item;
        }
        public IEnumerable<Listing> Retrieve()
        {
            var classified = new List<Listing>();
            var query = "SELECT l.[Id], lt.[ListingType], sc.[SubCategoryName], l.[Expiry], l.[Description], l.[Price], l.[Image] FROM [Classified].[dbo].[Listing] l INNER JOIN[Classified].[dbo].[ListingType] lt ON l.[ListingTypeId] = lt.[Id] INNER JOIN[Classified].[dbo].[Category] c ON l.[CategoryId] = c.[Id] INNER JOIN[Classified].[dbo].[SubCategory] sc ON c.[Id] = sc.[Id]";

            var conString = GetConnectionString();
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var listing = new Listing
                        {
                            ClassifiedId = reader.GetInt32(0),
                            ListingType = reader.GetString(1),
                            Category = reader.GetString(2),
                            Expiry = reader.GetInt32(3),
                            Description = reader.GetString(4),
                            Price = reader.GetDecimal(5)
                            //Image = (reader.GetValue(6) == null ? reader.GetValue(6) : img)
                        };

                        classified.Add(listing);
                    }
                    con.Close();
                }
            }
            return classified.ToArray();
        }
        public bool Update(Listing item)
        {
            var query = "UPDATE [Classified].[dbo].[Listing] " + $" SET [ListingTypeId] = '{item.ListingTypeId}'," + $" [CategoryId] = '{item.CategoryId}', " +
                        $" [Expiry]= '{item.Expiry}', " + $" [Description] = '{item.Description}', " + $" [Price] = '{item.Price}', " + $" [Image] = '{item.Image}' " +
                      $" WHERE Id = '{item.ClassifiedId}'";

            var conString = GetConnectionString();
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return true;
        }
        public bool Delete(int classifiedId)
        {
            var query = "DELETE FROM [Classified].[dbo].[Listing] " + $" WHERE Id = '{classifiedId}'";

            var conString = GetConnectionString();
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return true;
        }
        //
        public Listing GetItemById(int classifiedId)
        {
            Listing item = new Listing();
            var query = "SELECT l.[Id], lt.[ListingType], sc.[SubCategoryName], l.[Expiry], l.[Description], l.[Price], l.[Image] FROM [Classified].[dbo].[Listing] l INNER JOIN[Classified].[dbo].[ListingType] lt ON l.[ListingTypeId] = lt.[Id] INNER JOIN[Classified].[dbo].[Category] c ON l.[CategoryId] = c.[Id] INNER JOIN[Classified].[dbo].[SubCategory] sc ON c.[Id] = sc.[Id]" +
                $" WHERE l.[Id] = '{classifiedId}'";

            var conString = GetConnectionString();
            using (var con = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        item.ClassifiedId = reader.GetInt32(0);
                        item.ListingType = reader.GetString(1);
                        item.Category = reader.GetString(2);
                        item.Expiry = reader.GetInt32(3);
                        item.Description = reader.GetString(4);
                        item.Price = reader.GetDecimal(5);
                    }
                    con.Close();
                }
            }
            return item;
        }

    }
}