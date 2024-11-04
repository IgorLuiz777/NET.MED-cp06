using Microsoft.AspNetCore.Mvc;
using NET.MED.MODELS.Patient;
using NET.MED.REPOSITORY.Repositories;

namespace NET.MED.API.Controller;

[Route("patient")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly PatientRepository _patientRepository;

    public PatientController(PatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    {
        try
        {
            var patients = await _patientRepository.GetAll();
            return Ok(patients);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(Guid id)
    {
        try
        {
            var patient = await _patientRepository.GetById(id);
            if (patient == null) return NotFound("Patient not found with id: " + id);
            return Ok(patient);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

    }

    [HttpPost]
    public async Task<ActionResult> PostPatient(Patient patient)
    {
        try
        {
            await _patientRepository.Add(patient);
            return Ok(patient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Patient>> PutPatient(Guid id, Patient patient)
    {
        try
        {
            var existingPatient = await _patientRepository.GetById(id);
            if (existingPatient == null) return NotFound("Patient not found with id: " + id);
            existingPatient.Name = patient.Name;
            existingPatient.Age = patient.Age;
            existingPatient.Insurance = patient.Insurance;
            existingPatient.MedHistory = patient.MedHistory;
            await _patientRepository.Update(existingPatient);
            return Ok(existingPatient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePatient(Guid id)
    {
        try
        {
            var patient = await _patientRepository.GetById(id);
            if (patient == null) return NotFound("Patient not found with id: " + id);
            await _patientRepository.Delete(id);
            return NoContent();
        }   
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
