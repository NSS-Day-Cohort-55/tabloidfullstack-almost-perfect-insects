using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;
using System;

namespace Tabloid.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(IConfiguration configuration) : base(configuration) { }
        public List<Post> GetAllPosts()
        {
            var posts = new List<Post>();
            // establish connection to db
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // create sql query to get all posts
                    cmd.CommandText = @"SELECT 
                                            p.*, 
	                                        up.DisplayName, 
	                                        up.FirstName,
	                                        up.LastName,
	                                        up.Email,
	                                        up.CreateDateTime AS UserCreateDateTime, 
	                                        up.ImageLocation AS  UserImageLocation,
	                                        up.UserTypeId,
	                                        c.Name AS CategoryName
                                        FROM Post p
                                        JOIN UserProfile up ON up.Id = p.UserProfileId
                                        JOIN Category c ON c.Id = p.CategoryId
                                        WHERE p.IsApproved = 1 AND p.PublishDateTime IS NOT NULL
                                        AND p.PublishDateTime < SYSDATETIME()
                                        ORDER BY p.PublishDateTime DESC
                                        ";
                    // execute query
                    using (var reader = cmd.ExecuteReader())
                    {
                        // parse data - use while loop
                        while (reader.Read())
                        {
                            // add post to list with each iteration
                            posts.Add(NewPostFromReader(reader));
                        }
                    }

                }
            }
            return posts;
        }

        public Post GetById(int id)
        {
            Post post = null;
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        select 
	                                        p.*,
	                                        c.Name AS CategoryName, 
	                                        up.DisplayName,
	                                        up.FirstName,
	                                        up.LastName,
	                                        up.Email,
	                                        up.ImageLocation AS UserImageLocation,
	                                        up.CreateDateTime AS UserCreateDateTime,
	                                        up.UserTypeId
                                        FROM Post p
                                        JOIN UserProfile up ON up.Id = p.UserProfileId
                                        JOIN Category c ON c.Id = p.CategoryId
                                        WHERE p.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            post = NewPostFromReader(reader);
                        }
                    }
                }
            }
            return post;
        }

        public void AddPost(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Post
                        (Title, Content, ImageLocation, CreateDateTime, PublishDateTime, IsApproved, UserProfileId, CategoryId)
                        OUTPUT INSERTED.Id
                        VALUES (@title, @content, @imageLocation, SYSDATETIME(), @publishDateTime, @isApproved, @userProfileId, @categoryId)";
                    DbUtils.AddParameter(cmd, "@id", post.Id);
                    DbUtils.AddParameter(cmd, "@title", post.Title);
                    DbUtils.AddParameter(cmd, "@content", post.Content);
                    DbUtils.AddParameter(cmd, "@imageLocation", post.ImageLocation);
                    DbUtils.AddParameter(cmd, "@publishDateTime", post.PublishDateTime);
                    DbUtils.AddParameter(cmd, "@isApproved", post.IsApproved);
                    DbUtils.AddParameter(cmd, "@userProfileId", post.UserProfileId);
                    DbUtils.AddParameter(cmd, "@categoryId", post.CategoryId);

                    post.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        private Post NewPostFromReader(SqlDataReader reader)
        {
            return new Post
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Title = DbUtils.GetString(reader, "Title"),
                Content = DbUtils.GetString(reader, "Content"),
                ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                PublishDateTime = DbUtils.GetNullableDateTime(reader, "PublishDateTime"),
                IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),

                UserProfile = new UserProfile
                {
                    DisplayName = DbUtils.GetString(reader, "DisplayName"),
                    FirstName = DbUtils.GetString(reader, "FirstName"),
                    LastName = DbUtils.GetString(reader, "LastName"),
                    Email = DbUtils.GetString(reader, "Email"),
                    CreateDateTime = DbUtils.GetDateTime(reader, "UserCreateDateTime"),
                    ImageLocation = DbUtils.GetString(reader, "UserImageLocation"),
                    UserTypeId = DbUtils.GetInt(reader, "UserTypeId")

                },

                Category = new Category
                {
                    Name = DbUtils.GetString(reader, "CategoryName")
                }
            };
        }
    }
}

