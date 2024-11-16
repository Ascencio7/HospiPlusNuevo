using HospiPlus.DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using HospiPlus.ModeloPaciente;
using HospiPlus.ModeloMedico;
using System.Windows.Input;


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
            CargarConsultas();
            CargarMedicos();
            CargarPacientes();
            CargarCitas();
        }

        private void CargarMedicos()
        {
            try
            {
                List<MedicosModel> medicos = new List<MedicosModel>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT MedicoID, NombreMedico FROM Medicos";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicosModel medico = new MedicosModel()
                                {
                                    MedicoID = int.Parse(reader["MedicoID"].ToString()),
                                    NombreMedico = reader["NombreMedico"].ToString()
                                };

                                medicos.Add(medico);
                            }
                        }
                    }
                }


                cmbMedico.ItemsSource = medicos;
                cmbMedico.DisplayMemberPath = "NombreMedico";
                cmbMedico.SelectedValuePath = "MedicoID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar médicos: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarCitas()
        {
            try
            {
                List<KeyValuePair<int, string>> citas = new List<KeyValuePair<int, string>>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT CitaID, CONCAT('Cita #', CitaID) AS CitaDescripcion FROM Citas";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                citas.Add(new KeyValuePair<int, string>(Convert.ToInt32(reader["CitaID"]), reader["CitaDescripcion"].ToString()));
                            }
                        }
                    }
                }

                // Asignar la fuente de datos del ComboBox
                cmbCitaID.ItemsSource = citas;
                cmbCitaID.DisplayMemberPath = "Value";  // Mostrar la descripción de la cita
                cmbCitaID.SelectedValuePath = "Key";   // Usar el ID de la cita como valor
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las citas: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void CargarConsultas()
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
                gridConsultas.ItemsSource = consultas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las consultas: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private bool ValidarCampos()
        {
            if (

                string.IsNullOrWhiteSpace(cmbPaciente.Text) ||
                string.IsNullOrWhiteSpace(cmbMedico.SelectedValue.ToString()) ||
                string.IsNullOrWhiteSpace(txtAlergias.Text) ||
                string.IsNullOrWhiteSpace(txtAltura.Text) ||
                string.IsNullOrWhiteSpace(txtDiagnostico.Text) ||
                string.IsNullOrWhiteSpace(txtObservaciones.Text) ||
                string.IsNullOrWhiteSpace(txtSintomas.Text) ||
                string.IsNullOrWhiteSpace(txtPeso.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "HOSPI PLUS | Campos vacios", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtPeso.Clear();

            cmbPaciente.Text = "";
            cmbMedico.SelectedIndex = -1;
            txtAlergias.Clear();
            txtDiagnostico.Clear();
            txtObservaciones.Clear();
            txtAltura.Clear();
            txtSintomas.Clear();

            dpFechaConsulta.SelectedDate = null;
        }

        private void gridGConsultM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridConsultas.SelectedItem is ModeloConsultaPaciente consulta)
            {
                cmbPaciente.Text = consulta.PacienteID;
                cmbMedico.Text = consulta.MedicoID;
                txtAltura.Text = consulta.Altura.ToString();
                txtPeso.Text = consulta.Peso.ToString();
                txtAlergias.Text = consulta.Alergia;
                txtSintomas.Text = consulta.Sintomas;
                txtDiagnostico.Text = consulta.Diagnostico;
                dpFechaConsulta.SelectedDate = consulta.FechaConsulta;
            }
        }

        private void btnBuscarConsulSecre_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    // Asegúrate de que los valores seleccionados de los ComboBox son válidos
                    if (cmbPaciente.SelectedValue == null || cmbMedico.SelectedValue == null)
                    {
                        MessageBox.Show("Seleccione un paciente y un médico.", "HOSPI PLUS | Valor vacio", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    int pacienteID = (int)cmbPaciente.SelectedValue;
                    int medicoID = (int)cmbMedico.SelectedValue;

                    //
                    int citaID = (int)cmbCitaID.SelectedValue; // Si no tienes un campo específico, puedes dejarlo en cero o usar un campo del formulario

                    using (var conexion = ConexionDB.ObtenerCnx())
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (var command = conexion.CreateCommand())
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "Ingresar_consulta";

                            command.Parameters.AddWithValue("@CitaID", citaID);
                            command.Parameters.AddWithValue("@PacienteID", pacienteID);
                            command.Parameters.AddWithValue("@MedicoID", medicoID);
                            command.Parameters.AddWithValue("@Altura", decimal.Parse(txtAltura.Text));
                            command.Parameters.AddWithValue("@Peso", decimal.Parse(txtPeso.Text));
                            command.Parameters.AddWithValue("@Alergia", txtAlergias.Text);
                            command.Parameters.AddWithValue("@Sintomas", txtSintomas.Text);
                            command.Parameters.AddWithValue("@Diagnostico", txtDiagnostico.Text);
                            command.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text);
                            command.Parameters.AddWithValue("@FechaConsulta", DateTime.Parse(dpFechaConsulta.Text));

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Consulta agregada exitosamente.", "HOSPI PLUS | Consulta agregada", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarConsultas();  // Recarga la lista de consultas
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la consulta: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCancelarBuscar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            CargarConsultas();
        }
        private void CargarPacientes()
        {
            try
            {
                List<KeyValuePair<int, string>> pacientes = new List<KeyValuePair<int, string>>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT PacienteID, NombrePaciente FROM Pacientes";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                pacientes.Add(new KeyValuePair<int, string>(Convert.ToInt32(reader["PacienteID"]), reader["NombrePaciente"].ToString()));
                            }
                        }
                    }
                }


                cmbPaciente.ItemsSource = pacientes;
                cmbPaciente.DisplayMemberPath = "Value";
                cmbPaciente.SelectedValuePath = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pacientes: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void gridConsultas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridConsultas.SelectedItem is ModeloConsultaPaciente consulta)
            {
                cmbPaciente.Text = consulta.PacienteID;

                txtAltura.Text = consulta.Altura.ToString();
                txtPeso.Text = consulta.Peso.ToString();
                txtAlergias.Text = consulta.Alergia;
                txtSintomas.Text = consulta.Sintomas;
                txtDiagnostico.Text = consulta.Diagnostico;
                dpFechaConsulta.SelectedDate = consulta.FechaConsulta;
            }


        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    // Asegúrate de que los valores seleccionados de los ComboBox son válidos
                    if (cmbPaciente.SelectedValue == null || cmbMedico.SelectedValue == null || cmbCitaID == null)
                    {
                        MessageBox.Show("Seleccione un paciente y un médico.", "HOSPI PLUS | Campos vacios");
                        return;
                    }

                    int pacienteID = (int)cmbPaciente.SelectedValue;
                    int medicoID = (int)cmbMedico.SelectedValue;

                    //
                    int citaID = (int)cmbCitaID.SelectedValue;

                    using (var conexion = ConexionDB.ObtenerCnx())
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (var command = conexion.CreateCommand())
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "InsertarConsulta";

                            command.Parameters.AddWithValue("@CitaID", citaID);
                            command.Parameters.AddWithValue("@PacienteID", pacienteID);
                            command.Parameters.AddWithValue("@MedicoID", medicoID);
                            command.Parameters.AddWithValue("@Altura", decimal.Parse(txtAltura.Text));
                            command.Parameters.AddWithValue("@Peso", decimal.Parse(txtPeso.Text));
                            command.Parameters.AddWithValue("@Alergia", txtAlergias.Text);
                            command.Parameters.AddWithValue("@Sintomas", txtSintomas.Text);
                            command.Parameters.AddWithValue("@Diagnostico", txtDiagnostico.Text);
                            command.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text);
                            command.Parameters.AddWithValue("@FechaConsulta", dpFechaConsulta.SelectedDate);


                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Consulta agregada exitosamente.", "HOSPI PLUS | Consulta agregada", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarConsultas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la consulta: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    using (var conexion = ConexionDB.ObtenerCnx())
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (var command = conexion.CreateCommand())
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "Editarconsulta";


                            command.Parameters.AddWithValue("@PacienteID", int.Parse(cmbPaciente.SelectedValuePath));
                            command.Parameters.AddWithValue("@MedicoID", int.Parse(cmbMedico.SelectedValue.ToString()));
                            command.Parameters.AddWithValue("@Altura", decimal.Parse(txtAltura.Text));
                            command.Parameters.AddWithValue("@Peso", decimal.Parse(txtPeso.Text));
                            command.Parameters.AddWithValue("@Alergia", txtAlergias.Text);
                            command.Parameters.AddWithValue("@Sintomas", txtSintomas.Text);
                            command.Parameters.AddWithValue("@Diagnostico", txtDiagnostico.Text);

                            command.Parameters.AddWithValue("@FechaConsulta", DateTime.Parse(dpFechaConsulta.Text));
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Consulta modificada exitosamente.", "HOSPI PLUS | Consulta modificada", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarConsultas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar la consulta: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea limpiar los campos?", "HOSPI PLUS | Limpiar campos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarCampos();
            }
        }

    }
}