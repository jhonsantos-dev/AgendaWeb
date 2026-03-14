using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Agenda.DAL
{
    public class Conexion
    {
        private readonly string _connectionString;

        public Conexion()
        { 
            _connectionString = ConfigurationManager.ConnectionStrings["AgendaContactos"].ConnectionString;

        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}