using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Curso.Context
{
    public class conexionBaseDatos : Controller
    {
        protected SqlConnection _connection;

        string stringConection;
        string error = "";

        public string Open(string cadenaConexion)
        {
            try
            {
                _connection = new SqlConnection(cadenaConexion);
                _connection.Open();
                //return "Conexión exitosa";
            }
            catch (Exception e)
            {
                error = e.Message;
                //return $"Error: {error}";
            }
                return error;
        }

        public DataTable recuperarTabla(string sql)
        { 
            DataTable dt = new DataTable();

            if(_connection.State.ToString() == "Open")
            {
                SqlCommand cmd = new SqlCommand(sql, _connection);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Close();
            }
            return dt;
        }

        public int Insert(string sql)
        {
            int ultimoId = 0;
            try
            {
                if(_connection.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(sql, _connection);
                    ultimoId = cmd.ExecuteNonQuery();

                    ultimoId = Convert.ToInt32(ultimoId);
                }
            }
            catch (Exception e)
            {

                ultimoId = 0;
            }
            return ultimoId;
        }
    }
}
