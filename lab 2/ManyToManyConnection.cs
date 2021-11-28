using static System.Console;
using System.Collections.Generic;
using Npgsql;
using System;





public class ManyToManyConnection
    {
        private UserRep userRep;
        private TagRep tagRep;
        private QuestionRep questionRep;

        public ManyToManyConnection(UserRep userRep, TagRep tagRep, QuestionRep questionRep)
        {
            this.userRep = userRep;
            this.tagRep = tagRep;
            this.questionRep = questionRep;
        }

        public void AddQuestion(long userId, string questionInf)
        {
            User user = new User();
            user = userRep.GetUserById(userId);
            if (user == null)
            {
                WriteLine(@"User with id : {userId} dosn`t exist.");
                return;
            }


            // string ouestionInf = "1 3 4";
            string[] questionArr = questionInf.Split(' ');
            if (questionArr.Length < 1)
            {
                WriteLine(@"Incorrect input : {questionInf}.");
                return;
            }

            for (int i = 0; i < questionArr.Length; i++)
            {
                int parsed;
                if (!Int32.TryParse(questionArr[i], out parsed)) { WriteLine($"Incorrect input (tagID should be a number): '{questionInf}'"); return; }
                Tag questionTag = tagRep.GetTagById(long.Parse(questionArr[i]));
                if (questionTag == null) { WriteLine($"Tag with id {questionArr[i]} doesn`t exist. Question Not created."); return; }
            }
            Question newQuestion = new Question();
            newQuestion.customerId = user.id;
            newQuestion.questionDate = DateTime.Now;
            newQuestion.content = "";
            for (int i = 0; i < questionArr.Length; i++)
            {
                Tag questionTag = tagRep.GetTagById(long.Parse(questionArr[i]));
                if (i == 0)
                {
                    newQuestion.id = questionRep.Insert(newQuestion);
                    if (newQuestion.id == 0)
                    {
                        WriteLine(@"Question doesn`t created.");
                        return; ;
                    }

                }
                BuildQuestionTagLink(questionTag.id, newQuestion.id, tagRep, questionRep);
            }

            if (questionRep.Update(newQuestion.id, newQuestion))
            {
                WriteLine(@"Question created.");
            }
            return;
        }

        private void BuildQuestionTagLink(long tagId, long questionId, TagRep tagRep, QuestionRep questionRep)
        {

            Tag tagForAdding = tagRep.GetTagById(tagId);
            if (tagForAdding == null)
            {
                WriteLine($"Tag id '{tagId}' doesn`t exist. Choose another tag. ");
                return;
            }
            if (!questionRep.AddTagConection(tagForAdding.id, questionId))
            {
                WriteLine("Connection NOT created!!!");
                return;
            }
            WriteLine("Connection created.");


        }


    }