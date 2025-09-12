using System;

namespace PortalGalaxy.Entities.Infos;

public class TallerInfo
{
    public int Id { get; set; }
    public string Taller { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public string Instructor { get; set; } = null!;
    public DateOnly Fecha { get; set; }
    public string Situacion { get; set; } = null!;
}
