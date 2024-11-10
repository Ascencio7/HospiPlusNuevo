using HospiPlus.DataAcces;
using HospiPlus.ModeloMedico;
using HospiPlus.ServiceMedico;
using HospiPlus.ServicePaciente;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace HospiPlus.SistemaSecretario
{
    /// <summary>
    /// Lógica de interacción para ConsultasSecretario.xaml
    /// </summary>
    public partial class ConsultasSecretario : Page
    {
        public List<ConsultaPorFechaModel> Consultas { get; set; } // Lista para almacenar las consultas

        public ConsultasSecretario()
        {
            InitializeComponent();
            mostrarConsultasSecre();
            dtFechaConsultaSecre.Focus();
        }

        //DECLARACION DE VARIABLES
        int consultaSecreid = 0;



        #region METODOS PERSONALIZADOS

        // METODO PARA MOSTRAR LAS CONSULTAS
        void mostrarConsultasSecre()
        {
            Consultas = DatosConsultaPorFecha.MuestraConsulta();
            gridGestorConsultaSecre.ItemsSource = Consultas;
        }

        

        //METODO PARA BUSCAR CONSULTA POR FECHA
        
        void buscarConsultaPorFecha(DateTime fechaConsulta)
        {
            // Se llama al método que busca consultas por la fecha especificada
            var consultasFecha = DatosConsultaPorFecha.BuscarConsultasPorFecha(fechaConsulta);

            // Se verifica si se encontraron consultas
            if (consultasFecha.Count == 0)
            {
                MessageBox.Show("No se encontraron consultas.", "Sin Consultas", MessageBoxButton.OK, MessageBoxImage.Information);
                dtFechaConsultaSecre.SelectedDate = null;
                return;
            }
            else
            {
                // Se actualiza el ItemsSource del DataGrid con los resultados encontrados
                gridGestorConsultaSecre.ItemsSource = consultasFecha;
            }

        }
        #endregion FIN DE LOS METODOS PERSONALIZADOS



        //METODOS DEL FORMULARIO
        #region METODOS DEL FORMULARIO

        //btnBuscarConsulSecre
        private void btnBuscarConsulSecre_Click_1(object sender, RoutedEventArgs e)
        {
            // Asegurarse si hay una fecha seleccionada
            if (dtFechaConsultaSecre.SelectedDate.HasValue)
            {
                buscarConsultaPorFecha(dtFechaConsultaSecre.SelectedDate.Value);
            }
            else
            {
                // Se limpia el DatePicker
                dtFechaConsultaSecre.SelectedDate = null;
                mostrarConsultasSecre();
                return;
            }
        }


        private void btnCancelarConsulSecre_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la búsqueda?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dtFechaConsultaSecre.SelectedDate = null;
                mostrarConsultasSecre();
                return;
            }
        }
        #endregion



        #region Evento GRID
        private void gridGestorConsultaSecre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se verifica si hay algún elemento seleccionado
            if (gridGestorConsultaSecre.SelectedItem is ConsultaPorFechaModel consultaPorFecha)
            {
                // Se actualiza el DatePicker de la fecha de consulta
                dtFechaConsultaSecre.SelectedDate = consultaPorFecha.FechaConsulta.Date; // Cambiado a SelectedDate
                consultaSecreid = consultaPorFecha.ConsultaID;
            }

        }
        #endregion



        #region Evento ENTER en el Grid
        private void gridGestorConsultaSecre_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                // Se verifica si hay una dato seleccionado en el grid
                if(gridGestorConsultaSecre.SelectedItem is ConsultaPorFechaModel consulPorFechaSecre)
                {
                    // Se obtiene la fecha de la consulta seleccionada
                    DateTime fechaConsulSelecSecre = consulPorFechaSecre.FechaConsulta;

                    // Llamada al método para buscar las consultas
                    buscarConsultaPorFecha(fechaConsulSelecSecre);
                }
            }
        }

        #endregion

        

        private void dtFechaConsultaSecre_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Se verifica si hay una fecha seleccionada
                if (dtFechaConsultaSecre.SelectedDate.HasValue)
                {
                    // Se llama al método de búsqueda con la fecha seleccionada
                    buscarConsultaPorFecha(dtFechaConsultaSecre.SelectedDate.Value);
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una fecha válida.", "Fecha Incorrecta", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

}
