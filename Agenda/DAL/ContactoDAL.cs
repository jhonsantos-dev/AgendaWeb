using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Agenda.DAL
{
    public class ContactoDAL
    {
        private readonly Conexion _conexion;

        public ContactoDAL()
        { 
            _conexion = new Conexion();
        }
        // Agregar - Insert
        public void Agregar(Contacto c)
        {
            using (SqlConnection conn = _conexion.GetSqlConnection())
            {
                string query = "Insert into Contactos(Nombre,Apellido,Telefono,Email,Direccion) VALUES (@Nombre, @Apellido, @Telefono, @Email, @Direccion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre",c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Buscar - Select - Read

        public List<Contacto> Listar(string filtro = "")
        { 
            List<Contacto> lista = new List<Contacto>();
            using (SqlConnection conn = _conexion.GetSqlConnection())
            {
                string query = "Select * from Contactos Where Nombre LIKE @Filtro OR Telefono LIKE @Filtro";
                SqlCommand cmd = new SqlCommand(@query, conn);
                cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Contacto
                    {
                        Id_Contacto = (int)dr["Id_Contacto"],
                        Nombre = dr["Nombre"].ToString(),
                        Apellido = dr["Apellido"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Email = dr["Email"].ToString(),
                        Direccion = dr["Direccion"].ToString()

                    });
                }
            }
            return lista;
        }

        // Actualizar - Update 
        public void Actualizar(Contacto c)
        {
            using (SqlConnection conn = _conexion.GetSqlConnection())
            {
                string query = "UPDATE Contactos SET Nombre=@Nombre, Apellido=@Apellido, Telefono=@Telefono, Email=@Email, Direccion=@Direccion WHERE Id_Contacto=@Id_Contacto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@Id_Contacto", c.Id_Contacto);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Eliminar - Delete
        public void Eliminar(int Id_Contacto)
        {
            using (SqlConnection conn = _conexion.GetSqlConnection())
            {
                string query = "DELETE FROM Contactos WHERE Id_Contacto=@Id_Contacto";
                SqlCommand cmd = new SqlCommand (query, conn);
                cmd.Parameters.AddWithValue("@Id_Contacto", Id_Contacto);
                conn.Open();
                cmd.ExecuteNonQuery(); 
            }
        }

        //Obtener contacto por ID
        public Contacto ObtenerPorId(int id) 
        {
            Contacto c = null;

            using (SqlConnection conn = _conexion.GetSqlConnection())
            {
                string query = "SELECT * FROM Contactos WHERE Id_Contacto = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    c = new Contacto
                    {
                        Id_Contacto = (int)dr["Id_Contacto"],
                        Nombre = dr["Nombre"].ToString(),
                        Apellido = dr["Apellido"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Email = dr["Email"].ToString(),
                        Direccion = dr["Direccion"].ToString()
                    };
                }
            }

            return c;
        }


    }
}