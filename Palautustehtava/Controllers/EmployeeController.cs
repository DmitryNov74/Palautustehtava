using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palautustehtava.Models;

namespace Palautustehtava.Controllers
{
    [Route("nwemployee/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        //haku pääavaimella
        [HttpGet]
        [Route("employeeid/{Id}")]
        public List<Employee> GetByEmpId(int Id)

        {
            NorthwindContext db = new NorthwindContext();
            var employeeId = from e in db.Employees
                            where
      e.EmployeeId == Id
                            select e;
            return employeeId.ToList();

        }


        // Haku nimellä
        [HttpGet]
        [Route("lastname/{nimi}")]
        public List<Employee> GetByEmpName(string nimi)
        {
            NorthwindContext db = new NorthwindContext();
            var employeeName = from e in db.Employees
                            where
                            e.LastName == nimi
                            select e;
            return employeeName.ToList();
        }


        // Haku kaikki rivit
        [HttpGet]
        public List<Employee> GetAllEmployees()
        {
            NorthwindContext db = new NorthwindContext();
            var kaikki = db.Employees;
            return kaikki.ToList();
        }

        // Päivitys
        [HttpPut]
        [Route("key")]
        public ActionResult PutNewInfo(int key, [FromBody] Employee uusiTieto)
        {


            using (var db = new NorthwindContext())
            {

                Employee tyontekija = db.Employees.Find(key);
                if (tyontekija != null)
                {
                    tyontekija.LastName = uusiTieto.LastName;
                    tyontekija.FirstName = uusiTieto.FirstName;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound("Työntekijää ei löydy");
                }
            }
            return Ok();
        }


        // Lisäys
        [HttpPost]
        public ActionResult PostNewEmployee(Employee tyontekija)
        {


            using (var db = new NorthwindContext())
            {
                db.Employees.Add(new Employee()
                {
                    EmployeeId = tyontekija.EmployeeId,
                    LastName = tyontekija.LastName,
                    FirstName = tyontekija.FirstName,

                });

                db.SaveChanges();
            }

            return Ok();
        }


        // Delete

        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteEmployee(int key)
        {
            NorthwindContext db = new NorthwindContext();
            Employee tyontekija = db.Employees.Find(key);

            if (tyontekija != null)
            {
                db.Employees.Remove(tyontekija);
                db.SaveChanges();
                return Ok("Työntekijä " + tyontekija.LastName + " poistettu");
            }
            else
            {
                return BadRequest("Työntekijää ei löydy");
            }
        }
    }
}
