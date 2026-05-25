using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentRecordManager.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = "";
        public string RollNumber { get; set; } = "";
        public string Department { get; set; } = "";
        public double Marks { get; set; }
    }
}