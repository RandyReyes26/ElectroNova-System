using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IDALUsuario
    {
        Usuario Login(string pLogin, string pPassword);
        IEnumerable<Usuario> ObtenerTodoLogin();
        void GuardarUsuario(Usuario pUsuario);
        void ActualizarUsuario(Usuario pUsuario);
        Usuario ObtenerUsuarioPorId(int pLogin);
        bool EliminarUsuario(string pLogin);
        List<Usuario> SeleccionarTodos();
    }
}
