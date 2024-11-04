using Microsoft.AspNetCore.Mvc;
using NET.MED.MODELS.Doctor;
using NET.MED.REPOSITORY.Repositories;

namespace NET.MED.API.Controller;

[Route("doctor")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly DoctorRepository _doctorRepository;

    public DoctorController(DoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
    {
        try
        {
            var doctors = await _doctorRepository.GetAll();
            return Ok(doctors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(Guid id)
    {
        try
        {
            var doctor = await _doctorRepository.GetById(id);
            if (doctor == null) return NotFound("Doctor not found id:" + id);
            return Ok(doctor);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
    {
        try
        {
            doctor.StartWorkTime = TimeSpan.ParseExact(doctor.StartWorkTime, "hh\\:mm", null).ToString();
            doctor.EndWorkTime = TimeSpan.ParseExact(doctor.EndWorkTime, "hh\\:mm", null).ToString();

            await _doctorRepository.Add(doctor);
            return Ok(doctor);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Doctor>> PutDoctor(Guid id, Doctor doctor)
    {
        try
        {
            var existingDoctor = await _doctorRepository.GetById(id);;
            if (existingDoctor == null) return NotFound("Doctor not found id:" + id);
            existingDoctor.Name = doctor.Name;
            existingDoctor.Specialization = doctor.Specialization;
            doctor.StartWorkTime = TimeSpan.ParseExact(doctor.StartWorkTime, "hh\\:mm", null).ToString();
            doctor.EndWorkTime = TimeSpan.ParseExact(doctor.EndWorkTime, "hh\\:mm", null).ToString();
            existingDoctor.StartWorkTime = doctor.StartWorkTime;
            existingDoctor.EndWorkTime = doctor.EndWorkTime;
            existingDoctor.CRM = doctor.CRM;
            existingDoctor.HourPrice = doctor.HourPrice;
            await _doctorRepository.Update(existingDoctor);
            return Ok(doctor);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Doctor>> DeleteDoctor(Guid id)
    {
        try
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null) return NotFound("Doctor not found id:" + id);
            await _doctorRepository.Delete(id);
            return NoContent();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}