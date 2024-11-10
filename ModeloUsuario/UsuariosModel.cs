using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HospiPlus.ModeloUsuario
{
    public class UsuariosModel
    {
        public int UsuarioID { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Rol {  get; set; }        
        public UsuariosModel() { }
    }
}