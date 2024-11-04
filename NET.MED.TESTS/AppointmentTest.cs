using NET.MED.MODELS.Appointment;
using System;
using Xunit;

namespace NET.MED.TESTS
{
    public class AppointmentTest
    {
        // A - Arrange
        private readonly Appointment _appointment;

        public AppointmentTest()
        {
            _appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                StartDateTime = "2024-10-26T10:00:00",
                EndDateTime = "2024-10-26T11:00:00",
                PatientId = Guid.NewGuid(),
                DoctorId = Guid.NewGuid(),
                Patient = null,
                Doctor = null 
            };
        }

        [Fact]
        public void Appointment_ShouldHaveValidProperties()
        {
            // A - Assert (Resultado - Verificação)
            Assert.NotNull(_appointment);
            Assert.Equal("2024-10-26T10:00:00", _appointment.StartDateTime);
            Assert.Equal("2024-10-26T11:00:00", _appointment.EndDateTime);
            Assert.NotEqual(Guid.Empty, _appointment.PatientId);
            Assert.NotEqual(Guid.Empty, _appointment.DoctorId);
        }

        [Fact]
        public void Appointment_ShouldHaveUniqueAppointmentId()
        {
            // A - Assert (Resultado - Verificação)
            Assert.NotEqual(Guid.Empty, _appointment.AppointmentId);
        }
    }
}