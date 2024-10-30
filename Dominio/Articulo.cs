using Dominio.Interfaces;

namespace Dominio
{
    public class Articulo : IValidable
    {
        #region ATRIBUTOS
        private static int s_ultId = 1;
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public double Precio { get; set; }
        #endregion

        #region CONSTRUCTORES
        public Articulo(string nombre, string categoria, double precio)
        {
            Id = s_ultId++;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
        }
        #endregion

        #region MÉTODOS Y FUNCIONES
        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre no puede estar vacío.");
            if (string.IsNullOrEmpty(Categoria)) throw new Exception("La categoría no puede estar vacía.");
            if (Precio <= 0) throw new Exception("El precio debe ser mayor a $0.");
        }
        #endregion

        #region OVERRIDES
        public override string ToString()
        {
            return $"Artículo Nº{Id}: {Nombre} - ${Precio}";
        }

        public override bool Equals(object? obj)
        {
            Articulo a = obj as Articulo;
            return a != null && this.Nombre == a.Nombre && this.Categoria == a.Categoria && this.Precio == a.Precio;
        }
        #endregion
    }
}
