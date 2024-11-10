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





// Habilita las ventanas guardadas en las carpetas y poder mostrarlas
using HospiPlus.SistemaRegistro;
using HospiPlus.SistemaLogin;

//Los 3 niveles de usuario
using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;
using HospiPlus.SistemaSecretario;


using static MaterialDesignThemes.Wpf.Theme;


namespace HospiPlus.SistemaRegistro
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Page
    {
        
        public Registro()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            // Mensaje para estar seguro si desea salir o no
            MessageBoxResult resultado = MessageBox.Show("¿Seguro de regresar al login?", "HOSPI PLUS", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                //Comente para no perder la porcion de cod

                /* var formLoginPrincipal = new loginDiegoP();
                 NavigationService.Navigate(formLoginPrincipal);*/

                //Para volver al login 
                Window ventanaNueva = Window.GetWindow(this);
                if (ventanaNueva != null)
                {

                    //Para regresar al login
                    loginDiegoP login = new loginDiegoP();
                    login.Show();

                    ventanaNueva.Close();
                }

            }
        }

        private void btnRegistrarsePaciente_Click(object sender, RoutedEventArgs e)
        {

            if (ValidarDatos())
            {
                // Código para guardar los datos
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                limpiarDatos();
            }
        }

        private bool ValidarDatos()
        {
            // Verifica si los TextBox están vacíos
            if (string.IsNullOrWhiteSpace(txtCorreo.Text) ||
            string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Password) ||
                string.IsNullOrWhiteSpace(txtConfirmContra.Password))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Verifica si se ha seleccionado un ítem en el ComboBox
            if (comboBox.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un ítem en el ComboBox.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Si todas las validaciones pasan
            return true;
        }

        private void limpiarDatos()
        {
            txtCorreo.Text = "";
            txtNombre.Text = "";
            txtContrasena.Password = "";
            txtConfirmContra.Password = "";
            comboBox.Items.Clear();
        }
    }
    
}
