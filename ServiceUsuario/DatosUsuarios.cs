using HospiPlus.ModeloUsuario;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HospiPlus.DataAcces;
using System.Windows.Controls;

namespace HospiPlus.ServiceUsuario
{
    public class DatosUsuarios
    {
        public DatosUsuarios() { }

        public static List<UsuariosModel> MostrarUsuarios()
        {
            List<UsuariosModel> lsUsuarios = new List<UsuariosModel>();
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarUsuarios";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UsuariosModel modeleUsuario = new UsuariosModel
                            {
                                UsuarioID = int.Parse(dr["UsuarioID"].ToString()),
                                Usuario = dr["NombreUsuario"].ToString(), // Asignar el NombreUsuario
                                Correo = dr["Correo"].ToString(), // Asignar el Correo
                                Rol = dr["Rol"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),         // Asignar la contraseña como byte[]
                                
                            };

                            lsUsuarios.Add(modeleUsuario);  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close(); // Cerrar la conexión
                }
            }
            return lsUsuarios;
        }// fin de Mostrar Usuarios
    }
}
