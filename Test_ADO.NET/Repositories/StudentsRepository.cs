using System.Data;
using System.Data.SqlClient;
using Test_ADO.NET.Models;

namespace Test_ADO.NET.Repositories
{
    public class StudentsRepository : Repository<Students>
    {
        public override int Add(Students students)
        {
            int lastId = 0;

            string sqlExpression = "INSERT INTO students ( first_name, last_name, birthday, address, phone, email) VALUES ( @first_name, @last_name, @birthday, @address, @phone, @email) SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {

                        command.Parameters.Add("@first_name", SqlDbType.VarChar, 255).Value = students.FirstName;
                        command.Parameters.Add("@last_name", SqlDbType.VarChar, 255).Value = students.LastName;
                        command.Parameters.Add("@birthday", SqlDbType.Date).Value = students.Birthday;
                        command.Parameters.Add("@address", SqlDbType.VarChar, 255).Value = students.Address;
                        command.Parameters.Add("@phone", SqlDbType.Char, 15).Value = students.Phone;
                        command.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = students.Email;

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

        public override void Update(int id, Students students)
        {
            string sqlExpression = "UPDATE students SET first_name = @first_name, last_name = @last_name, birthday = @birthday, address = @address, phone = @phone, email = @email  WHERE students_id = @students_id";

            using (SqlConnection connection = new SqlConnection(ConStr))
            {

                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {
                        command.Parameters.Add("@students_id", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@first_name", SqlDbType.VarChar, 255).Value = students.FirstName;
                        command.Parameters.Add("@last_name", SqlDbType.VarChar, 255).Value = students.LastName;
                        command.Parameters.Add("@birthday", SqlDbType.Date).Value = students.Birthday;
                        command.Parameters.Add("@address", SqlDbType.VarChar, 255).Value = students.Address;
                        command.Parameters.Add("@phone", SqlDbType.Char, 15).Value = students.Phone;
                        command.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = students.Email;

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

        public override List<Students> Get()
        {
            string sqlExpression = "SELECT * FROM students";

            List<Students> students = new List<Students>();

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
                                students.Add(new Students
                                {

                                    FirstName = reader["first_name"] as string,
                                    LastName = reader["last_name"] as string,
                                    Birthday = reader["birthday"] as DateTime?,
                                    Address = reader["address"] as string,
                                    Phone = reader["phone"] as string,
                                    Email = reader["email"] as string


                                });
                            }
                        }
                    }

                }
                finally
                {
                    connection.Close();
                }

                return students;
            }
        }

        public override Students GetById(int id)
        {
            string sqlExpression = "SELECT * FROM students WHERE students_id =" + id.ToString();

            Students student = new Students();

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
                            student = new Students
                            {
                                StudentsId = id,
                                FirstName = reader["first_name"] as string,
                                LastName = reader["last_name"] as string,
                                Birthday = reader["birthday"] as DateTime?,
                                Address = reader["address"] as string,
                                Email = reader["email"] as string
                            };
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }

                return student;
            }

        }
    }
}
