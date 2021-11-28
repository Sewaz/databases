using System;
using System.Collections.Generic;

public class User
    {
        public long id;
        public string nickname;
        public string fullname;
        public string password;
        public DateTime createdAt;

        public List<Question> questions;

        public User()
        {
            this.id = 0;
            this.nickname = "";
            this.fullname = "";
            this.password = "";
            this.createdAt = default;
        }

        public User(string nickname, string fullname, string password, DateTime createdAt)
        {
            this.nickname = nickname;
            this.fullname = fullname;
            this.password = password;
            this.createdAt = createdAt;
        }

        public override string ToString()
        {
            return $"{id} {nickname} {fullname} {createdAt.ToString()}";
        }

    }