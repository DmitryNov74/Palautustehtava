
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palautustehtava.Models;

namespace Palautustehtava.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("nwproducts/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext db;

        public ProductsController(NorthwindContext dbparam)
        {
            db = dbparam;
        }

        //haku pääavaimella
        [HttpGet]
        [Route("productid/{Id}")]
        public List<Product> GetById(int Id)

        {
           //NorthwindContext db = new NorthwindContext();
            var productId = from p in db.Products where
                            p.ProductId == Id
                            select p;
            return productId.ToList();
    
        }


        // Haku nimellä
        [HttpGet]
        [Route("productname/{nimi}")]
        public List<Product> GetByName(string nimi)
        {
            //NorthwindContext db = new NorthwindContext();
            var productName = from p in db.Products
                            where
                            p.ProductName == nimi
                            select p;
            return productName.ToList();
        }


        // Haku kaikki rivit
        [HttpGet]
        public List<Product> GetAll()
        {
           // NorthwindContext db = new NorthwindContext();
            var kaikki = db.Products;
            return kaikki.ToList();
        }


        // Päivitys

        [HttpPut]
        [Route("{key}")]
        public ActionResult PutOne(int key,[FromBody]Product uusiTuote)
        {
            

            //using (var db = new NorthwindContext())
           // {
                
                Product tuote = db.Products.Find(key);
                if (tuote != null)
                {
                    tuote.ProductName = uusiTuote.ProductName;
                    tuote.UnitPrice = uusiTuote.UnitPrice;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound("Tuotetta ei löydy");
                }
           // }
            return Ok();
        }


     



        // Lisäys
        [HttpPost]
        public ActionResult PostNewProduct(Product tuote)
        {
           

           // using (var db = new NorthwindContext())
           // {
                db.Products.Add(new Product()
                {
                    ProductId = tuote.ProductId,
                    ProductName = tuote.ProductName,
                    UnitPrice = tuote.UnitPrice,
                    
                });

                db.SaveChanges();
          //  }

            return Ok();
        }


        // Delete

        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteOne(int key)
        {
           // NorthwindContext db = new NorthwindContext();
            Product tuote = db.Products.Find(key);

            if(tuote != null)
            {
                db.Products.Remove(tuote);
                db.SaveChanges();
                return Ok("Tuote " + tuote.ProductName + " poistettu");
            }
            else
            {
                return BadRequest("Tuotetta ei löydy");
            }
        }
    }
}
