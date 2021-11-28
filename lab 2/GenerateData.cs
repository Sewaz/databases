using Npgsql;
using System;


    public class GenerateData
    {
        public void GenerateGoods(int number, NpgsqlConnection connection)
        {
            string[] goodsArr = new string[] { "film", "apex_legends", "csgo", "math", "laptop", "studying", "game", "anime" };
            string[] boolean = new string[] { "true", "false" };
            Random random = new Random();
            TagRep tagsRep = new TagRep(connection);
            for (int i = 0; i < number; i++)
            {
                Tag newGood = new Tag();
                newGood.tagName = goodsArr[random.Next(0, goodsArr.Length - 1)];
                newGood.createdAt = DateTime.Now;
                tagsRep.Insert(newGood);
            }
        }

        public void GenerateUsers(int number, NpgsqlConnection connection)
        {
            string[] names = new string[] { "Ira", "Jon", "Mark", "Olia", "Alex", "Dima", "Ruslan", "Artem", "Vera", "Luda", "Maria", "Filip", "Maks", "Kate", "Kristofer", "Ivan", "Vlad", "Diana", "Dasha", "Darina", "Amina", "Arsen", "Lili", "Oleg", "" };
            string[] lastNames = new string[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Hill", "Flores", "Green", "Baker", "Hall" };
            Random random = new Random();
            UserRep usersRep = new UserRep(connection);
            for (int i = 0; i < number; i++)
            {
                User newUser = new User();
                newUser.fullname = names[random.Next(0, names.Length - 1)] + " " + lastNames[random.Next(0, lastNames.Length - 1)];
                newUser.nickname = "customer" + i.ToString();

                newUser.password = "12345";
                newUser.createdAt = DateTime.Now;
                usersRep.Insert(newUser);
            }

        }
        public void GenerateQuestions(int number, NpgsqlConnection connection)
        {

            Random random = new Random();
            UserRep usersRep = new UserRep(connection);
            QuestionRep questionRep = new QuestionRep(connection);
            TagRep tagRep = new TagRep(connection);
            ManyToManyConnection TableManyTomany = new ManyToManyConnection(usersRep, tagRep, questionRep);
            for (int i = 0; i < number; i++)
            {
                long randomUserId = random.Next(1, (int)usersRep.GetCount());
                TableManyTomany.AddQuestion(randomUserId, $"{random.Next(1, (int)tagRep.GetCount())} {random.Next(1, (int)tagRep.GetCount())}");
            }
        }
    }


