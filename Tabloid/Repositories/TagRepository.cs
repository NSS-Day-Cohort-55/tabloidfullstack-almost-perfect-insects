using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;
using System;

namespace Tabloid.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration configuration) : base(configuration) { }
        public List<Tag> GetAllTags()
        {
            var tags = new List<Tag>();
            // establish connection to db
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // create sql query to get all tags
                    cmd.CommandText = @"
                                        SELECT *
                                        FROM Tag
                                        ORDER BY Name ASC";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tag tag = new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            tags.Add(tag);
                        }
                        return tags;
                    }
                }
            }
        }
    }
}

                
                   