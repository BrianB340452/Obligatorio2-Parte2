namespace Dominio
{
    public class Cliente : Usuario
    {
        #region ATRIBUTOS
        public double Saldo { get; set; }
        #endregion

        #region CONSTRUCTORES
        public Cliente(string nombre, string apellido, string email, string clave, double saldo) : base(nombre, apellido, email, clave)
        {
            Saldo = saldo;
        }
        #endregion

        #region MÉTODOS Y FUNCIONES
        private void ValidarSaldo()
        {
            if (Saldo < 0) throw new Exception("El saldo debe ser un monto positivo.");
        }
        #endregion

        #region OVERRIDES
        public override void Validar()
        {
            base.Validar();
            ValidarSaldo();
        }

        public override string TipoUsuario()
        {
            return "Cliente";
        }

        public override string ToString()
        {
            return $"{Id}: {Nombre} {Apellido} | {Email} | ${Saldo}";
        }
        #endregion
    }
}