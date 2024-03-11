using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.Controllers
{
    /*
     * This class inherits from Controller class which is provided by ASP.NET MVC framework.
     */

    public class HomeController : Controller
    {
        /*
         * DBContext Class derived from Entity Framework responsible for interacting with 
         * the Database.An instance class is created. 
         */

        MVCCRUDDBContext _context = new MVCCRUDDBContext();

        /*
         * Index Action Method is invoked when the URL corresponding to this controller 
         * is accessed without specifying an action.
         */

        public ActionResult Index()
        {
            /* Retrives the list of students from the database using the _context object &
             * passes it to the associated view (Index.cshtml)
             */

            var listOfData = _context.Students.ToList();
            return View(listOfData); // List is then passed to the View.
        }

        /*
         * This method is used to display the form for creating a new Student Object. 
         * This method returns a view that contains the form for inputting data to create a 
         * new Student object.
         */

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /*
         * This method is invoked when the form for creating a new Student is submitted.
         * It receives a Student object (model) as a parameter, which contains the data entered into the form.
         */

        [HttpPost]
        public ActionResult Create(Student model)
        {
            _context.Students.Add(model); // The received object is added to the _context
            _context.SaveChanges(); // Then saved to the database. 
            ViewBag.Message = "Data Inserted Successfully"; // Display the message
            return View();
        }

        /*
         * This method is used to retrieve the data of a specific student with the given id
         * from the database.
         */

        [HttpGet]
        public ActionResult Edit(int id)
        {
            /* 
             * It uses the _context object to query the Students DbSet, filtering based on the provided id.
             * The .FirstOrDefault() method is used to retrieve the first or default student that matches the given id.
             */

            var data = _context.Students.Where(x => x.StudentID == id).FirstOrDefault();
            return View(data);
        }
        /* 
         * This method is invoked when the form for editing a student is submitted.
         */

        [HttpPost]
        public ActionResult Edit(Student model)
        {
            var data = _context.Students.Where(x => x.StudentID == model.StudentID).FirstOrDefault();
            if(data != null)
            {
                data.StudentName = model.StudentName;
                data.StudentFees = model.StudentFees;
                data.StudentCity = model.StudentCity;
                _context.SaveChanges();
                ViewBag.Message = "Updated records successfully for ID: " + data.StudentID;
            }
            // The method redirects the user to the Index action, which typically displays a list of students after editing.
            return RedirectToAction("Index");  
        }

        public ActionResult Read(int id)
        {
            var data = _context.Students.Where(x => x.StudentID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _context.Students.Where(x => x.StudentID == id).FirstOrDefault();
            _context.Students.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Record" + id + "Deleted Succesfully";
            return RedirectToAction("Index");
        }
    }
}