using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    // Listas en memoria RAM que van a sostener los datos vivos mientras la app corra
    private readonly List<Doctor> _doctors = new();
    private readonly List<Speciality> _specialities;

    public PersistenceInMemory()
    {
        // Al nacer la clase, cargamos automáticamente las especialidades del JSON
        _specialities = LoadSpecialities();
    }

    // Método privado exigido por el enunciado para levantar el JSON
    private List<Speciality> LoadSpecialities()
    {
        // AppContext.BaseDirectory asegura encontrar el archivo en la carpeta bin/ de ejecución
        var path = Path.Combine(AppContext.BaseDirectory, "specialities.json");

        if (!File.Exists(path))
            return new List<Speciality>();

        var json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Permite mapear minúsculas del JSON a C# sin romper nada
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

    // Baja lógica (establecer IsActive en false) tal como pide el DELETE del enunciado
    public void DeactivateDoctor(Guid id)
    {
        var doctor = _doctors.FirstOrDefault(d => d.Id == id);
        if (doctor is not null)
        {
            doctor.IsActive = false;
        }
    }
}