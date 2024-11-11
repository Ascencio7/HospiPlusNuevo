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
    /// Lógica de interacción para GestionRecetasAdmin.xaml
    /// </summary>
    public partial class GestionRecetasAdmin : Page
    {
        public List<RecetasModel> Recetas { get; set; }
        public GestionRecetasAdmin()
        {
            InitializeComponent();
            MostrarRecetas();
            txbBuscarRecetasAdmi.Focus();
        }


        // Variablke
        int recetaid = 0;

        #region Mostrar Recetas

        void MostrarRecetas()
        {
            Recetas = DatosRecetas.MostrarRecetas();
            gridGestorRecetasAdmin.ItemsSource = Recetas;
        }
        #endregion


        #region Metodo Buscar Receta por DUI


        // Metodo que busca la receta por el DUI del Paciente
        public void buscarRecetaPorDUIPaciente()
        {
            string dui = txbBuscarRecetasAdmi.Text;

            // Verifica si el DUI está vacío o tiene un formato incorrecto
            if (string.IsNullOrWhiteSpace(dui))
            {
                MessageBox.Show("Ingresa un número de DUI válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                MostrarRecetas();
                return;
            }

            // Valida que el DUI tenga exactamente 10 caracteres
            if (dui.Length != 10)
            {
                MessageBox.Show("El DUI debe contener exactamente 10 dígitos incluyendo '-'.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txbBuscarRecetasAdmi.Clear();
                return;
            }

            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    // Se llama al método de búsqueda
                    var recetasEncontradas = DatosRecetas.BuscarRecetasPorDUI(dui);

                    if (recetasEncontradas != null && recetasEncontradas.Count > 0)
                    {
                        gridGestorRecetasAdmin.ItemsSource = recetasEncontradas;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ninguna receta para el paciente con ese DUI.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        txbBuscarRecetasAdmi.Clear();
                        MostrarRecetas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar la receta: " + ex.Message, "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                    MostrarRecetas();
                    return;
                }
            }
        }
        #endregion




        #region Boton para Buscar
        private void btnBuscarRecetasAdmi_Click(object sender, RoutedEventArgs e)
        {
            buscarRecetaPorDUIPaciente(); // Se llama al metodo
        }
        #endregion



        #region Boton para Cancelar
        private void btnCancelarRecetasAdmi_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea cancelar la búsqueda?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Se limpia
                txbBuscarRecetasAdmi.Clear();

                MostrarRecetas();
                return;
            }
        }
        #endregion



        #region Evento del Grid
        private void gridGestorRecetasAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Se verifica si hay algún elemento seleccionado
            if (gridGestorRecetasAdmin.SelectedItem is RecetasModel recetaPorDUI)
            {
                // Se actualiza el txt del DUI
                txbBuscarRecetasAdmi.Text = recetaPorDUI.DuiPaciente;
                recetaid = recetaPorDUI.RecetaID; // Cambiado para almacenar el ID de la receta
            }
        }
        #endregion


        #region Evento Enter
        private void txbBuscarRecetasAdmi_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                buscarRecetaPorDUIPaciente();
            }
            
        }
        #endregion

        #region Botón para Limpiar
        private void btnLimpiarRecetasAdmi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar la búsqueda?", "Limpiar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Se limpia el TextBox
                txbBuscarRecetasAdmi.Clear();
                MostrarRecetas();
                return;
            }
        }
        #endregion



        #region Evento ENTER del GRID
        private void gridGestorRecetasAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            // Verifica si se ha presionado la tecla Enter
            if (e.Key == Key.Enter)
            {
                // Obtén la receta seleccionada actualmente en el DataGrid
                if (gridGestorRecetasAdmin.SelectedItem is RecetasModel recetaSeleccionada)
                {
                    // Actualiza el TextBox de DUI con el DUI de la receta seleccionada
                    txbBuscarRecetasAdmi.Text = recetaSeleccionada.DuiPaciente;

                    // Ejecuta el método de búsqueda o cualquier otra acción necesaria
                    buscarRecetaPorDUIPaciente();
                }
            }
        }

        #endregion

        private void btnAgregarAdmiRecetas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SistemaMedico/RecetasMedico.xaml", UriKind.Relative));

        }
    }
}
