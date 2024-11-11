﻿using Dominio.Enums;

namespace Dominio
{
    public class Venta : Publicacion
    {
        #region ATRIBUTOS
        public bool OfertaRelampago { get; set; }
        #endregion

        #region CONSTRUCTORES
        public Venta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion, bool ofertaRelampago) : base(nombre, estado, fechaPublicacion)
        {
            OfertaRelampago = ofertaRelampago;
        }

        // Constructor sólo para precargas
        public Venta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion, List<Articulo> articulos, bool ofertaRelampago) : base(nombre, estado, fechaPublicacion, articulos)
        {
            OfertaRelampago = ofertaRelampago;
        }
        #endregion

        #region MÉTODOS Y FUNCIONES
        public void CerrarVenta(Cliente comprador, DateTime fecha)
        {
            if (comprador == null) throw new Exception("Cliente inválido.");
            if (comprador.Saldo < CalcularPrecio()) throw new Exception("Saldo insuficiente.");

            Estado = EstadoPublicacion.CERRADA;
            UsuarioFinalizador = comprador;
            ClienteComprador = comprador;
            comprador.Saldo -= CalcularPrecio();
        }
        #endregion

        #region OVERRIDES
        public override double CalcularPrecio()
        {
            if (OfertaRelampago) return base.CalcularPrecio() * 0.8;
            return base.CalcularPrecio();
        }

        public override bool EsOfertaRelampago()
        {
            return OfertaRelampago;
        }

        public override string EnlaceWeb()
        {
            return $"/Publicaciones/Venta/{Id}";
        }

        public override string IconoWeb()
        {
            return "fa-solid fa-cart-shopping";
        }

        public override string TituloWeb()
        {
            return "Ir a comprar";
        }
        #endregion
    }
}