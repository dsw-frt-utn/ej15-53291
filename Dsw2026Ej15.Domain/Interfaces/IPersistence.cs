using System;
using System.Collections.Generic;
using System.Text;

using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    // Métodos para Especialidades (para leer el JSON)
    IEnumerable<Speciality> GetAllSpecialities();
    Speciality? GetSpecialityById(Guid id);

    // Métodos para Médicos (los requeridos para cumplir con los 4 endpoints del enunciado)
    void AddDoctor(Doctor doctor);                  // Para el POST
    IEnumerable<Doctor> GetActiveDoctors();         // Para el primer GET
    Doctor? GetActiveDoctorById(Guid id);           // Para el GET por id y el DELETE
    void DeactivateDoctor(Guid id);                 // Para el DELETE (baja lógica)
}