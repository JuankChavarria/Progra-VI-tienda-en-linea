using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Progra_VI_Tienda.Models;
using System.Data;
namespace Progra_VI_Tienda.Data
{
    public class ClientesADORepository
    {
        private readonly string _connectionString = "Server=.;Database=Test08052025;User Id=sa;Password=Alejandra25;TrustServerCertificate=True;";
        public List<Cliente> GetAll()
        {
            var lista = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListarCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id_cliente = (int)reader["IdCliente"],
                        Nombre = reader["Nombre"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Contraseña = reader["Contrasena"].ToString(),
                        Direccion = reader["Direccion"].ToString()
                    });
                }
            }
            return lista;
        }
        public void Insert(Cliente clientes)
        {
            int newId = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Este comando ejecuta el SP y luego obtiene el ID insertado
                string sql = "EXEC GuardarCliente @Nombre, @Correo, @Contrasena, @Direccion; SELECT MAX(ID) FROM Clientes; ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", clientes.Nombre);
                    cmd.Parameters.AddWithValue("@Correo", clientes.Correo);
                    cmd.Parameters.AddWithValue("@Contrasena", clientes.Contraseña);
                    cmd.Parameters.AddWithValue("@Direccion", clientes.Direccion);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    newId = Convert.ToInt32(result);
                }
            }
            // Bitácora
            string detalle = $"Insertó: Nombre={clientes.Nombre}, Correo={clientes.Correo},  Contrasena={clientes.Contraseña}, Direccion={clientes.Direccion}";
            RegistrarBitacora("INSERT", "Clientes", newId, detalle);
        }

        public Cliente GetById(int id)
        {
            Cliente est = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListarClientePorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    est = new Cliente
                    {
                        Id_cliente = (int)reader["IdCliente"],
                        Nombre = reader["Nombre"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Contraseña = reader["Contrasena"].ToString(),
                        Direccion = reader["Direccion"].ToString()
                    };
                }
            }
            return est;
        }
        public void Update(Cliente clientes)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("ActualizarCliente", conn);
                cmd.Parameters.AddWithValue("@Nombre", clientes.Nombre);
                cmd.Parameters.AddWithValue("@Correo", clientes.Correo);
                cmd.Parameters.AddWithValue("@Contrasena", clientes.Contraseña);
                cmd.Parameters.AddWithValue("@Direccion", clientes.Direccion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            // Bitácora
            string detalle = $"Actualizó a:  Nombre={clientes.Nombre}, Correo={clientes.Correo},  Contrasena={clientes.Contraseña}, Direccion={clientes.Direccion}";
            RegistrarBitacora("UPDATE", "Clientes", clientes.Id_cliente, detalle);
        }

        public void Delete(int Id_cliente)
        {
            Cliente clientes = null;
            clientes = GetById(Id_cliente);
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("EliminarCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_cliente", Id_cliente);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            // Bitácora
            string detalle = $"Eliminó: Nombre=Nombre={clientes.Nombre}, Correo={clientes.Correo},  Contrasena={clientes.Contraseña}, Direccion={clientes.Direccion}";
            RegistrarBitacora("DELETE", "Clientes", clientes.Id_cliente, detalle);
        }
        private void RegistrarBitacora(string operacion, string tabla, int idRegistro, string detalle)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"INSERT INTO BitacoraMovimientos
 (TipoOperacion, NombreTabla, IdRegistroAfectado, Detalle)
 VALUES (@operacion, @tabla, @id, @detalle)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@tabla", tabla);
                    cmd.Parameters.AddWithValue("@id", idRegistro);
                    cmd.Parameters.AddWithValue("@detalle", detalle);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

