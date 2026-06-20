using System;
using System.Collections.Generic;
using System.Text;

using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    IEnumerable<Speciality> GetAllSpecialities();
    Speciality? GetSpecialityById(Guid id);

 
    void AddDoctor(Doctor doctor);                  
    IEnumerable<Doctor> GetActiveDoctors();         
    Doctor? GetActiveDoctorById(Guid id);           
    void DeactivateDoctor(Guid id);                 
}