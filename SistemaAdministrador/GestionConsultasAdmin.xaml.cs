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

// Habilita mis clases
using HospiPlus.ModeloMedico;
using HospiPlus.ServiceMedico;
using HospiPlus.ModeloPaciente;
using HospiPlus.ServicePaciente;


using System.Security.Principal;
using System.Data.SqlClient;
using HospiPlus.DataAcces;



namespace HospiPlus.SistemaAdministrador
{
    public partial class GestionConsultasAdmin : Page
    {
        public List<ConsultaPorFechaModel> Consultas { get; set; } // Lista para almacenar las consultas

        public GestionConsultasAdmin()
        {
            InitializeComponent();
            MostrarConsultas();
            dtBuscarCitasAdmi.Focus();
        }

        // Declaración de variables
        int consultaid = 0;



        #region Mostrar las Consultas
        void MostrarConsultas()
        {
            Consultas = DatosConsultaPorFecha.MuestraConsulta();
            gridConsultaAdmin.ItemsSource = Consultas;
        }
        #endregion



        #region Método Buscar Consulta por Fecha
        void buscarPorFechaConsulta(DateTime fechaConsulta)
        {
            // Se llama al método que busca consultas por la fecha especificada
            var consultasFecha = DatosConsultaPorFecha.BuscarConsultasPorFecha(fechaConsulta);

            // Se verifica si se encontraron consultas
            if (consultasFecha.Count == 0)
            {
                MessageBox.Show("No se encontraron consultas.", "Sin Consultas", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Se actualiza el ItemsSource del DataGrid con los resultados encontrados
                gridConsultaAdmin.ItemsSource = consultasFecha;
            }
        }
        #endregion



        #region Evento del Grid
        private void gridConsultaAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se verifica si hay algún elemento seleccionado
            if (gridConsultaAdmin.SelectedItem is ConsultaPorFechaModel consultaPorFecha)
            {
                // Se actualiza el DatePicker de la fecha de consulta
                dtBuscarCitasAdmi.SelectedDate = consultaPorFecha.FechaConsulta.Date; // Cambiado a SelectedDate
                consultaid = consultaPorFecha.ConsultaID;
            }
        }
        #endregion







        #region Botón para Buscar Consulta
        private void btnBuscarConsultaAdmi_Click(object sender, RoutedEventArgs e)
        {
            // Se asegura de que hay una fecha seleccionada antes de llamar a buscarPorFechaConsulta()
            if (dtBuscarCitasAdmi.SelectedDate.HasValue)
            {
                buscarPorFechaConsulta(dtBuscarCitasAdmi.SelectedDate.Value);
            }
            else
            {
                // Se limpia el DatePicker
                dtBuscarCitasAdmi.SelectedDate = null; // Cambiado a SelectedDate

                MostrarConsultas();
                return;
            }
        }
        #endregion



        #region Botón de Cancelar
        private void btnCancelarConsultaAdmi_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("¿Desea cancelar la búsqueda?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Se limpia el DatePicker
                dtBuscarCitasAdmi.SelectedDate = null; // Cambiado a SelectedDate
                MostrarConsultas();
                return;
            }
        }
        #endregion




        #region Evento ENTER
        private void dtBuscarCitasAdmi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Se verifica si hay una fecha seleccionada
                if (dtBuscarCitasAdmi.SelectedDate.HasValue)
                {
                    // Se llama al método de búsqueda con la fecha seleccionada
                    buscarPorFechaConsulta(dtBuscarCitasAdmi.SelectedDate.Value);
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una fecha válida.", "Fecha Incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion


        #region Evento ENTER en DataGrid
        private void gridConsultaAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Verifica si hay una consulta seleccionada en el DataGrid
                if (gridConsultaAdmin.SelectedItem is ConsultaPorFechaModel consultaPorFecha)
                {
                    // Obtiene la fecha de la consulta seleccionada
                    DateTime fechaConsultaSeleccionada = consultaPorFecha.FechaConsulta;

                    // Llama al método para buscar consultas por la fecha seleccionada
                    buscarPorFechaConsulta(fechaConsultaSeleccionada);
                }
            }
        }
        #endregion

        private void btnAgregarConsultaAdmi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SistemaSecretario/ConsultasSecretario.xaml", UriKind.Relative));

        }
    }
}
