using System;

namespace WordsTeacher.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public DateTime UpdateTimeUtc { get; set; }
    }
}