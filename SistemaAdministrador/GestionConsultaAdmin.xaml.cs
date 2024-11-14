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

namespace HospiPlus.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para GestionConsultaAdmin.xaml
    /// </summary>
    public partial class GestionConsultaAdmin : Page
    {
        public GestionConsultaAdmin()
        {
            InitializeComponent();
            CargarConsultas();
        }


        #region Cargar las Consultas
        void CargarConsultas()
        {
            try
            {
                List<ModeloConsultaPaciente> consultas = new List<ModeloConsultaPaciente>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "MostrarConsultas";

                        using (DbDataReader leertabla = command.ExecuteReader())
                        {
                            while (leertabla.Read())
                            {
                                ModeloConsultaPaciente consulta = new ModeloConsultaPaciente()
                                {
                                    CitaID = leertabla.GetInt32(0),
                                    ConsultaID = int.Parse(leertabla["ConsultaID"].ToString()),
                                    PacienteID = leertabla["NombreCompletoPaciente"].ToString(),
                                    MedicoID = leertabla["NombreCompletoMedico"].ToString(),
                                    EspecialidadM = leertabla["EspecialidadMedico"].ToString(),
                                    Altura = decimal.Parse(leertabla["Altura"].ToString()),
                                    Peso = decimal.Parse(leertabla["Peso"].ToString()),
                                    Alergia = leertabla["Alergia"].ToString(),
                                    Sintomas = leertabla["Sintomas"].ToString(),
                                    Diagnostico = leertabla["Diagnostico"].ToString(),
                                    Observaciones = leertabla["Observaciones"].ToString(),
                                    FechaConsulta = DateTime.Parse(leertabla["FechaConsulta"].ToString()),
                                    EstadoCita = leertabla["EstadoCita"].ToString()
                                };

                                consultas.Add(consulta);
                            }
                        }
                    }
                }
                gridConsultasAdmin.ItemsSource = consultas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las consultas: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion








        #region Ir para agregar consultas desde Medico
        private void btnAgregarConsultaAdmin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("SistemaMedico/ConsultasMedico.xaml", UriKind.Relative));

        }
        #endregion



        #region Limpiar Campo
        void LimpiarCampo()
        {
            dtFechaConsultaAdmin.Text = "";
        }
        #endregion



        #region Boton para cancelar
        private void btnCancelarBusquedaAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea cancelar la búsqueda?", "HOSPI PLUS | Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarCampo();
            }
        }
        #endregion
    
    
    }
}