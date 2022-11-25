﻿using CoderFinalProject_Emilio_De_Leon.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;

namespace CoderFinalProject_Emilio_De_Leon.Controllers


{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("getallusers")]
        public dynamic GetUsers()
        {

            String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using(SqlCommand command = new SqlCommand("SELECT * FROM Usuario", connection))
                    {
                        connection.Open();
                        List<User> UserList = new List<User>();
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    User user = new User();
                                    user.Id = int.Parse(reader["id"].ToString());
                                    user.Nombre = reader["Nombre"].ToString();
                                    user.Apellido = reader["Apellido"].ToString();
                                    user.NombreUsuario = reader["NombreUsuario"].ToString();
                                    user.Contrasena = reader["Contraseña"].ToString();
                                    user.Mail = reader["Mail"].ToString();

                                    UserList.Add(user);
                                }
                                connection.Close();
                                var UserListJson = JsonSerializer.Serialize(UserList);
                                return UserListJson;
                            }
                            else
                            {
                                return "No data";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

        }
    }
}
