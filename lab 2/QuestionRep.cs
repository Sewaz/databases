using static System.Console;
using System.Collections.Generic;
using Npgsql;
using System;



public class QuestionRep
    {
        private NpgsqlConnection connection;
        public QuestionRep(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public long Insert(Question question)
        {
            connection.Open();

            var sql =
             @"INSERT INTO orders (customerId, questionDate, content) 
            VALUES (@customerId, @questionDate, @content);";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@customerId", question.customerId);
            command.Parameters.AddWithValue("@questionDate", question.questionDate);
            command.Parameters.AddWithValue("@content", question.content);

            long lastId =  command.ExecuteNonQuery();
            connection.Close();

            return lastId;
        }
        public bool AddTagConection(long tagId, long questionId)
        {
            connection.Open();
            var sql =
            @"INSERT INTO questions_tags (questionId, tagId) 
            VALUES (@questionId, @tagId);";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@questionId", questionId);
            command.Parameters.AddWithValue("@tagId", tagId);

            long lastId =  command.ExecuteNonQuery();

            connection.Close();
            if (lastId == 0)
            {
                return false;
            }
            return true;
        }

        public List<Question> GetAllQuestions()
        {
            connection.Open();

            var sql = @"SELECT * FROM questions";
            using var command = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<Question> questions = new List<Question>();
            while (reader.Read())
            {
                Question question = GetQuestion(reader);
                questions.Add(question);
            }
            reader.Close();
            connection.Close();
            return questions;
        }
        public long GetCount()
        {
            connection.Open();


            var sql = @"SELECT COUNT(*) FROM tags";
            using var command = new NpgsqlCommand(sql, connection);

            long count = (long)command.ExecuteScalar();
            return count;
        }

        public Question GetQuestionById(long id)
        {
            connection.Open();

            var sql = @"SELECT * FROM questions WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = command.ExecuteReader();
            Question question = new Question();
            if (reader.Read())
            {
                question = GetQuestion(reader);

            }
            connection.Close();
            return question;

        }

        public bool DeleteById(long id)
        {
            connection.Open();

            var sql = @"DELETE FROM questions WHERE id = @id";
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

        private bool DeleteByIdQuestion(long id)
        {
            connection.Open();

            var sql = @"DELETE FROM questions_tags WHERE questionId = @questionId";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            int nChanged = command.ExecuteNonQuery();
            connection.Close();
            if (nChanged == 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteByIdTag(long id, long questionId)
        {
            connection.Open();

            var sql = @"Select FROM questons_tags WHERE tagId = @tagId";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@tagId", id);

            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return this.DeleteById(questionId);

            }
            reader.Close();
            connection.Close();
            return false;

        }
        public bool Update(long id, Question question)
        {
            connection.Open();

            var sql = @"UPDATE questions SET customerId = @customerId, questionDate = @questionDate, content = @content WHERE id = @id";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", question.id);
            command.Parameters.AddWithValue("@customerId", question.customerId);
            command.Parameters.AddWithValue("@questionDate", question.questionDate);
            command.Parameters.AddWithValue("@content", question.content);
            int rowChange = command.ExecuteNonQuery();
            connection.Close();
            if (rowChange == 0)
            {
                return false;
            }

            return true;
        }


        public static Question GetQuestion(NpgsqlDataReader reader)
        {
            Question question = new Question();
            question.id = reader.GetInt32(0);
            question.customerId = reader.GetInt32(1);
            question.questionDate = reader.GetDateTime(2);
            question.content = reader.GetString(3);
            return question;
        }



    }