using Dominio.Enums;

namespace Dominio
{
    public class Subasta : Publicacion
    {
        private List<Oferta> _ofertas = new List<Oferta>();

        public Subasta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion) : base(nombre, estado, fechaPublicacion)
        {
        }

        // Constructor sólo para precargas
        public Subasta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion, List<Articulo> articulos, List<Oferta> ofertas) : base(nombre, estado, fechaPublicacion, articulos)
        {
            _ofertas = ofertas;
        }

        public void AgregarOferta(Oferta oferta)
        {
            if (oferta == null) throw new Exception("La oferta no puede ser nula.");

            Cliente ultimoClienteEnOfertar = UltimoClienteEnOfertar();
            if (ultimoClienteEnOfertar != null && ultimoClienteEnOfertar.Id == oferta.Cliente.Id) throw new Exception("Ya tiene la oferta más alta.");

            oferta.Validar();

            foreach (Oferta o in _ofertas)
            {
                if (o.Monto >= oferta.Monto) throw new Exception("La oferta debe ser mayor a la última oferta añadida");
            }
            
            _ofertas.Add(oferta);
        }

        private Cliente UltimoClienteEnOfertar()
        {
            return _ofertas.Last().Cliente;
        }

        public override double PrecioFinal()
        {
            if (_ofertas != null && _ofertas.Count != 0)
            {
                return _ofertas.Last().Monto;
            }

            return base.PrecioFinal();
        }
    }
}