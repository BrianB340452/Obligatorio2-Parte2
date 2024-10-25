namespace Dominio
{
    public class Cliente : Usuario
    {
        private double _saldo;

        public Cliente(string nombre, string apellido, string email, string clave, double saldo) : base(nombre, apellido, email, clave)
        {
            _saldo = saldo;
        }

        public override void Validar()
        {
            base.Validar();
            ValidarSaldo();
        }

        public override string ToString()
        {
            return $"{_id}: {_nombre} {_apellido} | {_email} | ${_saldo}";
        }
        
        private void ValidarSaldo()
        {
            if (_saldo < 0) throw new Exception("El saldo debe ser un monto positivo.");
        }
    }
}
