using corecrud.dbdata;
using corecrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace corecrud.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DemoContext obj = new DemoContext();  // database obj
            List<EmpModel> empobj = new List<EmpModel>();   //model obj
            var res = obj.Mytables.ToList();
            foreach (var item in res)
            {
                empobj.Add(new EmpModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address


                });
            }
            return View("empobj");
        }

        [HttpGet]
        public ActionResult AddEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmp(EmpModel obj)    //this is add method
        {
            DemoContext dataobj = new DemoContext();
            Mytable tbl = new Mytable();
            tbl.Id = obj.Id;
            tbl.Name = obj.Name;
            tbl.Email = obj.Email;
            tbl.Address = obj.Address;


            if (obj.Id == 0)
            {
                dataobj.Mytables.Add(tbl);
                dataobj.SaveChanges();
            }
            else
            {
                dataobj.Entry(tbl).State = EntityState.Modified;
                dataobj.SaveChanges();
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult EditEmp(int id)
        {
            DemoContext dbobj = new DemoContext();
            //  Mytable obj = new Mytable();
            EmpModel Emod = new EmpModel();

            var res = dbobj.Mytables.Where(a => a.Id == id).First();

            Emod.Id = res.Id;
            Emod.Name = res.Name;
            Emod.Email = res.Email;
            Emod.Address = res.Address;

            return View("AddEmp", Emod);
        }

        public ActionResult DelEmp(int id)
        {
            DemoContext dbobj = new DemoContext();
            var res = dbobj.Mytables.Where(b => b.Id == id).First();
            dbobj.Mytables.Remove(res);
            dbobj.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}