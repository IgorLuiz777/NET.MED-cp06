using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.EntityFrameworkCore;

namespace NET.MED.MODELS.Doctor;

[Collection("doctors")]
public class Doctor
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid DoctorId { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("specialization")]
    public string Specialization  { get; set; }
    
    [BsonElement("startWorkTime")]
    public string StartWorkTime { get; set; }

    [BsonElement("endWorkTime")]
    public string EndWorkTime { get; set; }
    
    [BsonElement("crm")]
    public string CRM { get; set; }
    
    [BsonElement("hourPrice")]
    public decimal HourPrice { get; set; }
}