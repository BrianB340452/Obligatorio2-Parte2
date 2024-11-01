﻿using Dominio.Interfaces;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        #region ATRIBUTOS
        private static int s_ultId = 1;
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        #endregion

        #region CONSTRUCTORES
        public Usuario(string nombre, string apellido, string email, string clave)
        {
            Id = s_ultId++;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Clave = clave;
        }
        #endregion

        #region MÉTODOS Y FUNCIONES
        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre no puede estar vacío.");
            if (string.IsNullOrEmpty(Apellido)) throw new Exception("El apellido no puede estar vacío.");
            if (!EmailValido(Email)) throw new Exception("El email ingresado es inválido.");
            if (Clave.Length < 4) throw new Exception("La contraseña debe contener un mínimo de 4 caracteres.");
        }

        public abstract char TipoUsuario();

        private bool EmailValido(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (!email.Contains("@")) return false;
            if (email.Contains(" ")) return false;
            if (email.StartsWith("@")) return false;
            if (email.EndsWith("@")) return false;

            return true;
        }
        #endregion

        #region OVERRIDES
        public override bool Equals(object? obj)
        {
            Usuario u = obj as Usuario;
            return u != null && this.Email == u.Email;
        }
        #endregion
    }
}