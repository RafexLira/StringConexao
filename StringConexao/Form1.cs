using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StringConexao
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'testeDataSet1.Cliente'. Você pode movê-la ou removê-la conforme necessário.
            //this.clienteTableAdapter.Fill(this.testeDataSet1.Cliente);
        }

        string StrConn = @"Data Source= corei7\sqlexpress; Database=Teste;Integrated Security=SSPI;";
        string ConsulTodos = "SELECT *FROM Cliente";
        string Selecionar = "SELECT *FROM Cliente WHERE Nome = @Nome"; // Isto é só um exemplo, o correto seria pelo ID
        string Incluir = "INSERT INTO Cliente (Nome, Telefone)VALUES(@Nome,@Telefone)";
        string Atualizar = "UPDATE Cliente SET Nome = @Nome, Telefone = @Telefone WHERE Nome = @Nome";


        private void BtnBuscar_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(StrConn);
            SqlDataAdapter da = new SqlDataAdapter(ConsulTodos, conn);
            DataSet ds = new DataSet();
            try
            {
         
            conn.Open();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
              
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               
            }

        }
        private void BtnProcurar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand cmd = new SqlCommand(Selecionar, conn);           


            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Nome", TxtNome.Text);
                SqlDataReader dr = cmd.ExecuteReader();              
             
                while (dr.Read())
                {
                    TxtCliente.Text = dr["Nome"].ToString();                
                    TxtTelefoneCli.Text = dr["Telefone"].ToString();
                  


                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void BtnIncluir_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand cmd = new SqlCommand(Incluir, conn);
            try
            {               
                cmd.Parameters.AddWithValue("@Nome", TxtNome.Text);
                cmd.Parameters.AddWithValue("@Telefone", TxtTelefone.Text.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("incluído com sucesso!");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand cmd = new SqlCommand(Atualizar, conn);
            try
            {
                cmd.Parameters.AddWithValue("@Nome", TxtNome.Text);
                cmd.Parameters.AddWithValue("@Telefone", TxtTelefone.Text.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Atualizado com sucesso!");
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

       
    }
}
/*
 *  SqlConnection conn = new SqlConnection(StrConn);
            SqlCommand cmd = new SqlCommand(ConsulTodos, conn);
            DataSet ds = new DataSet();


            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    listBox1.Items.Add(dr["Nome"]).ToString();
                    listBox1.Items.Add(dr["Telefone"]).ToString();
                 
                   
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
 
     */
