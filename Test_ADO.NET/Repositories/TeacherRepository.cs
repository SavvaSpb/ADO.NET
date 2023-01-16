using System.Data;
using System.Data.SqlClient;
using Test_ADO.NET.Models;

namespace Test_ADO.NET.Repositories
{
    public class TeacherRepository : Repository<Teacher>
    {
        public override int Add(Teacher teacher)
        {
            int lastId = 0;

            string sqlExpression = "INSERT INTO Teachers ( first_name, last_name, birthday, address, phone, email)" +
                " VALUES ( @first_name, @last_name, @birthday, @address, @phone, @email) SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(ConStr))
            {

                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {
                        command.Parameters.Add("@first_name", SqlDbType.VarChar, 255).Value = teacher.FirstName;
                        command.Parameters.Add("@last_name", SqlDbType.VarChar, 255).Value = teacher.LastName;
                        command.Parameters.Add("@birthday", SqlDbType.Date).Value = teacher.Birthday;
                        command.Parameters.Add("@address", SqlDbType.VarChar, 255).Value = teacher.Address;
                        command.Parameters.Add("@phone", SqlDbType.Char, 15).Value = teacher.Phone;
                        command.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = teacher.Email;

                        //int lastId = (int)command.ExecuteScalar();

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

        public override void Update(int id, Teacher teacher)
        {
            string sqlExpression = "UPDATE teachers SET first_name = @first_name, last_name = @last_name, birthday = @birthday, address = @address, phone = @phone, email = @email  " +
                                  "WHERE teachers_id = @teachers_id";

            using (SqlConnection connection = new SqlConnection(ConStr))
            {

                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {

                        command.Parameters.Add("@teachers_id", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@first_name", SqlDbType.VarChar, 255).Value = teacher.FirstName;
                        command.Parameters.Add("@last_name", SqlDbType.VarChar, 255).Value = teacher.LastName;
                        command.Parameters.Add("@birthday", SqlDbType.Date).Value = teacher.Birthday;
                        command.Parameters.Add("@address", SqlDbType.VarChar, 255).Value = teacher.Address;
                        command.Parameters.Add("@phone", SqlDbType.Char, 15).Value = teacher.Phone;
                        command.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = teacher.Email;

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

        public override List<Teacher> Get()
        {
            string sqlExpression = "SELECT * FROM teachers";

            List<Teacher> teachers = new List<Teacher>();

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
                                teachers.Add(new Teacher
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

                return teachers;

            }
        }

        public override Teacher GetById(int id)
        {
            string sqlExpression = "SELECT * FROM teachers WHERE teachers_id = " + id.ToString();

            Teacher teacher = new Teacher();

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
                            teacher = new Teacher
                            {
                                TeachersId = id,
                                FirstName = reader["first_name"] as string,
                                LastName = reader["last_name"] as string,
                                Birthday = reader["birthday"] as DateTime?,
                                Address = reader["address"] as string,
                                Phone = reader["phone"] as string,
                                Email = reader["email"] as string
                            };
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }

                return teacher;
            }

        }
    }
}
