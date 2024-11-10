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

using HospiPlus.SistemaRegistro;
using HospiPlus.SistemaLogin;
using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;
using System.Security.Principal;



namespace HospiPlus.SistemaSecretario
{
    /// <summary>
    /// Lógica de interacción para SistemSecretario.xaml
    /// </summary>
    public partial class SistemSecretario : Page
    {
        public SistemSecretario()
        {
            InitializeComponent();
        }

        //Paciente Secretaria
        #region btnGestionPacienteSecre
        private void btnGestionPacienteSecre_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaSecretario/GestionPacienteSecretario.xaml", UriKind.Relative));
        }

        private void btnGestionPacienteSecre_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnGestionPacienteSecre.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnGestionPacienteSecre_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnGestionPacienteSecre.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin

        //Consultas Secretaria
        #region btnConsultasSecretario
        private void btnConsultasSecretario_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaSecretario/ConsultasSecretario.xaml", UriKind.Relative));
        }
        private void btnConsultasSecretario_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnConsultasSecretario.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnConsultasSecretario_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnConsultasSecretario.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin


        //Citas Secretaria
        #region btnCitasSecretario
        private void btnCitasSecretario_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaSecretario/CitasSecretario.xaml", UriKind.Relative));
        }
        private void btnCitasSecretario_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnCitasSecretario.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnCitasSecretario_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnCitasSecretario.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin

        //Examenes Secretaria
        #region btnExamenesSecretario
        private void btnExamenesSecretario_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaSecretario/ExamenesSecretario.xaml", UriKind.Relative));
        }
        private void btnExamenesSecretario_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnExamenesSecretario.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnExamenesSecretario_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnExamenesSecretario.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin

        //Reportes Secretaria
        #region btnReportesSecretario
        private void btnReportesSecretario_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaSecretario/ReportesSecretario.xaml", UriKind.Relative));

        }
        private void btnReportesSecretario_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnReportesSecretario.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnReportesSecretario_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnReportesSecretario.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin 

        //Salir Secretaria
        #region btnSalirSecretario
        private void btnSalirSecretario_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Seguro de salir de sistema secretaria?", "HOSPI PLUS", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Si es así, se cierra la app
            if (resultado == MessageBoxResult.Yes)
            {
                
                //para regresar al login
                Window ventanaRegistro = Window.GetWindow(this);
                if (ventanaRegistro != null)
                {
                    //Para regresar al login
                    //MainWindow niveles = new MainWindow();
                    //niveles.Show();

                    loginDiegoP login = new loginDiegoP();
                    login.Show();
                    ventanaRegistro.Close();
                }
            }
        }
        private void btnSalirSecretario_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnSalirSecretario.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnSalirSecretario_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnSalirSecretario.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin

        //Bienvenida Secretaria
        #region frPrincipal
        private void frPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaSecretario/PaginaBienvenidaSecretario.xaml", UriKind.Relative));

            //NO HAGAN CASO A ESTA LINEA DE CODIGO POR PROBLEMAS CON EL MENU DE BOTONES XDDDDDDDDDD
            //frPrincipal.NavigationService.Navigate(new Uri("SistemaSecretario/PaginaBienvenidaSecretario.xaml", UriKind.Relative));
        }

        #endregion
        //fin


    }
}
