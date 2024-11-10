using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using HospiPlus.DataAcces;
using HospiPlus.ModeloPaciente;
using HospiPlus.ServicePaciente;


namespace HospiPlus.SistemaMedico
{
    /// <summary>
    /// Lógica de interacción para GestionPacienteMedico.xaml
    /// </summary>
    public partial class GestionPacienteMedico : Page
    {
        public GestionPacienteMedico()
        {
            InitializeComponent();
            CargarPacientes();
        }

        private void CargarPacientes()
        {
            try
            {
                List<PacientesModel> pacientes = DatosPaciente.MuestraPacientes();
                gridGestorPacienteMedico.ItemsSource = pacientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pacientes: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        void LimpiarCampos()
        {
            txtNombrePacienteMedic.Text = string.Empty;
            txtApellidoPacienteMedic.Text = string.Empty;
            txtApellidoCasadaPacienteMedic.Text = string.Empty;
            cmbEstadoCivilPacienteMedic.SelectedIndex = -1;
            cmbSexoPacienteMedic.SelectedIndex = -1;
            cmbEstadoPacienteMedic.SelectedIndex = -1;
            cmbDepartamentoPacienteMedic.SelectedIndex = -1;
            cmbMunicipioPacienteMedic.SelectedIndex = -1;
            dtFechaNPacienteMedic.Text = string.Empty;
            txtDUIPacienteMedic.Text = string.Empty;
            txTelefonoPacienteMedic.Text = string.Empty;
            txtCorreoPacienteMedic.Text = string.Empty;

        }

        //METODO PARA AGREGAR PACIENTE
        private void AgregarPaciente()
        {
            CargarPacientes();
            if (MessageBox.Show("Esta seguro de que quiere insertar los datos?", "Validacion || Hospi Plus", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    try
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (SqlCommand command = new SqlCommand("InsertarPaciente", conexion))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@NombrePaciente", txtNombrePacienteMedic.Text);
                            command.Parameters.AddWithValue("@ApellidoPaciente", txtApellidoPacienteMedic.Text);
                            command.Parameters.AddWithValue("@ApellidoDeCasada", txtApellidoCasadaPacienteMedic.Text);
                            command.Parameters.AddWithValue("@FechaNacimiento", DateTime.Parse(dtFechaNPacienteMedic.Text));
                            command.Parameters.AddWithValue("SexoID", Convert.ToInt32(((ComboBoxItem)cmbSexoPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoCivilID", Convert.ToInt32(((ComboBoxItem)cmbEstadoCivilPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@DUIPaciente", txtDUIPacienteMedic.Text);
                            command.Parameters.AddWithValue("@TelefonoPaciente", txTelefonoPacienteMedic.Text);
                            command.Parameters.AddWithValue("@CorreoPaciente", txtCorreoPacienteMedic.Text);
                            command.Parameters.AddWithValue("@DepartamentosID", Convert.ToInt32(((ComboBoxItem)cmbDepartamentoPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@MunicipioID", Convert.ToInt32(((ComboBoxItem)cmbMunicipioPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoID", Convert.ToInt32(((ComboBoxItem)cmbEstadoPacienteMedic.SelectedItem).Tag));

                            var result = command.ExecuteScalar();
                            MessageBox.Show("Paciente con ID: " + result + " insertado correctamente", "Exito || HospiPlus", MessageBoxButton.OK, MessageBoxImage.Information);
                            CargarPacientes();
                            LimpiarCampos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al insertar el paciente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        ConexionDB.CerrarConexion(conexion);
                    }
                }
            }
        }
        private void btnAgregarPacienteMedic_Click(object sender, RoutedEventArgs e)
            {
                AgregarPaciente();
            }

        private void gridGestorPacienteMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridGestorPacienteMedico.SelectedItem is PacientesModel paciente)
            {
                // Cargar datos del paciente en los TextBox
                txtNombrePacienteMedic.Text = paciente.NombrePaciente;
                txtApellidoPacienteMedic.Text = paciente.ApellidoPaciente;
                txtApellidoCasadaPacienteMedic.Text = paciente.ApellidoDeCasada;
                dtFechaNPacienteMedic.SelectedDate = paciente.FechaNacimientoPaciente;
                cmbSexoPacienteMedic.Text = paciente.SexoPaciente;
                cmbEstadoCivilPacienteMedic.Text = paciente.EstadoCivilPaciente;
                txtDUIPacienteMedic.Text = paciente.DUIPaciente;
                txTelefonoPacienteMedic.Text = paciente.TelefonoPaciente;
                txtCorreoPacienteMedic.Text = paciente.CorreoPaciente;
                cmbDepartamentoPacienteMedic.Text = paciente.DepartamentosPaciente;
                cmbMunicipioPacienteMedic.Text = paciente.MunicipioPaciente;
                cmbEstadoPacienteMedic.Text = paciente.EstadoPaciente;

                // Guardar el ID del paciente para actualizar
                txtNombrePacienteMedic.Tag = paciente.PacienteID;
            }
        }
    }
}
