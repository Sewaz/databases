using System;
using System.Collections.Generic;

    public class Question
    {
        public long id;
        public long customerId;
        public string content;
        public DateTime questionDate;
        public User customer;
        public List<Tag> tags;

        public Question()
        {
            this.id = 0;
            this.customerId = 0;
            this.content = "";
            this.questionDate = default;
            this.customer = null;
            this.tags = null;
            tags = new List<Tag>();
        }

        public Question(long customerId, string content, DateTime questionDate, List<Tag> tags)
        {
            this.customerId = customerId;
            this.content = content;
            this.questionDate = questionDate;

        }

        public override string ToString()
        {
            return $"[{id}] {customerId}  ({questionDate.ToString()}) {content}$";
        }
    }