using System;
using System.Collections.Generic;

namespace SistemaVenta.Model;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public int? IdRol { get; set; }

    public string? Clave { get; set; }

    public bool? EsActivo { get; set; }

    //estas dos opciones no debería exponerse a angular 
    public DateTime? FechaRegistro { get; set; }

    //nos sirve para relacionar el ususario con el rol
    public virtual Rol? IdRolNavigation { get; set; }
}
