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

using HospiPlus.SistemaLogin;
using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;


namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para InicioAdministrador.xaml
    /// </summary>
    public partial class InicioAdministrador : Page
    {

        public InicioAdministrador()
        {
            InitializeComponent();
            
        }

        //GESTION PACIENTE
        #region btnGestionPacienteAdmin

        private void btnGestionPacienteAdmin_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/GestionPacienteAdmin.xaml", UriKind.Relative));
        }
        private void btnGestionPacienteAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnGestionPacienteAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnGestionPacienteAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnGestionPacienteAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin PACIENTES


        //GESTION MEDICO
        #region btnGestionMedicoAdmin
        private void btnGestionMedicoAdmin_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/GestionMedicoAdmin.xaml", UriKind.Relative));
        }

        private void btnGestionMedicoAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnGestionMedicoAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnGestionMedicoAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnGestionMedicoAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin MEDICOS
        

        //GESTION CONSULTA
        #region btnConsultasAdmin
        private void btnConsultasAdmin_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/GestionConsultasAdmin.xaml", UriKind.Relative));
        }
        private void btnConsultasAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnConsultasAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));

        }

        private void btnConsultasAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnConsultasAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original

        }

        #endregion
        //fin CONSULTA


        //GESTION RECETAS
        #region btnRecetasAdmin
        private void btnRecetasAdmin_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/GestionRecetasAdmin.xaml", UriKind.Relative));
        }

        private void btnRecetasAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnRecetasAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));

        }

        private void btnRecetasAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnRecetasAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin RECETAS


        //GESTION CITAS
        #region btnCitasAdmin
        private void btnCitasAdmin_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/GestionCitasAdmin.xaml", UriKind.Relative));
        }
        private void btnCitasAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnCitasAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnCitasAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnCitasAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin CITAS


        //GESTION EXAMENES
        #region btnExamenesAdmin
        private void btnExamenesAdmin_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/GestionExamenesAdmin.xaml", UriKind.Relative));
        }
        private void btnExamenesAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnExamenesAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnExamenesAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnExamenesAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin EXAMENES


        //GESTION USUARIOS
        #region btnUsuariosAdmin
        private void btnUsuariosAdmin_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/GestionUsuariosAdmin.xaml", UriKind.Relative));
        }
        private void btnUsuariosAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnUsuariosAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnUsuariosAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnUsuariosAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin USUARIOS


        //GESTION REPORTES 
        #region btnReportesAdmin
        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/ReporteAdministrador.xaml", UriKind.Relative));
        }
        private void btnReportes_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnReportesAdmin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnReportes_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnReportesAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin REPORTES


        //GESTION SALIR
        #region btnSalirAdmin
        private void btnSalirAdmin_Click(object sender, RoutedEventArgs e)
        {
            // Mensaje para estar seguro si desea salir o no
            MessageBoxResult resultado = MessageBox.Show("¿Seguro de salir de sistema administrador?", "HOSPI PLUS", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Si es así, se cierra la app
            if (resultado == MessageBoxResult.Yes)
            {
                //Comente este cod por si acaso :D
                /*Application.Current.Shutdown();*/

                //para regresar al login
                Window ventanaRegistro = Window.GetWindow(this);
                if (ventanaRegistro != null)
                {
                    //Para regresar al login
                    MainWindow niveles = new MainWindow();
                    niveles.Show();

                    ventanaRegistro.Close();
                }
            }
        }
        private void btnSalirAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnSalirAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnSalirAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnSalirAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin SALIR


        //BIENVENIDA
        #region frPrincipal
        private void frPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/PaginaBienvenidaAdministrador.xaml", UriKind.Relative));
        }
        #endregion
        //fin Bienvenida

    }
}