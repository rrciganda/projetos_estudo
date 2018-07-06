using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Configuration;

namespace Data
{
    public class DataAcess
    {
        private string strConexao;
        private ArrayList alParameters;
        public DataAcess()
        {
            alParameters = new ArrayList();
        }

        public SqlConnection criaConexao()
        {
            try
            {
                String connetionString = String.Empty;
                connetionString = "Data Source=" + ConfigurationManager.AppSettings["SQL_DataSource"] + ";";
                connetionString += "Initial Catalog=" + ConfigurationManager.AppSettings["SQL_DataBase"] + ";";
                connetionString += "User ID=" + ConfigurationManager.AppSettings["SQL_User"] + ";";
                connetionString += "Password=" + ConfigurationManager.AppSettings["SQL_Pass"];
                strConexao = connetionString;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            //dt = Convert.ToDateTime("18-12-2010");

            //if (DateTime.Now >= Convert.ToDateTime("18-12-2010"))
            //{ strConexao = ""; };

            SqlConnection conn = new SqlConnection(strConexao);
            return conn;
        }

        public DataTable GetDataTable(string comando)
        {
            SqlConnection conn = criaConexao();

            // Cria Comando
            SqlCommand cmd = new SqlCommand(comando, conn);
            cmd.CommandType = CommandType.Text;

            foreach (SqlParameter oP in alParameters)
            {

                cmd.Parameters.Add(oP);
            }

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public SqlDataReader GetDataReader(string comando)
        {
            SqlConnection conn = criaConexao();

            // Cria Comando
            SqlCommand cmd = new SqlCommand(comando, conn);
            cmd.CommandType = CommandType.Text;

            foreach (SqlParameter oP in alParameters)
            {
                cmd.Parameters.AddWithValue(oP.ParameterName, oP.Value);
            }


            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //conn.Close();
            }
        }

        public void Execute(string comando)
        {
            SqlConnection conn = criaConexao();

            // Cria Comando
            SqlCommand cmd = new SqlCommand(comando, conn);
            cmd.CommandType = CommandType.Text;

            foreach (SqlParameter oP in alParameters)
            {
                cmd.Parameters.AddWithValue(oP.ParameterName, oP.Value);
            }

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void AddInParameter(string Name, object Value)
        {
            SqlParameter oP = new SqlParameter();

            try
            {
                oP.ParameterName = Name;
                oP.Value = Value;
                alParameters.Add(oP);
            }
            catch (Exception oErro)
            {
                throw (oErro);
            }
        }

        public void AddInParameter(string Name, object Value, SqlDbType tipo)
        {
            SqlParameter oP = new SqlParameter();

            try
            {
                oP.SqlDbType = tipo;
                oP.ParameterName = Name;
                oP.Value = Value;
                alParameters.Add(oP);
            }
            catch (Exception oErro)
            {
                throw (oErro);
            }
        }

    }
}
