﻿using System;
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
using System.Windows.Shapes;
// Habilita las ventanas guardadas en las carpetas y poder mostrarlas

using HospiPlus.SistemaLogin;
using HospiPlus.SistemaSecretario;
using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;

namespace HospiPlus.SistemaSecretario
{
    /// <summary>
    /// Lógica de interacción para frmSistemaSecretario.xaml
    /// </summary>
    public partial class frmSistemaSecretario : Window
    {

        // Instanciando a las paginas
        loginDiegoP login = new loginDiegoP();
        //InicioAdministrador inicioAdministrador = new InicioAdministrador();
        SistemSecretario sistemSecretario = new SistemSecretario();
        //SistemMedico sistemMedico = new SistemMedico();

        public frmSistemaSecretario()
        {
            InitializeComponent();

            // Aqui se puede definir la pagina por defecto
            frPrincipal.NavigationService.Navigate(sistemSecretario);
        }



        private void frPrincipal_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}