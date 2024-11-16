using HospiPlus.DataAcces;
using HospiPlus.ModeloMedico;
using HospiPlus.ModeloPaciente;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using HospiPlus.ServiceMedico;
using HospiPlus.ServicePaciente;
using System.Security.Principal;
using System.Data.SqlClient;



namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para GestionConsultaAdmin.xaml
    /// </summary>
    public partial class GestionConsultaAdmin : Page
    {
        public List<ConsultaPorFechaModel> Consultas { get; set; } // Lista para almacenar las consultas

        public GestionConsultaAdmin()
        {
            InitializeComponent();
            MostrarConsultas();
        }


        int consultaid = 0;


        #region Cargar las Consultas
        void MostrarConsultas()
        {
            Consultas = DatosConsultaPorFecha.MuestraConsulta();
            gridConsultasAdmin.ItemsSource = Consultas;
        }
        #endregion



        #region Metodo Buscar Consulta por Fecha

        void buscarPorFechaConsulta(DateTime fechaConsulta)
        {
            var consultasFecha = DatosConsultaPorFecha.BuscarConsultasPorFecha(fechaConsulta);

            if(consultasFecha.Count == 0)
            {
                MessageBox.Show("No se encontraron consultas.", "HOSPI PLUS | Sin Consultas", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                gridConsultasAdmin.ItemsSource = consultasFecha;
            }
        }

        #endregion



        #region Ir para agregar consultas desde Medico
        private void btnAgregarConsultaAdmin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SistemaMedico/ConsultasMedico.xaml", UriKind.Relative));

        }
        #endregion






        #region Boton para cancelar
        private void btnCancelarBusquedaAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea cancelar la búsqueda?", "HOSPI PLUS | Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dtFechaConsultaAdmin.SelectedDate = null;
                MostrarConsultas();
                return;
            }
        }

        #endregion


        private void btnBuscarConsultaPorFechaAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (dtFechaConsultaAdmin.SelectedDate.HasValue)
            {
                buscarPorFechaConsulta(dtFechaConsultaAdmin.SelectedDate.Value);
            }
            else
            {
                dtFechaConsultaAdmin.SelectedDate = null;

                MostrarConsultas();
                return;
            }
        }

        private void dtFechaConsultaAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (dtFechaConsultaAdmin.SelectedDate.HasValue)
                {
                    buscarPorFechaConsulta(dtFechaConsultaAdmin.SelectedDate.Value);
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una fecha válida.", "HOSPI PLUS | Fecha Incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void gridConsultasAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(gridConsultasAdmin.SelectedItem is ConsultaPorFechaModel consultaPorFecha)
                {
                    DateTime fechaConsultaSeleccionada = consultaPorFecha.FechaConsulta;

                    buscarPorFechaConsulta(fechaConsultaSeleccionada);
                }
            }
        }

        private void gridConsultasAdmin_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // Se verifica si hay algún elemento seleccionado
            if (gridConsultasAdmin.SelectedItem is ConsultaPorFechaModel consultaPorFecha)
            {
                // Se actualiza el DatePicker de la fecha de consulta
                dtFechaConsultaAdmin.SelectedDate = consultaPorFecha.FechaConsulta.Date; // Cambiado a SelectedDate
                consultaid = consultaPorFecha.ConsultaID;
            }
        }
    }
}