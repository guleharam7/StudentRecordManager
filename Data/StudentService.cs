using MongoDB.Driver;
using StudentRecordManager.Models;

namespace StudentRecordManager.Data
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IMongoClient client, Microsoft.Extensions.Options.IOptions<MongoDbSettings> settings)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _students = database.GetCollection<Student>("Students");
        }

        public List<Student> GetAll()
        {
            return _students.Find(s => true).ToList();
        }

        public Student GetById(string id)
        {
            return _students.Find(s => s.Id == id).FirstOrDefault();
        }

        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public void Update(string id, Student student)
        {
            _students.ReplaceOne(s => s.Id == id, student);
        }

        public void Delete(string id)
        {
            _students.DeleteOne(s => s.Id == id);
        }
    }
}