using Microsoft.AspNetCore.Mvc;
using NET.MED.MODELS.Appointment;
using NET.MED.MODELS.Doctor;
using NET.MED.MODELS.Patient;
using NET.MED.REPOSITORY.Repositories;

namespace NET.MED.API.Controller;

[Route("appointment")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly DoctorRepository _doctorRepository;
    private readonly PatientRepository _patientRepository;

    public AppointmentController(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository, PatientRepository patientRepository)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        try
        {
            var appointments = await _appointmentRepository.GetAll();
            return Ok(appointments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
    {
        try
        {
            var appointment = await _appointmentRepository.GetById(id);
            if (appointment == null) return NotFound("Appointment not found id:" + id);
            return Ok(appointment);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Appointment>> PostAppointment([FromBody] Appointment appointment)
    {
        try
        {
            var doctor = await _doctorRepository.GetById(appointment.DoctorId);
            var patient = await _patientRepository.GetById(appointment.PatientId);
        
            if (doctor == null || patient == null)
            {
                return NotFound("Doctor or Patient not found.");
            }
        
            appointment.StartDateTime = TimeSpan.ParseExact(appointment.StartDateTime, "hh\\:mm", null).ToString();
            appointment.EndDateTime = TimeSpan.ParseExact(appointment.EndDateTime, "hh\\:mm", null).ToString();

            appointment.Doctor = doctor;
            appointment.Patient = patient;

            await _appointmentRepository.Add(appointment);

            return Ok(appointment);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Appointment>> PutAppointment(Guid id, Appointment appointment)
    {
        try
        {
            var existingAppointment = await _appointmentRepository.GetById(id);;
            if (existingAppointment == null) return NotFound("Appointment not found id:" + id);
            appointment.StartDateTime = TimeSpan.ParseExact(appointment.StartDateTime, "hh\\:mm", null).ToString();
            appointment.EndDateTime = TimeSpan.ParseExact(appointment.EndDateTime, "hh\\:mm", null).ToString();
            existingAppointment.StartDateTime = appointment.StartDateTime;
            existingAppointment.EndDateTime = appointment.EndDateTime;
            existingAppointment.PatientId = appointment.PatientId;
            existingAppointment.DoctorId = appointment.DoctorId;
            await _appointmentRepository.Update(existingAppointment);
            return Ok(appointment);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Appointment>> DeleteAppointment(Guid id)
    {
        try
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null) return NotFound("Doctor not found id:" + id);
            await _appointmentRepository.Delete(id);
            return NoContent();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}