using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentRecordManagerAPI.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public double Marks { get; set; }
    }
}