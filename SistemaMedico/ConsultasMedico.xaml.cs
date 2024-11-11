using HospiPlus.DataAcces;
using HospiPlus.ModeloPaciente;
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


        private void CargarComboBoxes()
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);

                    // Cargar Médicos
                    SqlCommand cmdMedico = new SqlCommand("SELECT MedicoID, NombreMedico FROM Medicos WHERE EstadoID = 1", conexion);
                    SqlDataReader drMedico = cmdMedico.ExecuteReader();
                    while (drMedico.Read())
                    {
                        cmbMedicoIDMedico.Items.Add(new ComboBoxItem()
                        {
                            Content = $"{drMedico["MedicoID"]} -> {drMedico["NombreMedico"]}",
                            Tag = drMedico["MedicoID"]
                        });
                    }
                    drMedico.Close();

                    

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        

        #region Botón para cancelar
        private void btnCancelarBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la búsqueda?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //txtBuscarConsultM.Clear();
                CargarConsultas();
            }
        }
        #endregion

        private void cmbMedicoIDMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarComboBoxes();
        }


    }
}