using MongoDB.Driver;
using NET.MED.MODELS;
using NET.MED.MODELS.Doctor;

namespace NET.MED.REPOSITORY.Repositories;

public class DoctorRepository : IRepository<Doctor>
{
    private readonly MongoDbContext _context;

    public DoctorRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> GetById(Guid id)
    {
        return await _context.Doctors.Find(p => p.DoctorId == id).FirstOrDefaultAsync();
    }

    public async Task<List<Doctor>> GetAll()
    {
        return await _context.Doctors.Find(_ => true).ToListAsync();
    }

    public async Task<Doctor> Add(Doctor doctor)
    {
        await _context.Doctors.InsertOneAsync(doctor);
        return doctor;
    }

    public async Task<Doctor> Update(Doctor doctor)
    {
        var filter = Builders<Doctor>.Filter.Eq(p => p.DoctorId, doctor.DoctorId);
        await _context.Doctors.ReplaceOneAsync(filter, doctor);
        return doctor;
    }

    public async Task Delete(Guid id)
    {
        await _context.Doctors.DeleteOneAsync(p => p.DoctorId == id);
    }
}