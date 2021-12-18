using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3_n01489893.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Http.Cors;

namespace Assignment3_n01489893.Controllers
{
    public class TeacherDataController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key) or lower(salary) like lower(@key) or lower(hiredate) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                Decimal TeacherSalary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers
            return Teachers;
        }

        
        /// <summary>
        /// Finds a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"].ToString());
                Decimal TeacherSalary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return NewTeacher;
        }

        /// <summary>
        /// Finds a list of courses in the system given a teacher ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A list of courses</returns>
        [HttpGet]
        public IEnumerable<Course> ListCoursesForTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Courses
            List<Course> Courses = new List<Course> { };

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = (int)ResultSet["classid"];
                string CourseCode = ResultSet["classcode"].ToString();
                DateTime CourseStartDate = Convert.ToDateTime(ResultSet["startdate"].ToString());
                DateTime CourseFinishDate = Convert.ToDateTime(ResultSet["finishdate"].ToString());
                string CourseName = ResultSet["classname"].ToString();

                Course NewCourse = new Course();

                NewCourse.CourseId = CourseId;
                NewCourse.CourseCode = CourseCode;
                NewCourse.CourseStartDate = CourseStartDate;
                NewCourse.CourseFinishDate = CourseFinishDate;
                NewCourse.CourseName = CourseName;

                //Add the Course Name to the List
                Courses.Add(NewCourse);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return Courses;
        }

        /// <summary>
        /// Deletes a Teacher and associated courses from the connected MySQL Database if the ID of that teacher exists. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Delete associated courses to maintain referential integrity
            cmd.CommandText = "Delete from classes where teacherid=@id2";
            cmd.Parameters.AddWithValue("@id2", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Adds a Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Alexander",
        ///	"TeacherLname":"Bennett",
        ///	"EmployeeNumber":"T378",
        ///	"TeacherHireDate":"12/02/21 12:00:00 AM",
        ///	"TeacherSalary":"55.30"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname, @TeacherLname, @EmployeeNumber, @TeacherHireDate, @TeacherSalary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherHireDate", NewTeacher.TeacherHireDate);
            cmd.Parameters.AddWithValue("@TeacherSalary", NewTeacher.TeacherSalary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Updates a Teacher to the MySQL Database.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/1 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Alexander",
        ///	"TeacherLname":"Bennett",
        ///	"EmployeeNumber":"T378",
        ///	"TeacherHireDate":"12/02/21 12:00:00 AM",
        ///	"TeacherSalary":"55.30"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@TeacherHireDate, salary=@TeacherSalary where TeacherId=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherHireDate", TeacherInfo.TeacherHireDate);
            cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
