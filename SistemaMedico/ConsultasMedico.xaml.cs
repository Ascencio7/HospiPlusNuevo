using HospiPlus.DataAcces;
using HospiPlus.ModeloPaciente;
using HospiPlus.ServicePaciente;
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

namespace HospiPlus.SistemaMedico
{
    /// <summary>
    /// Lógica de interacción para ConsultasMedico.xaml
    /// </summary>
    public partial class ConsultasMedico : Page
    {
        public ConsultasMedico()
        {
            InitializeComponent();
            this.Loaded += ConsultasMedico_Loaded;
        }

        private void ConsultasMedico_Loaded(object sender, RoutedEventArgs e)
        {
            CargarConsultas();
        }

        private void CargarConsultas()
        {
            List<ModeloConsultaPaciente> consultas = ConsultaPaciente.ObtenerConsultas();
            gridGConsultM.ItemsSource = consultas;
        }

        #region Método para buscar consultas por DUI
        public void buscarConsultaDUIPaciente()
        {
            string dui = txtBuscarConsultM.Text;

            if (string.IsNullOrWhiteSpace(dui))
            {
                MessageBox.Show("Ingresa un número de DUI válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                CargarConsultas();
                return;
            }

            if (dui.Length != 10)
            {
                MessageBox.Show("El DUI debe contener exactamente 10 dígitos incluyendo '-'.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtBuscarConsultM.Clear();
                return;
            }

            using (var conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    var consultasEncontradas = ConsultaPaciente.BuscarConsultasPorDUI(dui);
                    if (consultasEncontradas != null && consultasEncontradas.Count > 0)
                    {
                        gridGConsultM.ItemsSource = consultasEncontradas;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ninguna consulta para el paciente con ese DUI.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtBuscarConsultM.Clear();
                        CargarConsultas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar la consulta: " + ex.Message, "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                    CargarConsultas();
                    return;
                }
            }
        }
        #endregion

        #region Botón para buscar consulta
        private void btnBuscarConsultM_Click(object sender, RoutedEventArgs e)
        {
            buscarConsultaDUIPaciente();
        }
        #endregion

        #region Botón para cancelar
        private void btnCancelarBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la búsqueda?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                txtBuscarConsultM.Clear();
                CargarConsultas();
            }
        }
        #endregion

        #region Evento ENTER
        private void txtBuscarConsultM_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buscarConsultaDUIPaciente();
            }
        }
        #endregion
    }
}