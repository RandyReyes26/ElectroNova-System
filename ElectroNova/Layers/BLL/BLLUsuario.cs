using ElectroNova.Interfaces;
using ElectroNova.Layers.DAL;
using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Layers.BLL
{
    class BLLUsuario : IBLLUsuario
    {
        public void Actualizar(Usuario pUsuario)
        {
            var dalUsuario = new DALUsuario();
            dalUsuario.ActualizarUsuario(pUsuario);
        }

        public bool EiminarUsuario(string pLogin)
        {
            IDALUsuario _DALUsuario = new DALUsuario();
            return _DALUsuario.EliminarUsuario(pLogin);
        }

        public void GuardarUsuario(Usuario pUsuario)
        {
            var DalUsuario = new DALUsuario();
            DalUsuario.GuardarUsuario(pUsuario);
        }

        public Usuario Login(string pLogin, string pPassword)
        {
            DALUsuario dALUsuario = new DALUsuario();

            return dALUsuario.Login(pLogin, pPassword);
        }

        public IEnumerable<Usuario> ObtenerTodoLogin()
        {
            IDALUsuario _DALUsuario = new DALUsuario();
            return _DALUsuario.ObtenerTodoLogin();
        }

        public List<Usuario> ObtenerTodos()
        {
            var dalUSuario = new DALUsuario();
            return dalUSuario.SeleccionarTodos();
        }

        public Usuario ObtenerUsuarioPorId(int pLogin)
        {
            IDALUsuario _DALUsuario = new DALUsuario();
            return _DALUsuario.ObtenerUsuarioPorId(pLogin);
        }
    }
}
