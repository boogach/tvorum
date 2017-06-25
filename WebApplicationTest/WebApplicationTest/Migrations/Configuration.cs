namespace WebApplicationTest.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplicationTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApplicationTest.Models.ApplicationDbContext context)
        {
            context.Owners.AddOrUpdate(x => x.ID,
            new Models.Owner() { ID = 1, FristName = "Test", LastName = "Test", BirthDate = new DateTime(1989, 5, 5), PhoneNumber = "+5325235235", Email = "test.test@gmail.com", City = "Našice", Address = "Braće Radic 158", PostCode = 31500 }
            );

            context.Ferrets.AddOrUpdate(x => x.ID,
                new Models.Ferret() { ID = 1, FerretName = "Paco", BirthDate = new DateTime(2012, 3, 29), Castration = true, Vaccination = true, VaccLepto = true, OwnerID = 1, CoatColor = "Sable"},
                new Models.Ferret() { ID = 2, FerretName = "Oliver", BirthDate = new DateTime(2015, 7, 12), Castration = true, Vaccination = true, VaccLepto = false, OwnerID = 1, CoatColor = "Dark Sable" },
                new Models.Ferret() { ID = 3, FerretName = "Mirian", BirthDate = new DateTime(2016, 5, 2), Castration = true, Vaccination = true, VaccLepto = false, OwnerID = 1, CoatColor = "Sable" }
            );

            context.Diseases.AddOrUpdate(x => x.ID,
                new Models.Disease() { ID = 1, DiseaseName = "Tupko", YearOfDisease = 2016, FerretID = 1 },
                new Models.Disease() { ID = 2, DiseaseName = "Gastritis", YearOfDisease = 2016, FerretID = 1 },
                new Models.Disease() { ID = 3, DiseaseName = "Glupko", YearOfDisease = 2015, FerretID = 2 },
                new Models.Disease() { ID = 4, DiseaseName = "Tupko", YearOfDisease = 2015, FerretID = 2 },
                new Models.Disease() { ID = 5, DiseaseName = "Glupko", YearOfDisease = 2015, FerretID = 3 },
                new Models.Disease() { ID = 6, DiseaseName = "Glupko", YearOfDisease = 2015, FerretID = 3 },
                new Models.Disease() { ID = 7, DiseaseName = "Tupko", YearOfDisease = 2015, FerretID = 3 }
            );
        }
    }
}
