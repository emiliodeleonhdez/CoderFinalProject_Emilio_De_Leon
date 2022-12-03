using CoderFinalProject_Emilio_De_Leon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CoderFinalProject_Emilio_De_Leon.Repositories
{
    public class ProductRepository
    {
        private SqlConnection? connection;
        String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";

        public ProductRepository()
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

        public List<Producto>? ListProducts()
        {
            List<Producto> products = new List<Producto>();
            if(connection == null)
            {
                throw new Exception("Connection with DB not established...");
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Producto", connection))
                        {
                            connection.Open();
                            List<Producto> ProductList = new List<Producto>();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        Producto product = new Producto();
                                        product.Id = int.Parse(reader["Id"].ToString());
                                        product.Descripciones = reader["Descripciones"].ToString();
                                        product.Costo = decimal.Parse(reader["Costo"].ToString());
                                        product.PrecioVenta = decimal.Parse(reader["PrecioVenta"].ToString());
                                        product.Stock = int.Parse(reader["Stock"].ToString());
                                        product.IdUsuario = int.Parse(reader["IdUsuario"].ToString());

                                        ProductList.Add(product);
                                    }
                                    connection.Close();
                                }
                                else
                                {
                                    return null;
                                }
                                return ProductList;
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

        public Producto CreateProduct(Producto producto)
        {
            if (connection == null)
            {
                throw new Exception("Connection with DB not established...");
            }
            try
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO Producto VALUES(@descripciones, @costo, @precioVenta, @stock, @idUsuario); SELECT @@Identity ", connection))
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones });
                    command.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = producto.Costo });
                    command.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = producto.PrecioVenta });
                    command.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock });
                    command.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
                    producto.Id = long.Parse(command.ExecuteScalar().ToString());
                    return producto;
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

        public bool DeleteProduct(int id)
        {
            if(connection == null)
            {
                throw new Exception("Connection with DB not established...");
            }
            try
            {
                int rowsReturned = 0;
                using (SqlCommand command = new SqlCommand("DELETE FROM Producto WHERE id = @id", connection))
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    rowsReturned = command.ExecuteNonQuery();
                }
                connection.Close();
                return rowsReturned > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
