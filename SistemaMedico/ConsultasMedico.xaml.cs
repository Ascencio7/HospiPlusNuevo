using HospiPlus.DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using HospiPlus.ModeloPaciente;
using HospiPlus.ModeloMedico;

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


                cmbMedicoIDMedico.ItemsSource = medicos;
                cmbMedicoIDMedico.DisplayMemberPath = "NombreMedico";
                cmbMedicoIDMedico.SelectedValuePath = "MedicoID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar médicos: " + ex.Message);
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
                                    ConsultaID = int.Parse(leertabla["ConsultaID"].ToString()),
                                    PacienteID = leertabla["NombreCompletoPaciente"].ToString(),
                                    MedicoID = leertabla["NombreCompletoMedico"].ToString(),
                                    Altura = decimal.Parse(leertabla["Altura"].ToString()),
                                    Peso = decimal.Parse(leertabla["Peso"].ToString()),
                                    Alergia = leertabla["Alergia"].ToString(),
                                    Sintomas = leertabla["Sintomas"].ToString(),
                                    Diagnostico = leertabla["Diagnostico"].ToString(),
                                    Observaciones = leertabla["Observaciones"].ToString(),
                                    FechaConsulta = DateTime.Parse(leertabla["FechaConsulta"].ToString())
                                };

                                consultas.Add(consulta);
                            }
                        }
                    }
                }
                gridGConsultM.ItemsSource = consultas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las consultas: " + ex.Message);
            }
        }

        

        private void btnBuscarConsulSecre_Copiar_Click_1(object sender, RoutedEventArgs e)
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
                            command.CommandText = "Editar_consulta";


                            command.Parameters.AddWithValue("@PacienteID", int.Parse(cmbNombrePaciente.Text));
                            command.Parameters.AddWithValue("@MedicoID", int.Parse(cmbMedicoIDMedico.SelectedValue.ToString()));
                            command.Parameters.AddWithValue("@Altura", decimal.Parse(txtNombrePaciente_Copiar.Text));
                            command.Parameters.AddWithValue("@Peso", decimal.Parse(txtNombrePaciente_Copiar1.Text));
                            command.Parameters.AddWithValue("@Alergia", txtNombrePaciente_Copiar2.Text);
                            command.Parameters.AddWithValue("@Sintomas", txtNombrePaciente_Copiar3.Text);
                            command.Parameters.AddWithValue("@Diagnostico", txtNombrePaciente_Copiar4.Text);

                            command.Parameters.AddWithValue("@FechaConsulta", DateTime.Parse(FechaConsulta.Text));
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Consulta modificada exitosamente.");
                    CargarConsultas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar la consulta: " + ex.Message);
                }
            }
        }

        private bool ValidarCampos()
        {
            if (

                string.IsNullOrWhiteSpace(cmbNombrePaciente.Text) ||
                string.IsNullOrWhiteSpace(cmbMedicoIDMedico.SelectedValue.ToString()) ||
                string.IsNullOrWhiteSpace(cmbNombrePaciente.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePaciente_Copiar.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePaciente_Copiar1.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePaciente_Copiar2.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePaciente_Copiar3.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePaciente_Copiar4.Text)) 
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtNombrePaciente_Copiar.Clear();

            
            cmbMedicoIDMedico.SelectedIndex = -1;
            txtNombrePaciente_Copiar.Clear();
            txtNombrePaciente_Copiar1.Clear();
            txtNombrePaciente_Copiar2.Clear();
            txtNombrePaciente_Copiar3.Clear();
            txtNombrePaciente_Copiar4.Clear();
            
            FechaConsulta.SelectedDate = null;
        }

        private void gridGConsultM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridGConsultM.SelectedItem is ModeloConsultaPaciente consulta) 
            {  cmbNombrePaciente.Text = consulta.PacienteID; 
                
                txtNombrePaciente_Copiar.Text = consulta.Altura.ToString(); 
                txtNombrePaciente_Copiar1.Text = consulta.Peso.ToString(); 
                txtNombrePaciente_Copiar2.Text = consulta.Alergia; 
                txtNombrePaciente_Copiar3.Text = consulta.Sintomas; 
                txtNombrePaciente_Copiar4.Text = consulta.Diagnostico;  
                FechaConsulta.SelectedDate = consulta.FechaConsulta; }
        }

        private void btnBuscarConsulSecre_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    // Asegúrate de que los valores seleccionados de los ComboBox son válidos
                    if (cmbNombrePaciente.SelectedValue == null || cmbMedicoIDMedico.SelectedValue == null)
                    {
                        MessageBox.Show("Seleccione un paciente y un médico.");
                        return;
                    }

                    int pacienteID = (int)cmbNombrePaciente.SelectedValue;
                    int medicoID = (int)cmbMedicoIDMedico.SelectedValue;

                    using (var conexion = ConexionDB.ObtenerCnx())
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (var command = conexion.CreateCommand())
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "Ingresar_consulta";

                            command.Parameters.AddWithValue("@PacienteID", pacienteID);
                            command.Parameters.AddWithValue("@MedicoID", medicoID);
                            command.Parameters.AddWithValue("@Altura", decimal.Parse(txtNombrePaciente_Copiar1.Text));
                            command.Parameters.AddWithValue("@Peso", decimal.Parse(txtNombrePaciente_Copiar2.Text));
                            command.Parameters.AddWithValue("@Alergia", txtNombrePaciente_Copiar.Text);
                            command.Parameters.AddWithValue("@Sintomas", txtNombrePaciente_Copiar3.Text);
                            command.Parameters.AddWithValue("@Diagnostico", txtNombrePaciente_Copiar4.Text);
                            
                            command.Parameters.AddWithValue("@FechaConsulta", DateTime.Parse(FechaConsulta.Text));

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Consulta agregada exitosamente.");
                    CargarConsultas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la consulta: " + ex.Message);
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
                List<string> nombresPacientes = new List<string>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT NombrePaciente FROM Pacientes";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nombresPacientes.Add(reader["NombrePaciente"].ToString());
                            }
                        }
                    }
                }


                cmbNombrePaciente.ItemsSource = nombresPacientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pacientes: " + ex.Message);
            }
        }


    }
}
