using System;
using System.Collections.Generic;

public class Tag

    {
        public long id;
        public string tagName;
        public DateTime createdAt;
        public List<Question> questions;


        public Tag()
        {
            this.id = 0;
            this.tagName = "";
            this.createdAt = default;
        }

        public Tag(string tagName, DateTime createdAt)
        {
            this.tagName = tagName;
            this.createdAt = createdAt;
        }

        public override string ToString()
        {
            return $"[{this.id}] - {this.tagName}";
        }
    }