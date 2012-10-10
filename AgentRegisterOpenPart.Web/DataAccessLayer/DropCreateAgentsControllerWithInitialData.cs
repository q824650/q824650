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
            DateTime dateAdded = DateTime.Now;

            context.Statuses.Add(new Status { Id = 1, Name = "Неактивен" });
            context.Statuses.Add(new Status { Id = 10, Name = "Активен" });

            context.Territories.Add(new Territory { Id = 1, Name = "Центральный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 2, Name = "Южный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 3, Name = "Северо-Западный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 4, Name = "Дальневосточный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 5, Name = "Сибирский Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 6, Name = "Уральский Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 7, Name = "Приволжский Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 8, Name = "Северо-Кавказский Федеральный Округ" });

            List<string> insuranceCompanies = new List<string> { "Аско", "Росно", "Госстрах", "Ингосстрах" };
            int k = 0;
            insuranceCompanies.ForEach(n => context.InsuranceCompanies.Add(new InsuranceCompany { Id = k++, Name = n }));

            string[] products = new string[] { "Каско", "Осаго", "Босаго", "Мосаго" };

            string[] имя = new string[] { "Иван", "Александр", "Олег", "Михаил", "Рамзан", "Исаак", "Ромуальд" };
            string[] отчество = new string[] { "Иванович", "Александрович", "Михайлович", "Рамзанович", "Исаакович", "Ромуальдович" };
            string[] фамилия = new string[] { "Иванов", "Александров", "Козлов", "Михайлов", "Кадыров", "Рабинович", "Козловский" };

            var statAr = context.Statuses.Local.ToArray();
            Random rnd = new Random();

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


                context.Agents.Add(new Agent()
                    {
                        //Id = i,
                        CertificateNumber = rnd.Next(1000000).ToString(),

                        FirstName = имя[rnd.Next(0, имя.Count() - 1)],
                        MiddleName = отчество[rnd.Next(0, отчество.Count() - 1)],
                        LastName = фамилия[rnd.Next(0, фамилия.Count() - 1)],

                        OrganizationHandedCertificate = insuranceCompanies[rnd.Next(0, insuranceCompanies.Count() - 1)],
                        InsuranceCompanyWorksInId = rnd.Next(0, context.InsuranceCompanies.Local.Count() - 1),
                        DateAdded = dateAdded,

                        ProductsWorksWith = productList,
                        RecordValidDeadline = dateAdded.AddYears(rnd.Next(1, 5)),

                        StatusID = statusID,

                        TerritoryWorksAtId = rnd.Next(1, context.Territories.Local.Count()),
                    });
            }

            context.SaveChanges();
        }
    }
}