using MongoDB.Bson;
using MongoDB.Driver;
using NET.MED.MODELS;
using NET.MED.MODELS.Patient;

namespace NET.MED.REPOSITORY.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly MongoDbContext _context;

        public PatientRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetById(Guid id)
        {
            return await _context.Patients.Find(p => p.PatientId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Patient>> GetAll()
        {
            return await _context.Patients.Find(_ => true).ToListAsync();
        }

        public async Task<Patient> Add(Patient patient)
        {
            await _context.Patients.InsertOneAsync(patient);
            return patient;
        }

        public async Task<Patient> Update(Patient patient)
        {
            var filter = Builders<Patient>.Filter.Eq(p => p.PatientId, patient.PatientId);
            await _context.Patients.ReplaceOneAsync(filter, patient);
            return patient;
        }

        public async Task Delete(Guid id)
        {
            await _context.Patients.DeleteOneAsync(p => p.PatientId == id);
        }
    }
}