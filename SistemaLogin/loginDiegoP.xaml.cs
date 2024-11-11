using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaRegistro;
using HospiPlus.SistemaSecretario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//base de datos
using System.Data.SqlClient;
using System.Windows;
using HospiPlus.SistemaMedico;
using MaterialDesignThemes.Wpf;


using System.Security.Cryptography;
using System.Web.UI.WebControls;


namespace HospiPlus.SistemaLogin
{
    /// <summary>
    /// Lógica de interacción para loginDiegoP.xaml
    /// </summary>
    public partial class loginDiegoP : Window
    {
        SQLControl sQLControl = new SQLControl();
        public loginDiegoP()
        {
            InitializeComponent();
        }
        public class SessionInfo
        {
            public static string UsuarioRol { get; set; }
        }
        class UsuarioInfo
        {
            public int UsuarioID { get; set; }
            public string Rol { get; set; }
        }

        class SQLControl
        {
            private SqlConnection connection =
                new SqlConnection(@"Server=VLADIMIR\SQLEXPRESS;Database=HOSPIPLUS2;Integrated Security=True;Encrypt=False");

            public UsuarioInfo Login(string correo, string password)
            {
                UsuarioInfo usuario = null;
                try
                {
                    connection.Open();
                    SqlCommand cmb = new SqlCommand("spLogin1", connection);
                    cmb.CommandType = System.Data.CommandType.StoredProcedure;
                    cmb.Parameters.AddWithValue("correo", correo);
                    cmb.Parameters.AddWithValue("pass", password);

                    SqlDataReader dr = cmb.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.FieldCount > 1)
                        {
                            usuario = new UsuarioInfo
                            {
                                UsuarioID = dr.IsDBNull(0) ? 0 : dr.GetInt32(0),
                                Rol = dr.IsDBNull(1) ? "Sin Rol" : dr.GetString(1)
                            };
                        }
                        else
                        {
                            MessageBox.Show("El procedimiento almacenado no devolvió las suficientes columnas.");
                        }
                    }

                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    connection.Close();
                }

                return usuario;
            }
        }


        #region Método para Iniciar Sesión
        public void IniciarSesion()
        {
            // Valida los datos antes de iniciar sesión
            if (string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Por favor, ingrese su correo y contraseña.", "HOSPI PLUS | Campos Vacios", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            // Llama al método Login y almacena el resultado
            UsuarioInfo usuario = sQLControl.Login(txtCorreo.Text, txtPass.Password);


            if (usuario != null) // Si encontró un usuario
            {
                SessionInfo.UsuarioRol = usuario.Rol;  // Guardar el rol del usuario logueado

                // Redirige según el rol del usuario
                switch (usuario.Rol)
                {
                    case "Administrador":
                        MessageBox.Show("¡Bienvenido!", "HOSPI PLUS | Sistema Administrador", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow main = new MainWindow();
                        this.Hide();
                        main.Show();
                        break;
                    case "Secretario":
                        MessageBox.Show("¡Bienvenido!", "HOSPI PLUS | Sistema Secretario", MessageBoxButton.OK, MessageBoxImage.Information);
                        frmSistemaSecretario sisSecretario = new frmSistemaSecretario();
                        this.Hide();
                        sisSecretario.Show();
                        break;
                    case "Medico":
                        MessageBox.Show("¡Bienvenido!", "HOSPI PLUS | Sistema Médico", MessageBoxButton.OK, MessageBoxImage.Information);
                        frmSistemaMedico sisMedico = new frmSistemaMedico();
                        this.Hide();
                        sisMedico.Show();
                        break;
                    default:
                        MessageBox.Show("Rol de usuario no reconocido.", "HOSPI PLUS | Error de Sesión", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta.", "HOSPI PLUS | Credenciales Incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);
                // Limpiar los campos de correo y contraseña
                txtCorreo.Clear();
                txtPass.Clear();
            }
        }
        #endregion


        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion(); // Llamada al Método de Iniciar Sesión
        }
        private void btnIniciarSesion_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnIniciarSesion.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }
        private void btnIniciarSesion_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnIniciarSesion.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }


        #region Eventos ENTER
        private void txtCorreo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IniciarSesion();
            }
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IniciarSesion();
            }
        }
        #endregion

        private void btnCerrarsesion_Click(object sender, RoutedEventArgs e)
        {
            // Mensaje para estar seguro si desea salir o no
            MessageBoxResult resultado = MessageBox.Show("¿Seguro que quiere cerrar sesión?", "HOSPI PLUS | Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Si es así, se cierra la app
            if (resultado == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();   
            }
        }

        private void btnCerrarsesion_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnCerrarsesion.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnCerrarsesion_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnCerrarsesion.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
    }
}