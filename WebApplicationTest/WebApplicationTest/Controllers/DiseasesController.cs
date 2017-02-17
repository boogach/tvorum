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
    public class DiseasesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Diseases
        public IQueryable<DiseaseDTO> GetDiseases()
        {
            var diseases = from o in db.Diseases.Include(x => x.Ferret)
                          select new DiseaseDTO()
                          {
                                Id = o.ID,
                                DiseaseName = o.DiseaseName,
                                Ferret = o.Ferret.FerretName
                          };
            return diseases;
        }

        [Route("api/diseases/{diseaseName}")]
        public IEnumerable<DiseaseDTO> GetDiseaseByName(string diseaseName)
        {
            var dis = from o in db.Diseases
                      select new DiseaseDTO()
                      {
                          Id = o.ID,
                          Ferret = o.Ferret.FerretName,
                          DiseaseName = o.DiseaseName
                      };
            return dis.Where(x => x.DiseaseName == diseaseName).ToList();
        }

        // GET: api/Diseases/5
        [ResponseType(typeof(DiseaseDetailDTO))]
        public async Task<IHttpActionResult> GetDisease(int id)
        {
            var disease = await db.Diseases.Select(f =>
                            new DiseaseDetailDTO()
                            {
                                ID = f.ID,
                                Ferret = f.Ferret.FerretName,
                                FerretId = f.Ferret.ID,
                                DiseaseName = f.DiseaseName

                            }).SingleOrDefaultAsync(f => f.ID == id);
            if (disease == null)
            {
                return NotFound();
            }

            return Ok(disease); ;
        }

        // PUT: api/Diseases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDisease(int id, Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disease.ID)
            {
                return BadRequest();
            }

            db.Entry(disease).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseExists(id))
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

        // POST: api/Diseases
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> PostDisease(Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diseases.Add(disease);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = disease.ID }, disease);
        }

        // DELETE: api/Diseases/5
        [ResponseType(typeof(Disease))]
        public async Task<IHttpActionResult> DeleteDisease(int id)
        {
            Disease disease = await db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            db.Diseases.Remove(disease);
            await db.SaveChangesAsync();

            return Ok(disease);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiseaseExists(int id)
        {
            return db.Diseases.Count(e => e.ID == id) > 0;
        }
    }
}