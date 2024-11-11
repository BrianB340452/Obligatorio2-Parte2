using Dominio.Interfaces;

namespace Dominio
{
    public class Oferta : IValidable
    {
        #region ATRIBUTOS
        private static int s_ultId = 1;
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public double Monto { get; set; }
        public DateTime FechaRealizada { get; set; }
        #endregion

        #region CONSTRUCTORES
        public Oferta(Cliente cliente, double monto, DateTime fechaRealizada)
        {
            Id = s_ultId++;
            Cliente = cliente;
            Monto = monto;
            FechaRealizada = fechaRealizada;
        }
        #endregion

        #region MÉTODOS Y FUNCIONES
        public void Validar()
        {
            if (Cliente == null) throw new Exception("Cliente inválido.");
            Cliente.Validar();
            if (Monto <= 0) throw new Exception("El monto a ofertar debe ser mayor a $0.");
            if (FechaRealizada < new DateTime(2024, 1, 1)) throw new Exception("La fecha de la oferta realizada es inválida.");
        }
        #endregion
    }
}