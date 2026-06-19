using System;
using System.Collections.Generic;
using System.Text;

using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Speciality Speciality { get; set; } = null!;

    // Constructor vacío requerido para la deserialización y pruebas
    public Doctor() { }

    // Constructor con las validaciones del enunciado (Name y LicenseNumber requeridos)
    public Doctor(string name, string licenseNumber, Speciality speciality)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("El nombre del médico es requerido.");
        if (string.IsNullOrWhiteSpace(licenseNumber))
            throw new ValidationException("El número de matrícula es requerido.");

        Name = name;
        LicenseNumber = licenseNumber;
        Speciality = speciality;
        IsActive = true; // El enunciado dice: "se crea activo"
    }
}