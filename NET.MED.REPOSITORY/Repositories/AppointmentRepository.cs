using MongoDB.Driver;
using NET.MED.MODELS;
using NET.MED.MODELS.Appointment;

namespace NET.MED.REPOSITORY.Repositories;

public class AppointmentRepository : IRepository<Appointment>
{
    private readonly MongoDbContext _context;

    public AppointmentRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment?> GetById(Guid id)
    {
        return await _context.Appointments.Find(p => p.AppointmentId == id).FirstOrDefaultAsync();
    }

    public async Task<List<Appointment>> GetAll()
    {
        return await _context.Appointments.Find(_ => true).ToListAsync();
    }

    public async Task<Appointment> Add(Appointment appointment)
    {
        await _context.Appointments.InsertOneAsync(appointment);
        return appointment;    }

    public async Task<Appointment> Update(Appointment appointment)
    {
        var filter = Builders<Appointment>.Filter.Eq(p => p.AppointmentId, appointment.AppointmentId);
        await _context.Appointments.ReplaceOneAsync(filter, appointment);
        return appointment;
    }

    public async Task Delete(Guid id)
    {
        await _context.Appointments.DeleteOneAsync(p => p.AppointmentId == id);
    }
}