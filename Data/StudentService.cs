using MongoDB.Driver;
using StudentRecordManagerAPI.Models;

namespace StudentRecordManagerAPI.Data
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IMongoClient client)
        {
            var database = client.GetDatabase("StudentDB");
            _students = database.GetCollection<Student>("Students");
        }

        // CREATE
        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        // GET ALL
        public List<Student> GetAll()
        {
            return _students.Find(_ => true).ToList();
        }

        // GET BY ID
        public Student GetById(string id)
        {
            return _students.Find(s => s.Id == id).FirstOrDefault();
        }

        // UPDATE
        public void Update(string id, Student student)
        {
            _students.ReplaceOne(s => s.Id == id, student);
        }

        // DELETE
        public void Delete(string id)
        {
            _students.DeleteOne(s => s.Id == id);
        }

        public bool RollNumberExists(string rollNumber)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.RollNumber, rollNumber);
            return _students.Find(filter).Any();
        }

        public bool RollNumberExistsForOther(string rollNumber, string currentId)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.RollNumber, rollNumber) &
                         Builders<Student>.Filter.Ne(s => s.Id, currentId);
            return _students.Find(filter).Any();
        }
    }
}