﻿
namespace KN_ProyectoWeb.Models
{
    public class Producto
    {


        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public decimal Precio { get; set; }

        public int IdCategoria { get; set; }

        public bool Estado { get; set; }

        public string Imagen { get; set; }




    }
}