using MongoDB.Driver;

namespace NET.MED.MODELS
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<Patient.Patient> Patients => _database.GetCollection<Patient.Patient>("patients");
        public IMongoCollection<Doctor.Doctor> Doctors => _database.GetCollection<Doctor.Doctor>("doctors");
        public IMongoCollection<Appointment.Appointment> Appointments => _database.GetCollection<Appointment.Appointment>("appointments");
    }
}