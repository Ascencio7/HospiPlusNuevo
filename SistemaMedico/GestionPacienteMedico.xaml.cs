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
using HospiPlus.ModeloMedico;


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


        #region Cargar Departamentos
        public void cargarDepartamentosPacientes()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (SqlCommand command = new SqlCommand("ObtenerDepartamentos", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();

                        cmbDepartamentoPacienteMedic.Items.Clear();
                        while (reader.Read())
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = reader["NombreDepartamento"].ToString();
                            item.Tag = reader["DepartamentosID"];
                            cmbDepartamentoPacienteMedic.Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar departamentos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion

        #region Cargar Municipios
        public void cargarMunicipios(int departamentoId)
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (SqlCommand command = new SqlCommand("ObtenerMunicipiosPorDepartamento", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DepartamentosID", departamentoId);
                        SqlDataReader reader = command.ExecuteReader();

                        cmbMunicipioPacienteMedic.Items.Clear();
                        while (reader.Read())
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = reader["NombreMunicipio"].ToString();
                            item.Tag = reader["MunicipioID"];
                            cmbMunicipioPacienteMedic.Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar municipios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion

        #region Evento SelectionChanged para Departamentos
        private void cmbDepartamentoPacienteMedic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDepartamentoPacienteMedic.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Tag.ToString(), out int departamentoId))
                {
                    cargarMunicipios(departamentoId);
                }
                else
                {
                    MessageBox.Show("Error al obtener el ID del departamento. Verifique el valor en el ComboBox.",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        #endregion

        #region Validaciones
        private bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;

            if (string.IsNullOrWhiteSpace(txtNombrePacienteMedic.Text))
            {
                estado = false;
                mensaje += "Nombre\n";
            }

            if (string.IsNullOrWhiteSpace(txtApellidoPacienteMedic.Text))
            {
                estado = false;
                mensaje += "Apellido\n";
            }

            if (string.IsNullOrWhiteSpace(dtFechaNPacienteMedic.Text))
            {
                estado = false;
                mensaje += "Fecha de nacimiento\n";
            }

            if (string.IsNullOrWhiteSpace(txtDUIPacienteMedic.Text))
            {
                estado = false;
                mensaje += "DUI\n";
            }

            if (string.IsNullOrWhiteSpace(txTelefonoPacienteMedic.Text))
            {
                estado = false;
                mensaje += "Teléfono\n";
            }

            if (string.IsNullOrWhiteSpace(txtCorreoPacienteMedic.Text))
            {
                estado = false;
                mensaje += "Correo\n";
            }

            if (cmbSexoPacienteMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Sexo\n";
            }

            if (cmbEstadoCivilPacienteMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Estado Civil\n";
            }

            if (cmbDepartamentoPacienteMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Departamento\n";
            }

            if (cmbMunicipioPacienteMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Municipio\n";
            }

            if (cmbEstadoPacienteMedic.SelectedItem == null)
            {
                estado = false;
                mensaje += "Estado\n";
            }

            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos:\n" + mensaje,
                                "Validación de Formulario",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            return estado;
        }

        private void txtDUIPacienteMedic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtDUIPacienteMedic.Text.Length > 10)
            {
                txtDUIPacienteMedic.Text = txtDUIPacienteMedic.Text.Substring(0, 10);
                txtDUIPacienteMedic.CaretIndex = txtDUIPacienteMedic.Text.Length;
                MessageBox.Show("El DUI debe tener 10 caracteres, incluyendo '-'.",
                    "DUI Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        private void btnCancelarPacienteMedic_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = MessageBox.Show("¿Cancelar la operacion?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (respuesta == MessageBoxResult.Yes)
            {
                LimpiarCampos();
                CargarPacientes();
            }
        }


        private void gridGestorPacienteMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PacientesModel pacientes = (PacientesModel)gridGestorPacienteMedico.SelectedItem;
            if (pacientes == null)
            {
                return; // No hay médico seleccionado, salimos del método
            }

            // Asignación de valores a los TextBox
            txtNombrePacienteMedic.Text = pacientes.NombrePaciente;
            txtApellidoPacienteMedic.Text = pacientes.ApellidoPaciente;
            txtApellidoCasadaPacienteMedic.Text = pacientes.ApellidoDeCasada;
            dtFechaNPacienteMedic.Text = pacientes.FechaNacimientoPaciente.ToString("yyyy-MM-dd");
            txTelefonoPacienteMedic.Text = pacientes.TelefonoPaciente;
            cmbDepartamentoPacienteMedic.Text = pacientes.DepartamentosPaciente.ToString();
            cmbMunicipioPacienteMedic.Text = pacientes.MunicipioPaciente.ToString();
            txtCorreoPacienteMedic.Text = pacientes.CorreoPaciente;
            txtDUIPacienteMedic.Text = pacientes.DUIPaciente;
            cmbSexoPacienteMedic.Text = pacientes.SexoPaciente; // Descripción del Sexo
            cmbEstadoCivilPacienteMedic.Text = pacientes.EstadoCivilPaciente; // Descripción del Estado Civil
            cmbEstadoPacienteMedic.Text = pacientes.EstadoPaciente;
        }

        private void cmbEstadoCivilPacienteMedic_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEstadoCivilPacienteMedic.SelectedItem != null)
            {
                // Verificar si la opción seleccionada es "Casado"
                string seleccion = cmbEstadoCivilPacienteMedic.SelectedItem.ToString();

                if (seleccion.Contains("Casado"))
                {
                    txtApellidoCasadaPacienteMedic.IsEnabled = true; // Habilitar el TextBox
                }
                else if (seleccion.Contains("Soltero"))
                {
                    txtApellidoCasadaPacienteMedic.IsEnabled = false; // Deshabilitar el TextBox
                    txtApellidoCasadaPacienteMedic.Text = string.Empty; // Limpiar el contenido si se desactiva
                }
                else if (seleccion.Contains("Divorciado"))
                {
                    txtApellidoCasadaPacienteMedic.IsEnabled = false; // Deshabilitar el TextBox
                    txtApellidoCasadaPacienteMedic.Text = string.Empty; // Limpiar el contenido si se desactiva
                }
                if (seleccion.Contains("Viudo"))
                {
                    txtApellidoCasadaPacienteMedic.IsEnabled = true; // Habilitar el TextBox
                }
            }
        }

        private void btnModificarPacienteMedic_Click(object sender, RoutedEventArgs e)
        {
            ModificarPaciente();

        }

        private void ModificarPaciente()
        {
            if (!ValidarFormulario())
            {
                return;
            }

            
            if (MessageBox.Show("¿Está seguro de que quiere actualizar los datos?", "Confirmación de Actualización || Hospi Plus", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    try
                    {
                        PacientesModel pacienteSeleccionado = (PacientesModel)gridGestorPacienteMedico.SelectedItem;
                        if (pacienteSeleccionado == null)
                        {
                            MessageBox.Show("Seleccione un paciente para modificar", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        ConexionDB.AbrirConexion(conexion);
                        using (SqlCommand command = new SqlCommand("EditarPaciente", conexion))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            
                            command.Parameters.AddWithValue("@PacienteID", pacienteSeleccionado.PacienteID);
                            command.Parameters.AddWithValue("@NombrePaciente", txtNombrePacienteMedic.Text);
                            command.Parameters.AddWithValue("@ApellidoPaciente", txtApellidoPacienteMedic.Text);
                            command.Parameters.AddWithValue("@ApellidoDeCasada", txtApellidoCasadaPacienteMedic.Text);
                            command.Parameters.AddWithValue("@FechaNacimientoPaciente", DateTime.Parse(dtFechaNPacienteMedic.Text));
                            command.Parameters.AddWithValue("@SexoID", Convert.ToInt32(((ComboBoxItem)cmbSexoPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoCivilID", Convert.ToInt32(((ComboBoxItem)cmbEstadoCivilPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@DUIPaciente", txtDUIPacienteMedic.Text);
                            command.Parameters.AddWithValue("@TelefonoPaciente", txTelefonoPacienteMedic.Text);
                            command.Parameters.AddWithValue("@CorreoPaciente", txtCorreoPacienteMedic.Text);
                            command.Parameters.AddWithValue("@DepartamentosID", Convert.ToInt32(((ComboBoxItem)cmbDepartamentoPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@MunicipioID", Convert.ToInt32(((ComboBoxItem)cmbMunicipioPacienteMedic.SelectedItem).Tag));
                            command.Parameters.AddWithValue("@EstadoID", Convert.ToInt32(((ComboBoxItem)cmbEstadoPacienteMedic.SelectedItem).Tag));

                            
                            command.ExecuteNonQuery();

                            MessageBox.Show("Paciente actualizado correctamente", "Éxito || HospiPlus", MessageBoxButton.OK, MessageBoxImage.Information);
                            CargarPacientes(); 
                            LimpiarCampos(); 
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al actualizar el paciente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        ConexionDB.CerrarConexion(conexion);
                    }
                }
            }
        }

    }

}