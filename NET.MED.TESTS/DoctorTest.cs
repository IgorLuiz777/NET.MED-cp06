using NET.MED.MODELS.Doctor;
using System;
using Xunit;

namespace NET.MED.TESTS
{
    public class DoctorTest
    {
        private readonly Doctor _doctor;

        public DoctorTest()
        {
            _doctor = new Doctor
            {
                DoctorId = Guid.NewGuid(),
                Name = "Robertao",
                Specialization = "Cardiologista",
                StartWorkTime = "08:00:00",
                EndWorkTime = "17:00:00",
                CRM = "123456",
                HourPrice = 30
            };
        }

        [Fact]
        public void Doctor_ShouldHaveValidProperties()
        {
            Assert.NotNull(_doctor);
            Assert.Equal("Robertao", _doctor.Name);
            Assert.Equal("Cardiologista", _doctor.Specialization);
            Assert.Equal("08:00:00", _doctor.StartWorkTime);
            Assert.Equal("17:00:00", _doctor.EndWorkTime);
            Assert.Equal("123456", _doctor.CRM);
            Assert.Equal(30, _doctor.HourPrice);
        }

        [Fact]
        public void Doctor_ShouldHaveUniqueDoctorId()
        {
            Assert.NotEqual(Guid.Empty, _doctor.DoctorId);
        }
        
    }
}