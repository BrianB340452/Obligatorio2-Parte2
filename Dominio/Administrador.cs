﻿namespace Dominio
{
    public class Administrador : Usuario
    {
        #region CONSTRUCTORES
        public Administrador(string nombre, string apellido, string email, string clave) : base(nombre, apellido, email, clave) {}
        #endregion

        #region OVERRIDES
        public override char TipoUsuario()
        {
            return 'A';
        }
        #endregion
    }
}