using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

using HospiPlus.ModeloUsuario;
using HospiPlus.DataAcces;

using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;
using HospiPlus.SistemaSecretario;
using HospiPlus.ServiceMedico;
using System.Collections;
using MaterialDesignThemes.Wpf;
using HospiPlus.ServiceUsuario;


namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para GestionUsuariosAdmin.xaml
    /// </summary>
    public partial class GestionUsuariosAdmin : Page
    {
        public GestionUsuariosAdmin()
        {
            InitializeComponent();
            MostrarUsuarios();
            txtNombreUsuariosAdmi.Focus();
        }

        int usuarioid = 0;



        bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;

            if (string.IsNullOrWhiteSpace(txtNombreUsuariosAdmi.Text))
            {
                estado = false;
                mensaje += "Nombre Usuario\n";
            }

            if (string.IsNullOrWhiteSpace(txtCorreoUsuariosAdmi.Text))
            {
                estado = false;
                mensaje += "Correo\n";
            }

            if (string.IsNullOrWhiteSpace(psContra.Password))
            {
                estado = false;
                mensaje += "Contraseña\n";
            }

            if (cmbRolUsuariosAdmi.SelectedItem == null)
            {
                estado = false;
                mensaje += "Rol de Usuario\n";
            }

            //MENSAJE
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos:\n" + mensaje,
                "Validación de Formulario",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            return estado;

        }









        #region Metodo Mostrar Usuarios
        // Mostrar para mostrar usuarios
        void MostrarUsuarios()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    // Obtener los usuarios desde el método MostrarUsuarios() de DatosUsuarios
                    var usuarios = DatosUsuarios.MostrarUsuarios();

                    // Enmascarar la contraseña antes de asignar la lista al Grid
                    foreach (var usuario in usuarios)
                    {
                        usuario.Contrasena = "********"; // Enmascarar la contraseña
                    }

                    // Asignar la lista de usuarios modificada al Grid
                    gridGestorUsuario.ItemsSource = usuarios;

                    txtNombreUsuariosAdmi.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al mostrar los usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LimpiarCampos();
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion



        // Cadena de Conexion del Servidor SQL
        #region Evento GRID
        private void gridGestorUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtiene el usuario seleccionado en el DataGrid
            UsuariosModel usuarios = (UsuariosModel)gridGestorUsuario.SelectedItem;

            if (usuarios == null)
            {
                return;
            }

            try
            {
                // Abre la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(@"Server=workstation id=HOSPIPLUS.mssql.somee.com;packet size=4096;user id=Asce_12_SQLLogin_1;pwd=fjhhqvclrl;data source=HOSPIPLUS.mssql.somee.com;persist security info=False;initial catalog=HOSPIPLUS;TrustServerCertificate=True"))
                {
                    connection.Open();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand cmd = new SqlCommand("spObtenerUsuarioContrasenaDesencriptada", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UsuarioID", usuarios.UsuarioID);

                        // Ejecuta el comando y obtiene los resultados
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Asigna los valores a los controles
                                txtNombreUsuariosAdmi.Text = dr["NombreUsuario"].ToString();
                                txtCorreoUsuariosAdmi.Text = dr["Correo"].ToString();

                                // Asigna la contraseña desencriptada al PasswordBox
                                // Asegúrate de que el campo sea el correcto y se desencripte adecuadamente
                                psContra.Password = dr["ContrasenaDesencriptada"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron resultados para el usuario seleccionado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                LimpiarCampos();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LimpiarCampos();
            }
        }
        #endregion




        #region Limpiar Campos
        public void LimpiarCampos()
        {
            txtNombreUsuariosAdmi.Text = "";
            txtCorreoUsuariosAdmi.Text = "";
            psContra.Clear();
            cmbRolUsuariosAdmi.Text = "";
        }
        #endregion




        #region Metodo Insertar Usuario
        public void InsertarUsuario(string nombreUsuario, string correo, string contrasenaTexto, string rol)
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    // Abrir la conexión
                    ConexionDB.AbrirConexion(conexion);

                    // Crear el comando para ejecutar el procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("InsertarUsuario", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros al comando
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@ContrasenaTexto", contrasenaTexto);
                    cmd.Parameters.AddWithValue("@Rol", rol);

                    // Agregar un parámetro para capturar el valor de retorno del procedimiento almacenado
                    SqlParameter returnParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParameter);

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery(); // Usar ExecuteNonQuery ya que no necesitamos un valor de retorno como tal

                    // Leer el valor de retorno
                    int resultado = (int)returnParameter.Value;

                    // Verificar el resultado del procedimiento almacenado
                    if (resultado == 1)
                    {
                        MessageBox.Show("El usuario se ha insertado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        MostrarUsuarios();
                        LimpiarCampos();
                    }
                    else if (resultado == 2)
                    {
                        MessageBox.Show("El correo ya está registrado. No se puede insertar el usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        MostrarUsuarios();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema al insertar el usuario. Intente nuevamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        MostrarUsuarios();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    ValidarFormulario();
                    MostrarUsuarios();
                    LimpiarCampos();
                }
                finally
                {
                    // Cerrar la conexión
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion



        #region Boton para Guardar Usuario
        private void btnGuardarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(txtNombreUsuariosAdmi.Text) || string.IsNullOrEmpty(txtCorreoUsuariosAdmi.Text) || string.IsNullOrEmpty(psContra.Password))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Llamar al método para insertar el nuevo usuario, pasando los valores del formulario directamente
            InsertarUsuario(txtNombreUsuariosAdmi.Text, txtCorreoUsuariosAdmi.Text, psContra.Password, cmbRolUsuariosAdmi.Text);
        }

        #endregion




        #region Metodo Editar Usuario
        public void editarUsuarios(UsuariosModel usuario)
        {
            // Validación de la contraseña (asegurarse de que no esté vacía o tenga un formato válido)
            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                MessageBox.Show("La contraseña no puede estar vacía.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validación del rol (asegurarse de que el rol esté en el listado permitido)
            if (!new[] { "Administrador", "Secretario", "Medico" }.Contains(usuario.Rol))
            {
                MessageBox.Show("Rol no válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Conexión a la base de datos y ejecución del procedimiento almacenado
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    using (SqlCommand cmd = new SqlCommand("EditarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros al comando
                        cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                        cmd.Parameters.AddWithValue("@NombreUsuario", usuario.Usuario);
                        cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("@ContrasenaTexto", usuario.Contrasena); // Utilizamos el campo de la contraseña sin cifrar
                        cmd.Parameters.AddWithValue("@Rol", usuario.Rol);

                        // Definir el parámetro de salida
                        SqlParameter returnValue = new SqlParameter
                        {
                            ParameterName = "@ReturnValue",
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.ReturnValue
                        };
                        cmd.Parameters.Add(returnValue);

                        // Ejecutar el procedimiento almacenado
                        cmd.ExecuteNonQuery();

                        // Obtener el valor de retorno
                        int result = (int)returnValue.Value;

                        // Verificar el resultado y mostrar el mensaje adecuado
                        switch (result)
                        {
                            case 1:
                                MessageBox.Show("Usuario editado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            case 2:
                                MessageBox.Show("El correo ya está registrado para otro usuario. No se puede actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                break;
                            case 0:
                            default:
                                MessageBox.Show("Hubo un problema al actualizar el usuario. Intente nuevamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                break;
                        }

                        // Llamar al método para mostrar los usuarios actualizados
                        MostrarUsuarios();
                        LimpiarCampos();
                    }
                }
                catch (SqlException ex)
                {
                    // Mostrar detalles adicionales del error de SQL
                    MessageBox.Show($"Ocurrió un error al editar el Usuario: {ex.Message}\nDetalles adicionales: {ex.ToString()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion



        #region Boton Modificar Usuario
        private void btnModificarUsuariosAdmi_Click(object sender, RoutedEventArgs e)
        {
            if (gridGestorUsuario.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un Usuario de la lista.", "Usuario no seleccionado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Se obtienen los datos del usuario seleccionado
            UsuariosModel selectedUsuario = (UsuariosModel)gridGestorUsuario.SelectedItem;

            // Crear un nuevo objeto de tipo UsuariosModel con los valores editados
            UsuariosModel usuarioEditado = new UsuariosModel
            {
                UsuarioID = selectedUsuario.UsuarioID,
                Usuario = txtNombreUsuariosAdmi.Text,
                Correo = txtCorreoUsuariosAdmi.Text,
                Contrasena = psContra.Password,  // Si la contraseña ha cambiado
                Rol = cmbRolUsuariosAdmi.Text
            };

            // Llamar al método de edición de usuario
            editarUsuarios(usuarioEditado);
            MostrarUsuarios();
            LimpiarCampos();
        }
        #endregion



        #region Boton de Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar el proceso?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarCampos(); // Limpiar los campos 
                MostrarUsuarios(); // Y actualiza el grid de los Médicos
                return;
            }
        }
        #endregion



        #region Eventos ENTER
        private void txtNombreUsuariosAdmi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Obtén los valores de los campos de entrada
                string nombreUsuario = txtNombreUsuariosAdmi.Text;
                string correo = txtCorreoUsuariosAdmi.Text;
                string contrasenaTexto = psContra.Password; // Si estás usando un PasswordBox
                string rol = cmbRolUsuariosAdmi.SelectedItem?.ToString(); // Si tienes un ComboBox para roles

                // Llama al método InsertarUsuario con los valores obtenidos
                InsertarUsuario(nombreUsuario, correo, contrasenaTexto, rol);
            }
        }

        private void txtCorreoUsuariosAdmi_KeyUp(object sender, KeyEventArgs e)
        {
            

            if (e.Key == Key.Enter)
            {
                // Obtén los valores de los campos de entrada
                string nombreUsuario = txtNombreUsuariosAdmi.Text;
                string correo = txtCorreoUsuariosAdmi.Text;
                string contrasenaTexto = psContra.Password; // Si estás usando un PasswordBox
                string rol = cmbRolUsuariosAdmi.SelectedItem?.ToString(); // Si tienes un ComboBox para roles

                // Llama al método InsertarUsuario con los valores obtenidos
                InsertarUsuario(nombreUsuario, correo, contrasenaTexto, rol);
            }
        }

        private void psContra_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                // Obtén los valores de los campos de entrada
                string nombreUsuario = txtNombreUsuariosAdmi.Text;
                string correo = txtCorreoUsuariosAdmi.Text;
                string contrasenaTexto = psContra.Password; // Si estás usando un PasswordBox
                string rol = cmbRolUsuariosAdmi.SelectedItem?.ToString(); // Si tienes un ComboBox para roles

                // Llama al método InsertarUsuario con los valores obtenidos
                InsertarUsuario(nombreUsuario, correo, contrasenaTexto, rol);
            }
        }

        private void cmbRolUsuariosAdmi_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                // Obtén los valores de los campos de entrada
                string nombreUsuario = txtNombreUsuariosAdmi.Text;
                string correo = txtCorreoUsuariosAdmi.Text;
                string contrasenaTexto = psContra.Password; // Si estás usando un PasswordBox
                string rol = cmbRolUsuariosAdmi.SelectedItem?.ToString(); // Si tienes un ComboBox para roles

                // Llama al método InsertarUsuario con los valores obtenidos
                InsertarUsuario(nombreUsuario, correo, contrasenaTexto, rol);
            }
        }

        private void gridGestorUsuario_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                // Comprueba si hay un elemento seleccionado en gridGestorUsuario y si es del tipo UsuariosModel
                if (gridGestorUsuario.SelectedItem is UsuariosModel usuarios)
                {
                    // Obtén los valores de los campos de entrada
                    string nombreUsuario = txtNombreUsuariosAdmi.Text;
                    string correo = txtCorreoUsuariosAdmi.Text;
                    string contrasenaTexto = psContra.Password; // Si estás usando un PasswordBox para la contraseña
                    string rol = cmbRolUsuariosAdmi.SelectedItem?.ToString(); // Asume que es un ComboBox para seleccionar roles

                    // Comprueba si los valores necesarios están presentes antes de llamar a InsertarUsuario
                    if (!string.IsNullOrEmpty(nombreUsuario) &&
                        !string.IsNullOrEmpty(correo) &&
                        !string.IsNullOrEmpty(contrasenaTexto) &&
                        !string.IsNullOrEmpty(rol))
                    {
                        // Llama al método InsertarUsuario con los valores obtenidos
                        InsertarUsuario(nombreUsuario, correo, contrasenaTexto, rol);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, complete todos los campos antes de insertar el usuario.");
                    }
                }
            }
        }
        #endregion
    }
}