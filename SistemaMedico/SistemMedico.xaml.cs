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
using HospiPlus.SistemaSecretario;



namespace HospiPlus.SistemaMedico
{
    /// <summary>
    /// Lógica de interacción para SistemMedico.xaml
    /// </summary>
    public partial class SistemMedico : Page
    {
        public SistemMedico()
        {
            InitializeComponent();
        }

        //Paciente Medico
        #region btnGestionPacienteMedico
        private void btnGestionPacienteMedico_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaMedico/GestionPacienteMedico.xaml", UriKind.Relative));
        }
        private void btnGestionPacienteMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnGestionPacienteMedico.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnGestionPacienteMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnGestionPacienteMedico.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin

        //Consulta Medico
        #region btnConsultasMedico
        private void btnConsultasMedico_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaMedico/ConsultasMedico.xaml", UriKind.Relative));
        }
        private void btnConsultasMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnConsultasMedico.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnConsultasMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnConsultasMedico.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin

        //Recetas Medico
        #region btnRecetasMedico
        private void btnRecetasMedico_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaMedico/RecetasMedico.xaml", UriKind.Relative));
        }
        private void btnRecetasMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnRecetasMedico.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnRecetasMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnRecetasMedico.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin

        //Citas Medico
        #region btnCitasMedico
        private void btnCitasMedico_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaMedico/CitasMedico.xaml", UriKind.Relative));
        }
        private void btnCitasMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnCitasMedico.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnCitasMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnCitasMedico.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin

        //Examen Medico
        #region btnExamenesMedico
        private void btnExamenesMedico_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaMedico/ExamenesMedico.xaml", UriKind.Relative));
        }
        private void btnExamenesMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnExamenesMedico.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnExamenesMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnExamenesMedico.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin

        //Reportes Medico
        #region btnReportesMedico
        private void btnReportesMedico_Click(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaMedico/ReportesMedico.xaml", UriKind.Relative));
        }
        private void btnReportesMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnReportesMedico.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void btnReportesMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnReportesMedico.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion
        //fin

        //Salir Medico
        #region btnSalirMedico
        private void btnSalirMedico_Click(object sender, RoutedEventArgs e)
        {
            // Mensaje para estar seguro si desea salir o no
            MessageBoxResult resultado = MessageBox.Show("¿Seguro de salir de Sistema Médico?", "HOSPI PLUS | Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Si es así, se cierra la app
            //if (resultado == MessageBoxResult.Yes)
            //{
            //Application.Current.Shutdown();
            //}
            //Si es así, se cierra la app
            if (resultado == MessageBoxResult.Yes)
            {
                //Comente este cod por si acaso :D
                /*Application.Current.Shutdown();*/

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
        private void btnSalirMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnSalirMedico.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnSalirMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnSalirMedico.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion
        //fin

        //Bienvenida Medico
        #region frPrincipal
        private void frPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaMedico/PaginaBienvenidaMedico.xaml", UriKind.Relative));
        }

        #endregion
        //fin
    }
}