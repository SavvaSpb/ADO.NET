using System.Data;
using System.Data.SqlClient;
using Test_ADO.NET.Models;

namespace Test_ADO.NET.Repositories
{
    public class CoursesRepository : Repository<Courses>
    {
        public override int Add(Courses courses)
        {
            int lastId = 0;

            string sqlExpression = "INSERT INTO courses ( courses_type_name, institute_id, teacher_id, salary ) VALUES  ( @courses_type_name, @institute_id, @teacher_id, @salary ) SELECT SCOPE_IDENTITY()";
            
            using (SqlConnection connection = new SqlConnection(ConStr))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {
                      
                        command.Parameters.Add("@courses_type_name", SqlDbType.VarChar, 255).Value = courses.CoursesTypeName;
                        command.Parameters.Add("@institute_id", SqlDbType.Int).Value = courses.InstituteId;
                        command.Parameters.Add("@teacher_id", SqlDbType.Int).Value = courses.TeacherId;
                        command.Parameters.Add("@salary", SqlDbType.Money).Value = courses.Salary;

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

        public override void Update(int id, Courses courses)
        {
            string sqlExpression = "UPDATE courses SET courses_type_name = @courses_type_name, institute_id = @institute_id, teacher_id = @teacher_id, salary = @salary " +
                                   "WHERE courses_id = @courses_id";

            using (SqlConnection connection = new SqlConnection(ConStr))
            {

                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {
                        command.Parameters.Add("@courses_id", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@courses_type_name", SqlDbType.VarChar, 255).Value = courses.CoursesTypeName;
                        command.Parameters.Add("@institute_id", SqlDbType.Int).Value = courses.InstituteId;
                        command.Parameters.Add("@teacher_id", SqlDbType.Int).Value = courses.TeacherId;
                        command.Parameters.Add("@salary", SqlDbType.Money).Value = courses.Salary;

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

        public override List<Courses> Get()
        {
            string sqlExpression = "SELECT * FROM courses";

            List<Courses> courses = new List<Courses>();

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
                                courses.Add(new Courses
                                {
                                    CoursesTypeName = reader["institute_type_name"] as string,
                                    InstituteId = Convert.ToInt32(reader["institute_id"]),
                                    TeacherId = Convert.ToInt32(reader["teacher_id"]),
                                    Salary = Convert.ToInt32(reader["money"])

                                });
                            }
                        }
                    }

                }
                finally
                {
                    connection.Close();
                }

                return courses;
            }
        }


        public override Courses GetById(int id)
        {
            string sqlExpression = "SELECT * FROM courses" + id.ToString(); ;

            Courses course = new Courses();

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
                            course = new Courses
                            {
                                CoursesId = id,
                                CoursesTypeName = reader["institute_type_name"] as string,
                                InstituteId = Convert.ToInt32(reader["institute_id"]),
                                TeacherId = Convert.ToInt32(reader["teacher_id"]),
                                Salary = Convert.ToInt32(reader["money"])
                            };
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }

                return course;
            }

        }
    }
}
