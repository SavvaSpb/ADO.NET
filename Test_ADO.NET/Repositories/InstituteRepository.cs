using System.Data;
using System.Data.SqlClient;
using Test_ADO.NET.Models;

namespace Test_ADO.NET.Repositories
{
    public class InstituteRepository : Repository<Institute>
    {
        public override int Add(Institute institute)
        {
            int lastId = 0;

            string sqlExpression = "INSERT INTO institute (institute_type_name) VALUES (@institute_type_name)";

            using (SqlConnection connection = new SqlConnection(ConStr))
            {

                string instituteTypeName = "institute_type_name";

                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("STR PRC", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@institute_type_name", SqlDbType.VarChar, 255, "institute_type_name"));

                        lastId = Convert.ToInt32(command.ExecuteScalar());

                        Console.WriteLine(connection.State);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine(connection.State);
                }

                return lastId;
            }
        }

        public override void Update(int id, Institute institute)
        {
            string sqlExpression = "UPDATE institute SET  institute_type_name  WHERE institute_id = @institute_id";

            using (SqlConnection connection = new SqlConnection(ConStr))
            {

                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine(connection.State);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine(connection.State);
                }
            }
        }


        public override List<Institute> Get()
        {
            string sqlExpression = "SELECT * FROM students";

            List<Institute> institutes = new List<Institute>();

            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {


                            while (reader.Read())
                            {
                                institutes.Add(new Institute
                                {
                                    InstituteTypeName = reader["institute_type_name"] as string

                                });
                            }
                        }
                    }

                }
                finally
                {
                    connection.Close();
                }

                return institutes;
            }
        }

        public override Institute GetById(int id)
        {
            string sqlExpression = "SELECT * FROM students" + id.ToString(); ;

            Institute institute = new Institute();

            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            institute = new Institute
                            {
                                InstituteId = id,
                                InstituteTypeName = reader["institute_type_name"] as string

                            };
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }

                return institute;
            }

        }
    }
}
