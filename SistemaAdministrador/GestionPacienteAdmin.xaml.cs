using HospiPlus.ModeloPaciente;
using HospiPlus.DataAcces;


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
using HospiPlus.ModeloMedico;
using HospiPlus.ServiceMedico;
using HospiPlus.SistemaMedico;
using HospiPlus.ServicePaciente;
using HospiPlus.SistemaSecretario;
using System.Security.Principal;
using System.Data.SqlClient;


namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para GestionPacienteAdmin.xaml
    /// </summary>
    public partial class GestionPacienteAdmin : Page
    {
        // Instanciando a las paginas
        //loginDiegoP login = new loginDiegoP();
        //InicioAdministrador inicioAdministrador = new InicioAdministrador();
        //SistemSecretario sistemSecretario = new SistemSecretario();
        //SistemMedico sistemMedico = new SistemMedico();
        GestionPacienteSecretario gestionPacienteSecretario = new GestionPacienteSecretario();
        public GestionPacienteAdmin()
        {
            InitializeComponent();
            MostrarPacientes();
            
        }


        //Declaracion de variables
        int pacienteid = 0;


        #region Método para Mostrar los datos Pacientes
        // Método para conectar y mostrar los registros desde SQL al DataGrid
        void MostrarPacientes()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    gridGestorPacienteAdmin.ItemsSource = DatosPaciente.MuestraPacientes();
                    txtBuscarPacientesAdmi.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al mostrar los pacientes: "
                        + ex.Message, "Error de Conexion", MessageBoxButton.OK, MessageBoxImage.Error);
                
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion

        void agregarPaciente()
        {
            
        }


        // Es el boton que borre XD
        private void btnModificarPacienteAdmi_Click(object sender, RoutedEventArgs e)
        {
            // En este boton la accion de ir a la otra page "Gestion Paciente" pero Secretario
            NavigationService.Navigate(new Uri("SistemaSecretario/GestionPacienteSecretario.xaml", UriKind.Relative));
        }
        //



        #region Método para buscar al paciente por DUI
        // Método que buscará al paciente según el número de DUI ingresado
        public void buscarPacienteDUI()
        {
            string dui = txtBuscarPacientesAdmi.Text;
            if (string.IsNullOrWhiteSpace(dui))
            {
                MessageBox.Show("Por favor, ingresa un número de DUI válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                MostrarPacientes();
                return;
            }

            if (dui.Length != 10)
            {
                MessageBox.Show("El DUI debe contener exactamente 10 dígitos incluyendo '-'.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtBuscarPacientesAdmi.Clear();
                return;
            }

            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    // Se llama al metodo que ejecuta el procedimiento almacenado
                    var pacientesEncontrados = DatosPaciente.BuscarPacientePorDUI(dui);


                    if (pacientesEncontrados != null && pacientesEncontrados.Count > 0)
                    {
                        gridGestorPacienteAdmin.ItemsSource = pacientesEncontrados;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún paciente con ese DUI.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtBuscarPacientesAdmi.Clear(); // O puedes usar txtBuscarPacientesAdmi.Text = "";
                        MostrarPacientes();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el paciente: " + ex.Message, "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtBuscarPacientesAdmi.Clear(); // O puedes usar txtBuscarPacientesAdmi.Text = "";
                    MostrarPacientes();
                    return;
                }
            }
        }
        #endregion



        #region Botón para buscar al paciente
        private void btnBuscarPacientePorDUIAdmi_Click(object sender, RoutedEventArgs e)
        {
            buscarPacienteDUI(); // Llamo al método
        }
        #endregion




        #region Botón para Limpiar los campos
        // Limpia donde se ingresa el número de DUI del Paciente y actualiza con todos los registros de la tabla Pacientes
        private void btnLimpiarPacienteAdmi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar la búsqueda?", "Limpiar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Limpia el TextBox donde se ingresa el DUI
                txtBuscarPacientesAdmi.Clear(); // O puedes usar txtBuscarPacientesAdmi.Text = "";

                // Llama al método para mostrar todos los pacientes
                MostrarPacientes();
                return; // Salir del método
            }
        }
        #endregion



        #region Evento del Grid
        private void gridGestorPacienteAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si hay algún elemento seleccionado
            if (gridGestorPacienteAdmin.SelectedItem is PacientesModel pacientes)
            {
                // Actualiza el TextBox con el DUI del paciente seleccionado
                txtBuscarPacientesAdmi.Text = pacientes.DUIPaciente;

                // Guarda el ID del paciente seleccionado
                pacienteid = pacientes.PacienteID;
            }
        }
        #endregion



        #region Evento ENTER
        // Evento de enter para buscar al paciente

        private void txtBuscarPacientesAdmi_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buscarPacienteDUI();
            }
        }
        #endregion


        #region Evento ENTER del Grid
        private void gridGestorPacienteAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                // Se obtiene el paciente actual en el data grid
                if(gridGestorPacienteAdmin.SelectedItem is PacientesModel pacientes)
                {
                    // Se actualiza el textbox de DUI con el DUI del paciente
                    txtBuscarPacientesAdmi.Text = pacientes.DUIPaciente;

                    buscarPacienteDUI();
                }
            }
        }
        #endregion

        private void btnAgregarPacienteAdmi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SistemaSecretario/GestionPacienteSecretario.xaml", UriKind.Relative));

        }
    }
}