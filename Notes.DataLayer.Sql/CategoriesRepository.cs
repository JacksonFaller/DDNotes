using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Notes.Model;

namespace Notes.DataLayer.Sql
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly string _connectionString;
        public CategoriesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Category Create(int userId, string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Categories output inserted.Id values (@userId, @name)";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@name", name);

                    using(var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new IOException("Insert query did not return Id");

                        return new Category()
                        {
                            Name = name,
                            UserId = userId,
                            Id = reader.GetInt32(reader.GetOrdinal("Id"))
                        };
                    }
                }
            }
        }

        public Category Get(int userId, string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select Id from Categories where Name = @name and UserId = @userId";
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Категория с именем: {name} не найдена");

                        return new Category()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = name,
                            UserId = userId
                        };
                    }
                }
            }
        }

        public Category Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select UserId, Name from Categories where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Категория с id: {id} не найдена");

                        return new Category
                        {
                            Id = id,
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                        };
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Categories where Id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Category Update(string name, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "update Categories set Name = @name output inserted.UserId where Id = @id";
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Категория с id: {id} не найдена");

                        return new Category()
                        {
                            Id = id,
                            Name = name,
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                        };
                    }
                }
            }
        }

        public IEnumerable<Category> GetCategories(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select Id, Name from Categories where UserId = @userId";
                    command.Parameters.AddWithValue("@userId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Category()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                UserId = id
                            };
                        }
                    }
                }
            }
        }
    }
}
