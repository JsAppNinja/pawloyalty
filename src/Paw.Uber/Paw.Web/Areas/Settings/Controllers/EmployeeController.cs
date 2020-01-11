using Paw.Services.Messages;
using Paw.Services.Messages.Web.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paw.Web.Areas.Settings.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Settings/Employee
        public ActionResult Index(GetEmployeeList getEmployeeList )
        {
            return View(getEmployeeList.ExecuteList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("FormPage", new AddEmployee());
        }

        [HttpPost]
        public ActionResult Add(AddEmployee addEmployee)
        {
            if (this.ModelState.IsValid)
            {
                addEmployee.ExecuteNonQuery();
                return Redirect("/Settings/Employee");
            }

            return View("FormPage", addEmployee);
        }

        [HttpGet]
        public ActionResult Update(GetUpdateEmployee getUpdateEmployee)
        {
            return View("FormPage", getUpdateEmployee);
        }

        [HttpPost]
        public ActionResult Update(UpdateEmployee updateEmployee)
        {
            if (this.ModelState.IsValid)
            {
                updateEmployee.ExecuteNonQuery();
                return Redirect("/Settings/Employee");
            }

            return View("FormPage", updateEmployee);
        }
    }
}