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
    /// <summary>
    /// Lógica de interacción para GestionCitasAdmin.xaml
    /// </summary>
    public partial class GestionCitasAdmin : Page
    {
        public List<CitasModel> VerCitas { get; set; } // lista de citas

        public GestionCitasAdmin()
        {
            InitializeComponent();
            MostrarCitas(); // Llama al método para mostrar las citas al inicializar
            txtBuscarCitasAdmi.Focus();
        }


        // Variables
        int citaid = 0;



        #region Metodo Mostrar Citas
        // Método para mostrar las citas
        void MostrarCitas()
        {
            // Aquí se establece los valores
            int? pacienteID = null; // Obtener este valor de un control de entrada (ej. TextBox)
            int? medicoID = null; 
            int? especialidadID = null; 
            int? estadoCitaID = null; 
            DateTime? fechaCita = null; 

            // Se llama al método MostrarCitas y se pasa los parámetros
            VerCitas = DatosCitas.MostrarCitas(pacienteID, medicoID, especialidadID, estadoCitaID, fechaCita);

            // Asigna la lista de citas al DataGrid
            gridGestorCitaAdmin.ItemsSource = VerCitas;
        }
        #endregion


        #region Metodo Buscar Cita por DUI
        public void buscarCitaDUIPaciente()
        {
            string dui = txtBuscarCitasAdmi.Text;

            // Se verifica si el DUI esta vacio o es formato incorrecto
            if (string.IsNullOrWhiteSpace(dui))
            {
                MessageBox.Show("Ingresa un número de DUI válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                MostrarCitas();
                return;
            }


            // Se valida que el DUI tenga 10 digitos
            if (dui.Length != 10)
            {
                MessageBox.Show("El DUI debe contener exactamente 10 dígitos incluyendo '-'.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtBuscarCitasAdmi.Clear();
                return;
            }

            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    // Se llama al metodo de busqueda
                    var citasEncontradas = DatosCitas.BuscarCitasPorDUI(dui);
                    if (citasEncontradas != null && citasEncontradas.Count > 0)
                    {
                        gridGestorCitaAdmin.ItemsSource = citasEncontradas;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ninguna cita para el paciente con ese DUI.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtBuscarCitasAdmi.Clear();
                        MostrarCitas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar la cita: " + ex.Message, "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                    MostrarCitas();
                    return;
                }
            }
        }
        #endregion


        #region Boton para Buscar Cita
        private void btnBuscarCitaAdmi_Click(object sender, RoutedEventArgs e)
        {
            buscarCitaDUIPaciente();
        }
        #endregion


        #region Boton para Cancelar
        private void btnCancelarCitaAdmi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la búsqueda?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Se limpia
                txtBuscarCitasAdmi.Clear();

                MostrarCitas();
                return;
            }
        }

        #endregion



        #region Evento del GRID
        private void gridGestorCitaAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se verifica si hay algún elemento seleccionado
            if (gridGestorCitaAdmin.SelectedItem is CitasModel citaPorDUI)
            {
                // Se actualiza el txt del DUI
                txtBuscarCitasAdmi.Text = citaPorDUI.DUIPaciente;
                citaid = citaPorDUI.CitaID; // Cambiado para almacenar el ID de la receta
            }
        }


        #endregion


        #region Evento ENTER
        private void txtBuscarCitasAdmi_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                buscarCitaDUIPaciente();
            }
        }


        #endregion




        #region Evento ENTER del GRID
        private void gridGestorCitaAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                // Se obtiene la cita actual del Data Grid
                if(gridGestorCitaAdmin.SelectedItem is CitasModel citaSeleccionada)
                {
                    // Se actualiza el TextBox con el DUI de la cita seleccionada
                    txtBuscarCitasAdmi.Text = citaSeleccionada.DUIPaciente;

                    // Se ejecuta el metodo
                    buscarCitaDUIPaciente();
                }
            }
        }
        #endregion
    }
}