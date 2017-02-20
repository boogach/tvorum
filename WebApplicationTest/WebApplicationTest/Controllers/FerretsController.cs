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
    public class FerretsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Ferrets
        public IQueryable<FerretDTO> GetFerrets()
        {
            var ferrets = from o in db.Ferrets
                         select new FerretDTO()
                         {
                             ID = o.ID,
                             FerretName = o.FerretName,
                             BirthDate = o.BirthDate,
                             Disease = o.Diseases.Where(x => x.FerretID == o.ID).Select(y => y.DiseaseName).ToList()
                         };
            return ferrets;
        }

        [Route("api/ferrets/{diseaseName}/disease")]
        public IEnumerable<FerretDTO> GetFerretsByDisease(string diseaseName)
        {
            var ferrets = from o in db.Ferrets
                          select new FerretDTO()
                          {
                              ID = o.ID,
                              FerretName = o.FerretName,
                              BirthDate = o.BirthDate,
                              Disease = o.Diseases.Where(x => x.FerretID == o.ID).Select(y => y.DiseaseName).ToList()
                          };
            return ferrets.Where(x => x.Disease.Contains(diseaseName)).ToList();
        }


        // GET: api/Ferrets/5
        [ResponseType(typeof(FerretDTO))]
        public async Task<IHttpActionResult> GetFerret(int id)
        {
            var ferret = await db.Ferrets.Select(f =>
                           new FerretDetailsDTO()
                           {
                                ID = f.ID,
                                FerretName = f.FerretName,
                                BirthDate = f.BirthDate,
                                Castration = f.Castration,
                                CoatColor = f.CoatColor,
                                Diseases = f.Diseases.Where(x => x.FerretID == id).ToList(),
                                OwnerID = f.OwnerID,
                                OwnerName = f.Owner.FristName + " " + f.Owner.LastName,
                                Vaccination = f.Vaccination,
                                VaccLepto = f.VaccLepto

                           }).SingleOrDefaultAsync(f => f.ID == id);
            if (ferret == null)
            {
                return NotFound();
            }

            return Ok(ferret);
        }

        // PUT: api/Ferrets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFerret(int id, Ferret ferret)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ferret.ID)
            {
                return BadRequest();
            }

            db.Entry(ferret).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FerretExists(id))
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

        // POST: api/Ferrets
        [ResponseType(typeof(Ferret))]
        public async Task<IHttpActionResult> PostFerret(Ferret ferret)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ferrets.Add(ferret);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ferret.ID }, ferret);
        }

        // DELETE: api/Ferrets/5
        [ResponseType(typeof(Ferret))]
        public async Task<IHttpActionResult> DeleteFerret(int id)
        {
            Ferret ferret = await db.Ferrets.FindAsync(id);
            if (ferret == null)
            {
                return NotFound();
            }

            db.Ferrets.Remove(ferret);
            await db.SaveChangesAsync();

            return Ok(ferret);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FerretExists(int id)
        {
            return db.Ferrets.Count(e => e.ID == id) > 0;
        }
    }
}