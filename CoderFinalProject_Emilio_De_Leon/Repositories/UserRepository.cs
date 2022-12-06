using CoderFinalProject_Emilio_De_Leon.Models;
using System.Data.SqlClient;

namespace CoderFinalProject_Emilio_De_Leon.Repositories
{
    public class UserRepository
    {
        private SqlConnection? connection;
        String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
        public UserRepository()
        {
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception)
            {
                throw new Exception("Connection with DB not established...");
            }
        }

        public List<User>? ListUsers()
        {
            List<User>? users = new List<User>();
            if (connection == null) throw new Exception("Connection with DB not established...");

            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Usuario", connection))
                        {
                            connection.Open();
                            List<User> UserList = new List<User>();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        User user = new User();
                                        user.Id = int.Parse(reader["Id"].ToString());
                                        user.Nombre = reader["Nombre"].ToString();
                                        user.Apellido = reader["Apellido"].ToString();
                                        user.NombreUsuario = reader["NombreUsuario"].ToString();
                                        user.Contrasena = reader["Contraseña"].ToString();
                                        user.Mail = reader["Mail"].ToString();
                                        UserList.Add(user);
                                    }
                                    connection.Close();
                                }
                                else
                                {
                                    return null;
                                }
                                return UserList;
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
