using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities;

public abstract class BaseEntity
{
    // El enunciado pide la propiedad Id de tipo Guid
    public Guid Id { get; init; } = Guid.NewGuid();

    protected BaseEntity() { }
}
