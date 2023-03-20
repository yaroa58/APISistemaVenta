﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class DashBoardDTO
    {
        public int TotalVentas { get; set; }
        public string? TotalImgresos { get; set;}
        public int TotalProductos { get; set; }
        public List<VentaSemanaDTO> VentasUltimaSemana { get; set; }

    }
}
