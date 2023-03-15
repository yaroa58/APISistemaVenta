using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{ // clase para recibir laas credenciales para acceder al sistema
    public class LoginDTO
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
