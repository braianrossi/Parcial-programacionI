using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Parcial2.Datos
{
    public class AccesoDatos
    {
        SqlConnection conexion;
        SqlCommand comando;
        private string cadenaConexion;
        public AccesoDatos()
        {
            cadenaConexion = @"Data Source=172.16.10.196;Initial Catalog=Elecciones2023;User ID=alumno1w1;Password=alumno1w1";
            conexion = new SqlConnection(cadenaConexion);
            comando = new SqlCommand();
        }
        public void conectar()
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
        }
        public void desconectar()
        {
            conexion.Close();
        }
        public DataTable consultarBD(string consultaSQL)
        {
            DataTable tabla = new DataTable();
            conectar();
            comando.CommandText = consultaSQL;
            tabla.Load(comando.ExecuteReader());
            desconectar();
            return tabla;
        }
        public int actualizarBD(string consultaSQL, List<Parametro> lParametro)
        {
            int filaAfectada;
            conectar();
            comando.CommandText = consultaSQL;
            comando.Parameters.Clear();
            foreach (Parametro p in lParametro)
            {
                comando.Parameters.AddWithValue(p.Nombre, p.Valor);
            }
            filaAfectada = comando.ExecuteNonQuery();
            desconectar();
            return filaAfectada;
        }
    }
}