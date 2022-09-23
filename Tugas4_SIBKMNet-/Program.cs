using System;
using System.Data.SqlClient;
using Tugas4_SIBKMNet_.Models;

namespace Tugas4_SIBKMNet_
{
    class Program
    {

        SqlConnection sqlConnection;
        /*
         * Data Source -> Server
         * Initial Catalog -> Database
         * User ID -> username
         * Password -> password
         * Connect Timeout
         */

        string connectionString = "Data Source=localhost;Initial Catalog=SIBKMNET;User ID=sibkmnet;Password=123456;Connect Timeout=30;";

        private static object ex;

        public object Country { get; private set; }
        static void Main(string[] args)
        {
            Program program = new Program();
            //program.GetAll();
            //program.GetById(1);

            country Country = new country()
            {
                id = 2,
                Name = "Waktu Indonesia Barat(Contoh Update)"
            };

            program.Update(Country);
            //program.Delete(Country);
            //program.Insert(Country);
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

                Console.WriteLine("suksess connect");
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + "-" + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("data tidak ada dalam baris");
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
            string query = "SELECT * FROM Country where id=@id";
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);

            try
            {
                sqlConnection.Open();

                Console.WriteLine("suksess connect");
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + "-" + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("data tidak ada dalam baris");
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

        void Insert(country Country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@name";
                sqlParameter.Value = Country.Name;

                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Country" + "(Name) values (@name)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        void Delete(country Country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = Country.id;

                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText = "DELETE FROM Country WHERE id = @id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }

        }

        void Update(country Country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameterId = new SqlParameter();
                sqlParameterId.ParameterName = "@id";
                sqlParameterId.Value = Country.id;

                SqlParameter sqlParameterName = new SqlParameter();
                sqlParameterName.ParameterName = "@UpdateName";
                sqlParameterName.Value = Country.Name;

                sqlCommand.Parameters.Add(sqlParameterId);
                sqlCommand.Parameters.Add(sqlParameterName);

                try
                {
                    sqlCommand.CommandText = "UPDATE Country SET Name=@UpdateName WHERE id=@id";
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
