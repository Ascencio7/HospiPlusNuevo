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
using HospiPlus.SistemaAdministrador;
using HospiPlus.SistemaMedico;
using HospiPlus.SistemaSecretario;



using HospiPlus.ModeloMedico;
using HospiPlus.ModeloExamen;
using HospiPlus.ModeloPaciente;
using HospiPlus.ModeloUsuario;

using HospiPlus.ServicePaciente;
using HospiPlus.ServiceMedico;
using HospiPlus.ServiceUsuario;
using HospiPlus.ServiceExamenes;


using System.Security.Principal;
using System.Data.SqlClient;
using HospiPlus.DataAcces;


namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para GestionExamenesAdmin.xaml
    /// </summary>
    public partial class GestionExamenesAdmin : Page
    {

        public GestionExamenesAdmin()
        {
            InitializeComponent();
            mostrarExamenes();
            dtBuscarExamenPorFechaAdmin.Focus();
        }

        int examenid = 0;

        #region Mostrar las Consultas
        void mostrarExamenes()
        {
            using(SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    gridGestorExamenAdmin.ItemsSource = DatosExamenes.MostrarExamenes();
                    dtBuscarExamenPorFechaAdmin.Focus();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error al mostrar los Examenes: "
                        + ex.Message, "Error de Conexion", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion


        #region Evento GRID
        private void gridGestorExamenAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(gridGestorExamenAdmin.SelectedItem is ExamenesModel examenes)
            {
                //dtBuscarExamenPorFechaAdmin.Text = examenes.FechaExamen;
                examenid = examenes.ID;
            }
        }
        #endregion
    }
}
