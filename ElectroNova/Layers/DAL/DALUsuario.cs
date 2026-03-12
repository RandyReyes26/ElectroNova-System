using ElectroNova.Interfaces;
using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.DAL
{
    class DALUsuario : IDALUsuario
    {
        public void ActualizarUsuario(Usuario pUsuario)
        {
            throw new NotImplementedException();
        }

        public bool EliminarUsuario(string pLogin)
        {
            throw new NotImplementedException();
        }

        public void GuardarUsuario(Usuario pUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario Login(string pLogin, string pPassword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> ObtenerTodoLogin()
        {
            throw new NotImplementedException();
        }

        public Usuario ObtenerUsuarioPorId(int pLogin)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> SeleccionarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
