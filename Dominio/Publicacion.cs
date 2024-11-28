using Dominio.Enums;
using Dominio.Interfaces;

namespace Dominio
{
    public abstract class Publicacion : IValidable
    {
        #region ATRIBUTOS
        protected List<Articulo> _articulos = new List<Articulo>();
        private static int s_ultId = 1;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public EstadoPublicacion Estado { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public Cliente ClienteComprador { get; set; }
        public Usuario UsuarioFinalizador { get; set; }
        public DateTime FechaFinalizada { get; set; }
        public List<Articulo> Articulos { get { return _articulos; } }
        #endregion

        #region CONSTRUCTORES
        protected Publicacion(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion)
        {
            Id = s_ultId++;
            Nombre = nombre;
            Estado = estado;
            FechaPublicacion = fechaPublicacion;
        }

        // Constructor sólo para precargas
        protected Publicacion(string nombre, EstadoPublicacion estado, DateTime fechaPublicacion, List<Articulo> articulos)
        {
            Id = s_ultId++;
            Nombre = nombre;
            Estado = estado;
            FechaPublicacion = fechaPublicacion;
            _articulos = articulos;
        }
        #endregion

        #region MÉTODOS Y FUNCIONES
        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre no puede estar vacío.");
            if (FechaPublicacion < new DateTime(2024, 1, 1) || FechaPublicacion > DateTime.Today) throw new Exception("La fecha de publicación es inválida.");
        }

        public abstract string TipoPublicacion();

        public abstract void CerrarPublicacion(Usuario u);

        public virtual void AgregarArticulo(Articulo articulo)
        {
            if (articulo == null) throw new Exception("El artículo no puede ser nulo.");
            articulo.Validar();
            if (_articulos.Contains(articulo)) throw new Exception("El artículo ingresado ya se encuentra en la publicación.");
            _articulos.Add(articulo);
        }

        public virtual double CalcularPrecio()
        {
            double precioFinal = 0;

            foreach (Articulo a in _articulos)
            {
                precioFinal += a.Precio;
            }

            return precioFinal;
        }

        public string ClaseBtnWeb()
        {
            if (Estado != EstadoPublicacion.ABIERTA) return "btn btn-danger disabled";
            return "btn btn-success";
        }

        public virtual bool EsOfertaRelampago()
        {
            return false;
        }

        public abstract string EnlaceWeb();
        public abstract string IconoWeb();
        public abstract string TituloWeb();
        #endregion

        #region OVERRIDES
        public override string ToString()
        {
            return $"Nº{Id}: {Nombre} | Estado: {Estado} | Fecha publicación: {FechaPublicacion.ToShortDateString()}";
        }
        #endregion
    }
}