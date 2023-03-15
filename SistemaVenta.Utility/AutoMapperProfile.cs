﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.Model;

// esta clase poder definir conversion modelo - dto - modelo (mapear)

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            //mapero para personalizar interaccion de nombres destino y rol de usuario
            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore()
                )

                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion Usuario


            #region Categoria
            CreateMap<Categroria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            //obtener descripcion de categoría para que muestre con la info del prodcuto
            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino =>
                destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )

                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            // Personalizarlo pero invirtiendo las opciones de arriba 

            CreateMap<ProductoDTO, Producto>()
             .ForMember(destino =>
             destino.IdCategoriaNavigation,
             opt => opt.Ignore()
             )

             .ForMember(destino =>
             destino.Precio,
             opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO")))
             )
             .ForMember(destino =>
             destino.EsActivo,
             opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
             );

            #endregion Producto

            #region Venta
            CreateMap<Venta, VentaDTO>()
             .ForMember(destino =>
             destino.TotalTexto,
             opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO")))
             )
             .ForMember(destino =>
             destino.FechaRegistro,
             opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/mm/yy"))
             );

            CreateMap<VentaDTO, Venta>()
             .ForMember(destino =>
             destino.Total,
             opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO")))
             );
            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
                destino.DescripcionProducto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )

                .ForMember(destino =>
                destino.PrecioTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO")))
                );

            CreateMap<DetalleVentaDTO, DetalleVenta>()

             .ForMember(destino =>
             destino.Precio,
             opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-CO")))
             )
             .ForMember(destino =>
             destino.Total,
             opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO")))
             );

            #endregion DetalleVenta

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
             .ForMember(destino =>
             destino.FechaRegistro,
             opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/mm/yy"))
             )
             .ForMember(destino =>
             destino.NumeroDocumento,
             opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
             )

            .ForMember(destino =>
             destino.TipoPago,
             opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
             )
            .ForMember(destino =>
             destino.TotalVenta,
             opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-CO")))
             )

            .ForMember(destino =>
             destino.Producto,
             opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
             )
            .ForMember(destino =>
             destino.Precio,
             opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO")))
             )
             .ForMember(destino =>
             destino.Total,
             opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO")))
             );
            #endregion Reporte
        }
    }
}
