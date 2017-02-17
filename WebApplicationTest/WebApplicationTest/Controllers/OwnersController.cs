using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class OwnersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Owners
        public IQueryable<OwnerDTO> GetOwners()
        {
            var owners = from o in db.Owners
                         select new OwnerDTO()
                         {
                             Id = o.ID,
                             FullName = o.FristName + " " + o.LastName,
                             FerretName = o.Ferrets.Where(x => x.OwnerID == o.ID).ToList()
                         };
            return owners;
        }

        // GET: api/Owners/5
        [ResponseType(typeof(OwnerDetailsDTO))]
        public async Task<IHttpActionResult> GetOwner(int id)
        {
            var owner = await db.Owners.Select(f =>
                new OwnerDetailsDTO()
                {
                    Id = f.ID,
                    FristName = f.FristName,
                    LastName = f.LastName,
                    BirthDate = f.BirthDate,
                    PhoneNumber = f.PhoneNumber,
                    City = f.City,
                    PostCode = f.PostCode,
                    Address = f.Address,
                    Email = f.Email,
                    Ferrets = f.Ferrets.Where(x => x.OwnerID == id).ToList()

                }).SingleOrDefaultAsync(f => f.Id == id);
            if(owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }

        // PUT: api/Owners/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOwner(int id, Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != owner.ID)
            {
                return BadRequest();
            }

            db.Entry(owner).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Owners
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> PostOwner(Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Owners.Add(owner);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = owner.ID }, owner);
        }

        // DELETE: api/Owners/5
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> DeleteOwner(int id)
        {
            Owner owner = await db.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            db.Owners.Remove(owner);
            await db.SaveChangesAsync();

            return Ok(owner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerExists(int id)
        {
            return db.Owners.Count(e => e.ID == id) > 0;
        }
    }
}