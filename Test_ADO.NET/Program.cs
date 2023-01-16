using Test_ADO.NET.Models;
using Test_ADO.NET.Repositories;

namespace Test_ADO.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TeacherRepository teacherRepository = new TeacherRepository();

            string zapros = "";

            Console.WriteLine("Enter request:");
            zapros = Console.ReadLine();

            
            switch (zapros)
            {
                // enum //
                case "GetAllTeachers":

                    Console.WriteLine("All teachers:");

                    var newTeacherFirst = teacherRepository.Get();

                    foreach (var row in newTeacherFirst)
                    {
                        Console.WriteLine($"FirstName:{row.FirstName} , LastName:{row.LastName} , Birthday: {row.Birthday} , Address: {row.Address} , Phone: {row.Phone} , Email: {row.Email}");
                    }
                    break;


                case "GetTeacherById":

                    Console.WriteLine("Teacher:");

                    int id = Console.Read();

                    var newTeacherSecond = teacherRepository.GetById(id);

                    Console.WriteLine($"FirstName: {newTeacherSecond.FirstName}, LastName:{newTeacherSecond.LastName} , Birthday: {newTeacherSecond.Birthday} , Address: {newTeacherSecond.Address} , Phone: {newTeacherSecond.Phone} , Email: {newTeacherSecond.Email}");

                    break;


                case "AddTeacher":

                    Console.WriteLine("Request form: FirstName LastName Birthday Address Phone Email");

                    string newTeacherString = Console.ReadLine();
                    
                    string[] parameters = newTeacherString.Split(' ');

                    Teacher newTeacherThird = new Teacher();
                    newTeacherThird.FirstName = parameters[0];
                    newTeacherThird.LastName = parameters[1];
                    newTeacherThird.Birthday = Convert.ToDateTime(parameters[2]);
                    newTeacherThird.Address = parameters[3];
                    newTeacherThird.Phone = parameters[4];
                    newTeacherThird.Email = parameters[5];

                    teacherRepository.Add(newTeacherThird);

                    break;


                case "UpdateTeacher":

                    Console.WriteLine("Request form: TeachersId FirstName LastName Birthday Address Phone Email");

                    string newTeacherStringSecond = Console.ReadLine();
                    string[] parametersSecond = newTeacherStringSecond.Split(' ');

                    Teacher newTeacherFourth = new Teacher();

                    newTeacherFourth.TeachersId =  Convert.ToInt32(parametersSecond[0]);
                    newTeacherFourth.FirstName = parametersSecond[1];
                    newTeacherFourth.LastName = parametersSecond[2];
                    newTeacherFourth.Birthday = Convert.ToDateTime(parametersSecond[3]);
                    newTeacherFourth.Address = parametersSecond[4];
                    newTeacherFourth.Phone = parametersSecond[5];
                    newTeacherFourth.Email = parametersSecond[6];

                    teacherRepository.Update(newTeacherFourth.TeachersId, newTeacherFourth);

                    break;
            }
        }
    }
}

// GetAllTeachers -> Print teachers info                   foreach
// GetTeacherById -> read Id from console
// Hv Hv 1900-05-05 London +374555333 hkdjdj@gmail.com