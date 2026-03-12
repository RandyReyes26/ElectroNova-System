using ElectroNova.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroNova.Interfaces
{
    interface IBLLUsuario
    {
        Usuario Login(string pLogin, string pPassword);
        IEnumerable<Usuario> ObtenerTodoLogin();
        Usuario ObtenerUsuarioPorId(int pLogin);
        void GuardarUsuario(Usuario pUsuario);
        bool EiminarUsuario(string pLogin);
        void Actualizar(Usuario pUsuario);
        List<Usuario> ObtenerTodos();
    }
}
