﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration) { }
        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();
            // establish connection to db
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT *
                        FROM Category
                        ORDER BY [Name]";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            categories.Add(category);
                        }
                        return categories;
                    }
                }
            }
        }
    }
}
