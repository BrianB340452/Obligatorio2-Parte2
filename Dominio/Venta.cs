using Dominio.Enums;

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

        public override string TipoPublicacion()
        {
            return "Venta";
        }

        public override void CerrarPublicacion(Usuario u)
        {
            if (u == null || u.TipoUsuario() != "Cliente") throw new Exception("Cliente inválido.");
            Cliente comprador = (Cliente) u;

            if (comprador.Saldo < CalcularPrecio()) throw new Exception("Saldo insuficiente.");

            // Una vez superadas las validaciones, la compra cambia al estado "Cerrada". 
            // Se registra el administrador que la finaliza, el cliente que realiza la compra 
            // y se descuenta el precio de la compra del saldo del cliente.
            Estado = EstadoPublicacion.CERRADA;
            UsuarioFinalizador = comprador;
            ClienteComprador = comprador;
            FechaFinalizada = DateTime.Now;

            comprador.Saldo -= CalcularPrecio();
        }
        #endregion
    }
}