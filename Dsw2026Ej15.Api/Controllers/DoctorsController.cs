using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Api.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class DoctorsController: ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    // 1. POST: api/doctors 
    [HttpPost]
    public IActionResult Create([FromBody] DoctorCreateDto dto)
    {
        var speciality = _persistence.GetSpecialityById(dto.SpecialityId);
        if (speciality == null)
        {
            return BadRequest(new { error = "La especialidad especificada no existe." });
        }

        var doctor = new Doctor(dto.Name, dto.LicenseNumber, speciality);
        _persistence.AddDoctor(doctor);

        var response = new DoctorResponseDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        };

        return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, response);
    }

    // 2. GET: api/doctors 
    [HttpGet]
    public IActionResult GetAllActive()
    {
        var doctors = _persistence.GetActiveDoctors()
            .Where(d => d.IsActive)
            .Select(d => new DoctorResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                LicenseNumber = d.LicenseNumber,
                SpecialityName = d.Speciality.Name
            });

        return Ok(doctors);
    }

    // 3. GET: api/doctors/{id} 
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor == null || !doctor.IsActive)
        {
            return NotFound(new { error = "Médico no encontrado o inactivo." });
        }

        var response = new DoctorResponseDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        };

        return Ok(response);
    }

    // 4. DELETE: api/doctors/{id} 
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor == null || !doctor.IsActive)
        {
            return NotFound(new { error = "Médico no encontrado o ya está inactivo." });
        }

        _persistence.DeactivateDoctor(doctor.Id); 
        return  NoContent(); 
    }

}
