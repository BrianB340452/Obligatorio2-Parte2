using Dominio.Enums;

namespace Dominio
{
    public class Subasta : Publicacion, IComparable<Subasta>
    {
        #region ATRIBUTOS
        private List<Oferta> _ofertas = new List<Oferta>();
        #endregion

        #region CONSTRUCTORES
        public Subasta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion) : base(nombre, estado, fechaPublicacion) {}

        // Constructor sólo para precargas
        public Subasta(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion, List<Articulo> articulos, List<Oferta> ofertas) : base(nombre, estado, fechaPublicacion, articulos)
        {
            _ofertas = ofertas;
        }
        #endregion

        #region MÉTODOS Y FUNCIONES
        public void AgregarOferta(Oferta oferta)
        {
            if (oferta == null) throw new Exception("La oferta no puede ser nula.");

            Cliente? ultimoClienteEnOfertar = UltimoClienteEnOfertar();

            if (ultimoClienteEnOfertar != null && ultimoClienteEnOfertar.Id == oferta.Cliente.Id) throw new Exception("Su oferta ya es la más alta.");

            foreach (Oferta o in _ofertas)
            {
                if (o.Monto >= oferta.Monto) throw new Exception("Su oferta debe ser mayor a la oferta más alta.");
            }
            
            _ofertas.Add(oferta);
        }

        public void CerrarSubasta(Usuario finalizador)
        {
            if (Estado != EstadoPublicacion.ABIERTA) throw new Exception("La subasta ya se encuentra cerrada o cancelada.");

            UsuarioFinalizador = finalizador;
            FechaFinalizada = DateTime.Today;

            Oferta? o = MejorOfertaConSaldoSuficiente();

            if (o != null)
            {
                ClienteComprador = o.Cliente;
                ClienteComprador.Saldo -= o.Monto;
                Estado = EstadoPublicacion.CERRADA;
            }

            // En caso de que NO haya un ofertante con saldo suficiente, se cancela la subasta.
            if (o == null) Estado = EstadoPublicacion.CANCELADA;
        }

        private Cliente? UltimoClienteEnOfertar()
        {
            if (_ofertas.Count > 0) return _ofertas.Last().Cliente;
            return null;
        }

        // Devuelve la mejor oferta, donde el cliente tenga saldo suficiente.
        private Oferta? MejorOfertaConSaldoSuficiente()
        {
            int i = _ofertas.Count - 1;
            Oferta? oferta = null;

            while (i >= 0 && oferta == null)
            {
                Oferta o = _ofertas[i];
                if (o.Cliente.Saldo >= o.Monto) oferta = o;
                i--;
            }

            return oferta;
        }

        public int CompareTo(Subasta other)
        {
            return FechaPublicacion.CompareTo(other.FechaPublicacion);
        }
        #endregion

        #region OVERRIDES
        public override double CalcularPrecio()
        {
            if (_ofertas != null && _ofertas.Count != 0) return _ofertas.Last().Monto;
            return 0;
        }

        public override string TipoPublicacion()
        {
            return "Subasta";
        }

        public override bool EsOfertaRelampago()
        {
            return base.EsOfertaRelampago();
        }

        public override string EnlaceWeb()
        {
            return $"/Publicaciones/Subasta/{Id}";
        }

        public override string IconoWeb()
        {
            return "fa-solid fa-gavel";
        }

        public override string TituloWeb()
        {
            return "Ir a ofertar";
        }
        #endregion
    }
}