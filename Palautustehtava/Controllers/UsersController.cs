using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palautustehtava.Models;

namespace Palautustehtava.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       // private readonly NorthwindContext db = new NorthwindContext();

        private readonly NorthwindContext db;

        public UsersController(NorthwindContext dbparam)
        {
            db = dbparam;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var users = db.Users;

            foreach (var user in users)
            {
                user.Password = null;
            }
            return Ok(users);
        }


      
        


        


        //haku pääavaimella
        [HttpGet]
        [Route("userid/{Id}")]
        public List<User> GetById(int Id)

        {
           // NorthwindContext db = new NorthwindContext();
            var userId = from p in db.Users
                            where
                            p.UserId == Id
                            select p;
            return userId.ToList();

        }


        // Haku nimellä
        [HttpGet]
        [Route("firstname/{nimi}")]
        public List<User> GetByName(string nimi)
        {
           // NorthwindContext db = new NorthwindContext();
            var firstName = from p in db.Users
                              where
                              p.FirstName == nimi
                              select p;
            return firstName.ToList();
        }


        // Delete

        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteOne(int key)
        {
           // NorthwindContext db = new NorthwindContext();
            User user = db.Users.Find(key);

            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Ok("Käyttäjä " + user.FirstName + " on poistettu");
            }
            else
            {
                return BadRequest("Tuotetta ei löydy");
            }
        }


        [HttpPut]
        [Route("{key}")]
        public ActionResult PutOne(int key, [FromBody] User uusiUser)
        {


           // using (var db = new NorthwindContext())
            //{

                User user = db.Users.Find(key);
                if (user != null)
                {
                    user.FirstName = uusiUser.FirstName;
                    user.LastName = uusiUser.LastName;
                    user.Password = uusiUser.Password;
                    user.Email = uusiUser.Email;
                    user.AccessLevelId = uusiUser.AccessLevelId;
                    user.Username = uusiUser.Username;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound("Käyttäjää ei löydy");
                }
            //}
            return Ok();
        }


        [HttpPost]
        public ActionResult PostCreateNew([FromBody] User u)
        {
            try
            {

                db.Users.Add(u);
                db.SaveChanges();
                return Ok("Lisättiin käyttäjä " + u.Username);
            }
            catch (Exception e)
            {
                return BadRequest("Lisääminen ei onnistunut. Tässä lisätietoa: " + e);
            }
        }
    }
}
