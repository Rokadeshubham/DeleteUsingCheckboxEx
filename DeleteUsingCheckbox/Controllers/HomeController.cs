using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeleteUsingCheckbox.Models;

namespace DeleteUsingCheckbox.Controllers
{
    public class HomeController : Controller
    {
        EmployeeEntities entities = new EmployeeEntities();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.ListEmployee = entities.Employees.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string[] ids = formCollection["UserId"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var employee = entities.Employees.Find(int.Parse(id));
                entities.Employees.Remove(employee);
                entities.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(Employee emp)
        {
            entities.Employees.Add(emp);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var emp = entities.Employees.FirstOrDefault(x=>x.UserId==id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            entities.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            entities.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}