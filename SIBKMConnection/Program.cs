using SIBKMConnection.Models;
using System;
using System.Data.SqlClient;

namespace SIBKMConnection
{
    class Program
    {
        SqlConnection sqlConnection;

        /*
         * Data Source - Server 
         * Initial Catalog - Database
         * User ID - Username
         * Password - Password
         * Connect Timeout
         */
        string connectionString = "Data Source=localhost;Initial Catalog=SIBKMNETConnection;User ID=sibkmnet;Password=1234567890;Connect Timeout=30;";

        static void Main(string[] args)
        {
            Program program = new Program();

            //program.GetById(3);

            //untuk isi country
            Country country = new Country()
            {
                Name = "Waktu Indonesia Bagian Tenggara",
                Id = 6
            };
            //program.Insert(country);
            //program.Delete(country);
            program.Update(country);
            program.GetAll();
        }

        void GetAll()
        {
            string query = "SELECT * FROM Country";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

        }

        void GetById(int id)
        {
            string query = "SELECT * FROM Country WHERE Id = @id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        void Update(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@name";
                sqlParameter.Value = country.Name;

                SqlParameter sqlParameter2 = new SqlParameter();
                sqlParameter2.ParameterName = "@id";
                sqlParameter2.Value = country.Id;

                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.Parameters.Add(sqlParameter2);

                try
                {
                    //input yang bakal dijalanin seperti apa dalam command
                    sqlCommand.CommandText = "UPDATE Country SET Name = (@name) WHERE Id = @id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }

        }

        //Update
        void Insert(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@name";
                sqlParameter.Value = country.Name;

                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    //input yang bakal dijalanin seperti apa dalam command
                    sqlCommand.CommandText = "INSERT INTO Country(Name) VALUES (@name)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }
        //Delete - Statements in table
        void Delete(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = country.Id;

                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    //input yang bakal dijalanin seperti apa dalam command
                    sqlCommand.CommandText = "DELETE FROM Country WHERE (Id) = (@id)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }
    }
}
