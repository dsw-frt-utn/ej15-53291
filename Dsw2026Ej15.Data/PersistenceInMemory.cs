using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Doctor> _doctors = new();
    private readonly List<Speciality> _specialities;

    public PersistenceInMemory()
    {
        _specialities = LoadSpecialities();
    }

    // Método privado para levantar el JSON
    private List<Speciality> LoadSpecialities()
    {
   
        var path = Path.Combine(AppContext.BaseDirectory, "specialities.json");

        if (!File.Exists(path))
            return new List<Speciality>();

        var json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true 
        };

        return JsonSerializer.Deserialize<List<Speciality>>(json, options)
               ?? new List<Speciality>();
    }

    // ── Métodos de Especialidades ──
    public IEnumerable<Speciality> GetAllSpecialities() => _specialities.ToList();

    public Speciality? GetSpecialityById(Guid id)
        => _specialities.FirstOrDefault(s => s.Id == id);

    // ── Métodos de Médicos ──
    public void AddDoctor(Doctor doctor)
        => _doctors.Add(doctor);

    public IEnumerable<Doctor> GetActiveDoctors()
        => _doctors.Where(d => d.IsActive).ToList();

    public Doctor? GetActiveDoctorById(Guid id)
        => _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);

    public void DeactivateDoctor(Guid id)
    {
        var doctor = _doctors.FirstOrDefault(d => d.Id == id);
        if (doctor is not null)
        {
            doctor.IsActive = false;
        }
    }
}