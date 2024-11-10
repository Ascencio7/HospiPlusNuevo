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
using HospiPlus.SistemaSecretario;
using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;



namespace HospiPlus
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        loginDiegoP login = new loginDiegoP();

        public MainWindow()
        {
            InitializeComponent();
        }


        //SISTEMA ADMI
        #region btnSistemaAdministrador
        private void btnSistemaAdministrador_Click(object sender, RoutedEventArgs e)
        {
            frmSistemaAdministrador sisAdmin = new frmSistemaAdministrador();
            sisAdmin.Show();
            this.Hide();
        }
        private void btnSistemaAdministrador_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnSistemaAdministrador.Background = new SolidColorBrush(Color.FromRgb(93, 173, 226));
        }

        private void btnSistemaAdministrador_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnSistemaAdministrador.Background = new SolidColorBrush(Color.FromRgb(174, 214, 241)); // Color original
        }

        #endregion
        //fin


        //SISTEMA MEDICO
        #region btnSistemaMedico
        private void btnSistemaMedico_Click(object sender, RoutedEventArgs e)
        {
            frmSistemaMedico sisMedico = new frmSistemaMedico();
            sisMedico.Show();

            //this.Hide();
        }
        private void btnSistemaMedico_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            btnSistemaMedico.Background = new SolidColorBrush(Color.FromRgb(93, 173, 226));
        }

        private void btnSistemaMedico_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            btnSistemaMedico.Background = new SolidColorBrush(Color.FromRgb(174, 214, 241)); // Color original
        }
        #endregion
        //fin


        //SISTEMA SECRETARIO
        #region SistemaSecretario
        private void SistemaSecretario_Click(object sender, RoutedEventArgs e)
        {
            frmSistemaSecretario sisSecretario = new frmSistemaSecretario();
            sisSecretario.Show();
            //this.Hide();
        }
        private void SistemaSecretario_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            SistemaSecretario.Background = new SolidColorBrush(Color.FromRgb(93, 173, 226));
        }

        private void SistemaSecretario_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            SistemaSecretario.Background = new SolidColorBrush(Color.FromRgb(174, 214, 241)); // Color original
        }
        #endregion
        //fin

        //SALIR LOGIN
        #region SalirLogin
        private void SalirLogin_Click(object sender, RoutedEventArgs e)
        {
            // Mensaje para estar seguro si desea salir o no
            MessageBoxResult resultado = MessageBox.Show("¿Seguro que quiere cerrar sesión?", "HOSPI PLUS | Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Si es así, se cierra la app
            if (resultado == MessageBoxResult.Yes)
            {
                login.Show();
                this.Close();
                

            }
            //this.Close(); // Se oculta la ventana de los 3 niveles

        }

        private void SalirLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cambiar el color cuando el cursor entra en el área del botón
            SalirLogin.Background = new SolidColorBrush(Color.FromRgb(102, 0, 161));
        }

        private void SalirLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            // Restaurar el color original cuando el cursor sale del área del botón
            SalirLogin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }


        #endregion
        //fin
    }
}
