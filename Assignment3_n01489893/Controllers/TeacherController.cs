using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_n01489893.Models;
using System.Diagnostics;

namespace Assignment3_n01489893.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/SearchResult
        public ActionResult SearchResult(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            IEnumerable<Course> NewTeachCourses = controller.ListCoursesForTeacher(id);

            return View(Tuple.Create(NewTeacher, NewTeachCourses));
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //GET : /Teacher/Ajax_DeleteConfirm/{id}
        public ActionResult Ajax_DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/Add
        public ActionResult Add()
        {
            return View();
        }

        //GET : /Teacher/Ajax_Add
        public ActionResult Ajax_Add()
        {
            return View();

        }

        //GET : /Teacher/AddError
        public ActionResult AddError()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime TeacherHireDate, Decimal TeacherSalary = 0)
        {
            List<string> errors = new List<string>();

            // Server side validation
            // Note: I am not able to assign a default value to TeacherHireDate (tried DateTime.MinValue/Default(DateTime), both didn't work) or make it nullable
            // Server throws an error before reaching validation so I skipped the validation of TeacherHireDate.

            // Validate TeacherFname
            if (string.IsNullOrEmpty(TeacherFname))
            {
                errors.Add("First Name is mssing.");
            }
            // Validate TeacherLname
            if (string.IsNullOrEmpty(TeacherLname))
            {
                errors.Add("Last Name is mssing.");
            }
            // Validate EmployeeNumber
            if (string.IsNullOrEmpty(EmployeeNumber))
            {
                errors.Add("Employee Number is mssing.");
            }
            //Validate TeacherSalary
            if (TeacherSalary == 0)
            {
                errors.Add("Salary is mssing.");
            }

            if (errors.Count > 0)
            {
                // Route to AddError page if any of the input is invalid
                return View("AddError", errors);
                // Note: I would like it to redirect to the form with user's previous input pre-populated but I couldn't figure it out.
            }
            else
            {
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;

                TeacherDataController controller = new TeacherDataController();
                controller.AddTeacher(NewTeacher);

                return RedirectToAction("List");
            }
        }
    }
}