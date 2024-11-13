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
        public void ValidarSaldo()
        {
            if (Saldo < 0) throw new Exception("El saldo debe ser un monto positivo.");
        }

        public void RecargarSaldo(int monto)
        {
            if (Saldo <= 0) throw new Exception("El saldo a recargar debe ser mayor a $0.");
            Saldo += monto;
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