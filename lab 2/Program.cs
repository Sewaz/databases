using System;
using static System.Console;
using Npgsql;
using System.Collections.Generic;


namespace lab2bd
{   
    class Program
    {
        static void Main(string[] args)
        {
            Commond();
        }

        public static void Commond()
        {
            NpgsqlConnection connection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=be3dexod; Database=postgres");
            UserRep ur = new UserRep(connection);
            TagRep tr = new TagRep(connection);
            QuestionRep qr = new QuestionRep(connection);
            ManyToManyConnection services = new ManyToManyConnection(ur, tr, qr);

            while (true)
            {
                string table;
                WriteLine("Enter command:");
                string input = ReadLine();
                
                if (input == "add" || input == "get" || input == "delete" || input == "update" || input == "generate" || input == "find")
                {
                    WriteLine("Enter table name: ");
                    string tableName = ReadLine();
                    if (tableName == "users" || tableName == "tags" || tableName == "questions" || tableName == "questions_tags") { WriteLine(tableName); }
                    else { WriteLine("This table was not found!"); }
                    table = tableName;
                    
                    if (input == "add" && table == "users")
                    {
                        WriteLine("write item information like: {nickName} {name}");
                        
                        User user = new User();
                        user.password = "123";
                        user.nickname = "User1";
                        user.fullname = "Boss";
                        long id = ur.Insert(user);
                        WriteLine(id);

                    }
                    
                    else if (input == "add" && table == "tags")
                    {
                        WriteLine("write item information like: {nickName} {name}");
                        Tag tag = new Tag();
                        tag.tagName = "123";
                        long id = tr.Insert(tag);
                        WriteLine(id);

                    }
                    else if (input == "get" && table == "users")
                    {
                        List<User> users = ur.GetAllUsers();
                        foreach (User user in users)
                        {
                            WriteLine(user.ToString());
                        }
                    }

                    else if (input == "get" && table == "questions")
                    {
                        List<Question> questions = qr.GetAllQuestions();
                        foreach (Question question in questions)
                        {
                            WriteLine(question.ToString());
                        }
                    }
                    else if (input == "get" && table == "tags")
                    {
                        List<Tag> tags = tr.GetAllTags();
                        foreach (Tag tag in tags)
                        {
                            WriteLine(tag.ToString());
                        }
                    }
                    else if (input == "delete" && table == "questions")
                    {
                        WriteLine("Enter id: ");
                        int id = int.Parse(ReadLine());
                        if (id <= 0)
                        {
                            WriteLine("Id must be a positive number and > 0");
                        }
                        else
                        {
                            bool isdeleted = qr.DeleteById(id);
                            if (isdeleted)
                            {
                                WriteLine("Question was deleted successfully");
                            }
                            else
                            {
                                WriteLine("Question wasn't deleted");
                            }
                        }
                    }
                    else if (input == "delete" && table == "users")
                    {
                        WriteLine("Enter id: ");
                        int id = int.Parse(ReadLine());
                        if (id <= 0)
                        {
                            WriteLine("Id must be a positive number and > 0");
                        }
                        else
                        {
                            bool isdeleted = ur.DeleteById(id);
                            if (isdeleted)
                            {
                                WriteLine("User was deleted successfully");
                            }
                            else
                            {
                                WriteLine("User wasn't deleted");
                            }
                        }
                    }
                    else if (input == "delete" && table == "tags")
                    {
                        WriteLine("Enter id: ");
                        int id = int.Parse(ReadLine());
                        if (id <= 0)
                        {
                            WriteLine("Id must be a positive number and > 0");
                        }
                        else
                        {
                            bool isdeleted = tr.DeleteById(id);
                            if (isdeleted)
                            {
                                WriteLine("Tag was deleted successfully");
                            }
                            else
                            {
                                WriteLine("Tag wasn't deleted");
                            }
                        }

                    }
                    else if (input == "delete" && table == "questions")
                    {
                        WriteLine("Enter id: ");
                        int id = int.Parse(ReadLine());
                        if (id <= 0)
                        {
                            WriteLine("Id must be a positive number and > 0");
                        }
                        else
                        {
                            bool isdeleted = qr.DeleteById(id);
                            if (isdeleted)
                            {
                                WriteLine("Question was deleted successfully");
                            }
                            else
                            {
                                WriteLine("Question wasn't deleted");
                            }
                        }

                    }
                    else if(input == "find" && table == "tags")
                    {
                        throw new NotImplementedException();
                    }
                    else if (input == "update" && table == "tags")
                    {
                        Tag tag = new Tag();
                        tag.tagName = "321";
                        WriteLine("Enter id: ");
                        int id = int.Parse(ReadLine());
                        if (id <= 0)
                        {
                            WriteLine("Id must be a positive number and > 0");
                        }
                        else
                        {
                            tr.Update(id, tag);
                        }
                    }
                    else if (input == "update" && table == "users")
                    {
                        User user = new User();
                        user.password = "321";
                        user.nickname = "User2";
                        user.fullname = "MrBoss";
                        WriteLine("Enter id: ");
                        int id = int.Parse(ReadLine());
                        if (id <= 0)
                        {
                            WriteLine("Id must be a positive number and > 0");
                        }
                        else
                        {
                            ur.Update(id, user);
                        }
                    }


                    else if (input == "generate")
                    {
                        GenerateData generateData = new GenerateData();
                        WriteLine("Enter the number of values need to be generated");
                        string stringNumber = ReadLine();
                        int numGoods;
                        if (!Int32.TryParse(stringNumber, out numGoods)) { WriteLine($"Incorect items number input(must be int): {input}"); return; }
                        if (table == "tags")
                        {
                            generateData.GenerateGoods(numGoods, connection);
                            WriteLine("Created");
                        }
                        int numUsers;
                        if (!Int32.TryParse(stringNumber, out numUsers)) { WriteLine($"Incorect items number input(must be int): {input}"); return; }
                        if (table == "users")
                        {
                            generateData.GenerateUsers(numUsers, connection);
                            WriteLine("Created");
                        }
                        int numQuestions;

                        if (!Int32.TryParse(stringNumber, out numQuestions)) { WriteLine($"Incorect items number input(must be int): {input}"); return; }
                        if (table == "questions")
                        {
                            generateData.GenerateQuestions(numQuestions, connection);
                            WriteLine("Created");
                        }

                    }

                    else
                    {
                        Error.WriteLine($"Incorrect input: {input}");
                    }
                }
                else if(input == "Exit")
                {
                    break;
                }
                else
                {
                    WriteLine("Incorect input, try again!");
                }
            }
        }
    }
}
