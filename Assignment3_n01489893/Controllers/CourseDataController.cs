using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3_n01489893.Models;
using MySql.Data.MySqlClient;

namespace Assignment3_n01489893.Controllers
{
    public class CourseDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the classes table of our school database.
        /// <summary>
        /// Returns a list of Courses in the system
        /// </summary>
        /// <example>GET api/CourseData/ListCourses</example>
        /// <returns>
        /// A list of courses
        /// </returns>
        [HttpGet]
        public IEnumerable<Course> ListCourses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Courses
            List<Course> Courses = new List<Course> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = Convert.ToInt32(ResultSet["classid"]);
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

            //Return the final list of courses
            return Courses;
        }

        /// <summary>
        /// Finds a course in the system given an ID
        /// </summary>
        /// <param name="id">The course primary key</param>
        /// <returns>A course object</returns>
        [HttpGet]
        public Course FindCourse(int id)
        {
            Course NewCourse = new Course();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int CourseId = Convert.ToInt32(ResultSet["classid"]);
                string CourseCode = ResultSet["classcode"].ToString();
                DateTime CourseStartDate = Convert.ToDateTime(ResultSet["startdate"].ToString());
                DateTime CourseFinishDate = Convert.ToDateTime(ResultSet["finishdate"].ToString());
                string CourseName = ResultSet["classname"].ToString();

                NewCourse.CourseId = CourseId;
                NewCourse.CourseCode = CourseCode;
                NewCourse.CourseStartDate = CourseStartDate;
                NewCourse.CourseFinishDate = CourseFinishDate;
                NewCourse.CourseName = CourseName;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return NewCourse;
        }
    }
}
