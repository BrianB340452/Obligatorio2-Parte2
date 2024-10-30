using Dominio.Enums;

namespace Dominio
{
    public class Venta : Publicacion
    {
        private bool _ofertaRelampago;

        public Venta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion, bool ofertaRelampago) : base(nombre, estado, fechaPublicacion)
        {
            _ofertaRelampago = ofertaRelampago;
        }

        // Constructor sólo para precargas
        public Venta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion, List<Articulo> articulos, bool ofertaRelampago) : base(nombre, estado, fechaPublicacion, articulos)
        {
            _ofertaRelampago = ofertaRelampago;
        }

        public override double CalcularPrecio()
        {
            if (_ofertaRelampago) return base.CalcularPrecio() * 0.8;
            return base.CalcularPrecio();
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
    }
}
