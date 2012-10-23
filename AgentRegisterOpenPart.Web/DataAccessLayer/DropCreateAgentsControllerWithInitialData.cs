using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using AgentRegisterOpenPart.Web.Models;

namespace AgentRegisterOpenPart.Web.DataAccessLayer
{
    public class DropCreateAgentsControllerWithInitialData : DropCreateDatabaseAlways<AgentContext>
    {
        protected override void Seed(AgentContext context)
        {
            InitDatabase.CreateIndicesSeedLookups(context);
            string[] products = new string[] { "ОСАГО", "ДСАГО", "КАСКО", "ВЗР" };

            string[] имя = new string[] { "Иван", "Александр", "Олег", "Михаил", "Рамзан", "Исаак", "Ромуальд" };
            string[] отчество = new string[] { "Иванович", "Александрович", "Михайлович", "Рамзанович", "Исаакович", "Ромуальдович" };
            string[] фамилия = new string[] { "Иванов", "Александров", "Козлов", "Михайлов", "Кадыров", "Рабинович", "Козловский" };

            var statAr = context.Statuses.Local.ToArray();
            Random rnd = new Random();

            DateTime baseDate = DateTime.Now.AddYears(-3);


            for (int i = 0; i < 30; i++)
            {
                List<string> productsCopy = new List<string>(products);

                int numberProducts = rnd.Next(1, productsCopy.Count());
                string productList = string.Empty;
                for (int j = 0; j < numberProducts; j++)
                {
                    int ind = rnd.Next(0, numberProducts - j - 1);
                    string product = productsCopy[ind];
                    productList = (productList == string.Empty) ? product : productList+","+ product;
                    productsCopy.Remove(product);
                }              //Product                

                int statusID = statAr[rnd.Next(0, context.Statuses.Local.Count() - 1)].Id;

                int territoryWorksAtNum = rnd.Next(0, context.Territories.Local.Count()-1);
                var territoryWorksAt = context.Territories.Local.Skip(territoryWorksAtNum).Take(1).Single();

                context.Agents.Add(new Agent()
                    {                        
                        AgencyAgreementNumber = rnd.Next(1000000).ToString(),
                        CertificateNumber = rnd.Next(1000000).ToString(),

                        FirstName = имя[rnd.Next(0, имя.Count() - 1)],
                        MiddleName = отчество[rnd.Next(0, отчество.Count() - 1)],
                        LastName = фамилия[rnd.Next(0, фамилия.Count() - 1)],

                        OrganizationHandedCertificate = context.InsuranceCompanies.Local[rnd.Next(0, context.InsuranceCompanies.Local.Count() - 1)].Name,
                        InsuranceCompanyWorksInId = rnd.Next(0, context.InsuranceCompanies.Local.Count() - 1),
                        DateCertificateExpires = baseDate.AddMonths(rnd.Next(1,59)),

                        ProductsWorksWith = productList,                     
                        StatusID = statusID,

                        TerritoryWorksAtKLADRCode = territoryWorksAt.KLADRCode,
                    });
            }

            context.SaveChanges();
        }
    }
}