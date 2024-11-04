using NET.MED.MODELS.Patient;
using System;
using Xunit;

namespace NET.MED.TESTS
{
    public class PatientTests
    {
        // A - Arrange
        private readonly Patient _patient;

        public PatientTests()
        {
            _patient = new Patient
            {
                PatientId = Guid.NewGuid(),
                Name = "Claudinho",
                Age = 30,
                MedHistory = "Sem historico",
                Insurance = true
            };
        }

        [Fact]
        public void Patient_ShouldHaveValidProperties()
        {
            // A - Assert (Resultado - Verificação)
            Assert.NotNull(_patient);
            Assert.Equal("Claudinho", _patient.Name);
            Assert.Equal(30, _patient.Age);
            Assert.Equal("Sem historico", _patient.MedHistory);
            Assert.True(_patient.Insurance);
        }

        [Fact]
        public void Patient_ShouldHaveUniquePatientId()
        {
            // A - Assert (Resultado - Verificação)
            Assert.NotEqual(Guid.Empty, _patient.PatientId);
        }
    }
}