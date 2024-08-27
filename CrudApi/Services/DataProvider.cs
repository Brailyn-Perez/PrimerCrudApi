
using CrudApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CrudApi.Services
{
    public class DataProvider
    {
        private ConnectionDB connectionDB = new ConnectionDB();


        public List<PersonaModel> GetData()
        {
            var listaPersonas = new List<PersonaModel>();

            using (var conn = new SqlConnection(connectionDB.ConnectionString()))
            {
                conn.Open();
                string query = "SELECT * FROM PERSONA";
                var command = new SqlCommand(query, conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaPersonas.Add(new PersonaModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2)
                        });
                    }
                }

            }
            return (listaPersonas);
        }
        public (List<PersonaModel>,bool) GetByID(int IDpersona)
        {
            var listaPersonas = new List<PersonaModel>();

            using (var conn = new SqlConnection(connectionDB.ConnectionString()))
            {
                conn.Open();
                string query = "SELECT * FROM PERSONA WHERE ID = @ID";
                var command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@ID", IDpersona);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            listaPersonas.Add(new PersonaModel
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2)
                            });
                            
                        }
                        return (listaPersonas,true);
                    }


                    return (listaPersonas, false);
                    

                }

            }
            
        }
        public List<PersonaModel> PutPersonas(int IDPersona ,PersonaModel persona)
        {
            var personas = new List<PersonaModel>(); 
            string query = "UPDATE PERSONA SET NOMBRE = @NOMBRE , APELLIDO = @APELLIDO WHERE ID = @ID";
            using (var conn = new SqlConnection(connectionDB.ConnectionString()))
            {
                conn.Open();
                var command =new SqlCommand(query,conn);
                command.Parameters.AddWithValue("@ID", IDPersona);
                command.Parameters.AddWithValue("@NOMBRE", persona.Nombre);
                command.Parameters.AddWithValue("@APELLIDO", persona.Apellido);
                command.ExecuteNonQuery();
                
            }
            //ahora retornamos la persona editada
            return personas;
        }

        public string DeletePersonas(int IDPersona)
        {
            string query = "DELETE FROM PERSONA WHERE ID=@ID";
            using (var conn = new SqlConnection(connectionDB.ConnectionString()))
            {
                conn.Open();
                var command = new SqlCommand(query,conn);
                command.Parameters.AddWithValue("@ID",IDPersona);
                command.ExecuteNonQuery();
            }

            return ("Persona Eliminada con Exito");
        }
    }
}
