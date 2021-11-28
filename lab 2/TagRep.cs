using static System.Console;
using System.Collections.Generic;
using Npgsql;
using System;


public class TagRep
    {
        private NpgsqlConnection connection;
        public TagRep(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public long GetCount()
        {
            connection.Close();
            connection.Open();


            var sql = @"SELECT COUNT(*) FROM tags";
            using var command = new NpgsqlCommand(sql, connection);

            long count = (long)command.ExecuteScalar();
            return count;
        }

        public long Insert(Tag tag)
        {
            connection.Open();

            var sql =
           @"INSERT INTO tags (tagName, createdAt) 
            VALUES (@tagName, @createdAt);";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@tagName", tag.tagName);
            command.Parameters.AddWithValue("@createdAt", DateTime.Now);
            long lastId = command.ExecuteNonQuery();

            connection.Close();

            return lastId;
        }

        public List<Tag> GetAllTags()
        {
            connection.Open();

            var sql = @"SELECT * FROM tags";
            using var command = new NpgsqlCommand(sql, connection);

            NpgsqlDataReader reader = command.ExecuteReader();
            List<Tag> tags = new List<Tag>();
            while (reader.Read())
            {
                Tag tag = GetTag(reader);
                tags.Add(tag);
            }
            reader.Close();
            connection.Close();
            return tags;
        }

        public Tag GetTagById(long id)
        {
            connection.Open();

            var sql = @"SELECT * FROM tags WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = command.ExecuteReader();
            Tag tag = new Tag();
            if (reader.Read())
            {
                tag = GetTag(reader);
            }
            connection.Close();

            return tag;
        }


        public bool DeleteById(long id)
        {
            connection.Open();

            var sql = @"DELETE FROM tags WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            int nChanged = command.ExecuteNonQuery();
            connection.Close();
            if (nChanged == 0)
            {
                return false;
            }

            return true;
        }


        public bool Update(long id, Tag tag)
        {
            connection.Open();

            var sql = @"UPDATE tags SET tagName = @tagName, createdAt = @createdAt WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@tagName", tag.tagName);
            command.Parameters.AddWithValue("@createdAt", tag.createdAt);
            int rowChange = command.ExecuteNonQuery();
            connection.Close();
            if (rowChange == 0)
            {
                return false;
            }

            return true;
        }

        public List<Tag> GetOrderTags(List<long> tagIds)
        {
            List<Tag> tagList = new List<Tag>();
            connection.Open();
            foreach (long id in tagIds)
            {

                var sql = @"SELECT * FROM tags WHERE id = @id";
                using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = command.ExecuteReader();
                Tag tag = new Tag();
                if (reader.Read())
                {
                    tag = GetTag(reader);
                    tagList.Add(tag);
                }
            }
            connection.Close();

            return tagList;
        }
        static Tag GetTag(NpgsqlDataReader reader)
        {
            Tag tag = new Tag();
            tag.id = reader.GetInt32(0);
            tag.tagName = reader.GetString(1);
            return tag;
        }
        



    }