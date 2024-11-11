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



        #region Validar Formulario
        bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;

            // Validación de la fecha de examen
            if (!dtBuscarExamenPorFechaAdmin.SelectedDate.HasValue)
            {
                estado = false;
                mensaje += "Fecha Examen\n";
            }

            // MENSAJE
            if (!estado)
            {
                MessageBox.Show("Debe llenar el campo:\n" + mensaje,
                "Validación de Formulario",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            return estado;
        }
        #endregion



        #region Mostrar los Examenes
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
            if (gridGestorExamenAdmin.SelectedItem is ExamenesModel examenes)
            {
                dtBuscarExamenPorFechaAdmin.Text = examenes.FechaExamen.ToString("yyyy-MM-dd");
                examenid = examenes.ID;
            }
        }
        #endregion



        #region Metodo Buscar Examen por fecha
        void buscarExamenPorFecha(DateTime fechaExamen)
        {
            ValidarFormulario();
            var examenesFecha = DatosBuscarExamenPorFechaExamen.BuscarExamenPorFecha(fechaExamen);

            // Si no hay exámenes para la fecha seleccionada, muestra un mensaje
            if (examenesFecha.Count == 0)
            {
                MessageBox.Show("No se encontraron exámenes.", "Sin Exámenes", MessageBoxButton.OK, MessageBoxImage.Information);
                mostrarExamenes();
            }
            else
            {
                // Si se encuentran exámenes los carga en la cuadrícula
                gridGestorExamenAdmin.ItemsSource = examenesFecha;
            }
        }
        #endregion


        #region Botón para Buscar Examen
        private void btnBuscarExamenAdmi_Click(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();
            if (dtBuscarExamenPorFechaAdmin.SelectedDate.HasValue)
            {
                buscarExamenPorFecha(dtBuscarExamenPorFechaAdmin.SelectedDate.Value);
            }
            else
            {
                dtBuscarExamenPorFechaAdmin.SelectedDate = null;
                mostrarExamenes();
            }
        }
        #endregion


        #region Botón de Cancelar
        private void btnCancelarExamenAdmi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar la búsqueda?", "Limpiar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dtBuscarExamenPorFechaAdmin.Text = string.Empty;

                mostrarExamenes();
                return;
            }
        }

        #endregion


        #region Evento ENTER del Grid
        private void gridGestorExamenAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (gridGestorExamenAdmin.SelectedItem is ExamenesModel examenSeleccionado)
                {
                    DateTime fechaExamenSeleccionada = examenSeleccionado.FechaExamen;
                    buscarExamenPorFecha(fechaExamenSeleccionada);
                }
            }
        }
        #endregion


        #region Evento ENTER para el DatePicker
        private void dtBuscarExamenPorFechaAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Validar el formulario
                if (ValidarFormulario())
                {
                    // Solo busca el examen si la validación es correcta
                    DateTime fechaSeleccionada = dtBuscarExamenPorFechaAdmin.SelectedDate.Value;
                    buscarExamenPorFecha(fechaSeleccionada);
                }
            }
        }


        #endregion

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SistemaMedico/ExamenesMedico.xaml", UriKind.Relative));

        }
    }
}
