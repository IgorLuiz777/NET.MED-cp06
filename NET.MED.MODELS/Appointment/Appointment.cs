using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.EntityFrameworkCore;

namespace NET.MED.MODELS.Appointment;

[Collection("appointments")]
public class Appointment
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid AppointmentId { get; set; }
    
    [BsonElement("startDateTime")]
    public string StartDateTime { get; set; }
    
    [BsonElement("endDateTime")]
    public string EndDateTime { get; set; }
    
    [BsonElement("patientId")]
    [BsonRepresentation(BsonType.String)]
    public Guid PatientId { get; set; }

    [BsonElement("doctorId")]
    [BsonRepresentation(BsonType.String)]
    public Guid DoctorId { get; set; }
    
    [BsonIgnoreIfNull]
    public Patient.Patient? Patient { get; set; }
    
    [BsonIgnoreIfNull]
    public Doctor.Doctor? Doctor { get; set; }
}