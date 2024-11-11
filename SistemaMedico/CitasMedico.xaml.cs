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

namespace HospiPlus.SistemaMedico
{
    /// <summary>
    /// Lógica de interacción para CitasMedico.xaml
    /// </summary>
    public partial class CitasMedico : Page
    {
        public List<CitasModel> VerCitas { get; set; } 

        public CitasMedico()
        {
            InitializeComponent();
            MostrarCitas(); 
            //txtBuscarCitasMedico.Focus();
        }

        
        int citaid = 0;

        #region Metodo Mostrar Citas
       
        void MostrarCitas()
        {
            int? pacienteID = null;
            int? medicoID = null;
            int? especialidadID = null;
            int? estadoCitaID = null;
            DateTime? fechaCita = null;

            
            VerCitas = DatosCitas.MostrarCitas(pacienteID, medicoID, especialidadID, estadoCitaID, fechaCita);

          
            gridGestorCitaMedico.ItemsSource = VerCitas;
        }
        #endregion

        #region Metodo Buscar Cita por DUI
         public void buscarCitaDUIPaciente()
         {
             string dui = txtBuscarCitasMedico.Text;

             
             if (string.IsNullOrWhiteSpace(dui))
             {
                 MessageBox.Show("Ingresa un número de DUI válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                 MostrarCitas();
                 return;
             }

             
             if (dui.Length != 10)
             {
                 MessageBox.Show("El DUI debe contener exactamente 10 dígitos incluyendo '-'.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                 txtBuscarCitasMedico.Clear();
                 return;
             }

             using (SqlConnection conexion = ConexionDB.ObtenerCnx())
             {
                 try
                 {
                     ConexionDB.AbrirConexion(conexion);

                   
                     var citasEncontradas = DatosCitas.BuscarCitasPorDUI(dui);
                     if (citasEncontradas != null && citasEncontradas.Count > 0)
                     {
                         gridGestorCitaMedico.ItemsSource = citasEncontradas;
                     }
                     else
                     {
                         MessageBox.Show("No se encontró ninguna cita para el paciente con ese DUI.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                         txtBuscarCitasMedico.Clear();
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
        private void btnBuscarCitaMedico_Click(object sender, RoutedEventArgs e)
        {
            buscarCitaDUIPaciente();
        }
        #endregion
        

        
        #region Boton para Cancelar
        private void btnCancelarCitaMedico_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la búsqueda?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                txtBuscarCitasMedico.Clear();

                MostrarCitas();
                return;
            }
        }

        #endregion
        

        
        #region Evento del GRID
        private void gridGestorCitaMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (gridGestorCitaMedico.SelectedItem is CitasModel citaPorDUI)
            {
                
                txtBuscarCitasMedico.Text = citaPorDUI.DUIPaciente;
                citaid = citaPorDUI.CitaID;
            }
        }
        #endregion

        #region Evento ENTER
        private void txtBuscarCitasMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buscarCitaDUIPaciente();
            }
        }
        #endregion

        

        
        #region Evento ENTER del GRID
        private void gridGestorCitaMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               
                if (gridGestorCitaMedico.SelectedItem is CitasModel citaSeleccionada)
                {
                    
                    txtBuscarCitasMedico.Text = citaSeleccionada.DUIPaciente;

                    
                    buscarCitaDUIPaciente();
                }
            }
        }
        #endregion
        
    }
}

