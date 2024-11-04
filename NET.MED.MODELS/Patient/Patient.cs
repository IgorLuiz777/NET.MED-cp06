using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.EntityFrameworkCore;

namespace NET.MED.MODELS.Patient;

[Collection("patients")]
public class Patient
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid PatientId { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("age")]
    public int Age { get; set; }
    
    [BsonElement("medHistory")]
    public string MedHistory { get; set; }
    
    [BsonElement("insurance")]
    public bool Insurance { get; set; }
}