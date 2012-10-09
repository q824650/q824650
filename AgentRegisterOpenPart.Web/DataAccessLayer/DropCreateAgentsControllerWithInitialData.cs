using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using AgentRegisterOpenPart.Web.Models;

namespace AgentRegisterOpenPart.Web.DataAccessLayer
{
    public class DropCreateAgentsControllerWithInitialData: DropCreateDatabaseAlways<AgentContext>
    {
        protected override void Seed(AgentContext context)
        {
            DateTime dateAdded = DateTime.Now;

            context.Statuses.Add(new Status{ Id = 0,  Name = "Неактивен"});
            context.Statuses.Add(new Status{ Id = 10, Name = "Активен"});

            context.Territories.Add(new Territory { Id = 1, Name = "Центральный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 2, Name = "Южный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 3, Name = "Северо-Западный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 4, Name = "Дальневосточный Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 5, Name = "Сибирский Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 6, Name = "Уральский Федеральный Округ" });
            context.Territories.Add(new Territory { Id = 7, Name = "Приволжский Федеральный Округ" });            
            context.Territories.Add(new Territory { Id = 8, Name = "Северо-Кавказский Федеральный Округ" });


            List<Agent> agents = new List<Agent>();
            Random rnd = new Random();

            string[] products = new string [] {"Каско", "Осаго", "Босаго", "Маско"};
            List<string> insuranceCompanies = new List<string> { "Аско", "Росно", "Госстрах", "Ингосстрах" };

            string[] имя = new string[] { "Иван", "Александр", "Олег", "Михаил", "Рамзан", "Исаак", "Ромуальд"/* "Екатерина", "Мария", "Татьяна", "Ольга"*/ };
            string[] отчество = new string[] { "Иванович", "Александрович", "Михайлович", "Рамзанович", "Исаакович", "Ромуальдович" };
            string[] фамилия = new string[] { "Иванов", "Александров", "Козлов", "Михайлов", "Рамзанов", "Рабинович", "Козловский" };

            string productList=string.Empty;
            for (int i = 0; i < 30; i++)
            {

                int numberProducts = rnd.Next(1, products.Count());
                for(int j=0; j < numberProducts-1; j++)
                {
                    int ind = rnd.Next(0, numberProducts-j);
                    StringBuilder sb = new StringBuilder(insuranceCompanies[ind]);
                    string product = sb.ToString();
                    insuranceCompanies.Remove(insuranceCompanies[ind]);
                    productList = string.Join(",", new string[]{productList, product});
                }              //Product


                agents.Add(new Agent()
                //context.Agents.Add(new Agent()
                    {
                        //Id = i,
                        CertificateNumber = rnd.Next(1000000).ToString(),

                        FirstName = имя[rnd.Next(0, имя.Count() - 1)],
                        MiddleName = отчество[rnd.Next(0, отчество.Count() - 1)],
                        LastName = фамилия[rnd.Next(0, фамилия.Count() - 1)],
                        
                        OrganizationHandedCertificate = insuranceCompanies[rnd.Next(0, insuranceCompanies.Count()-1)],
                        InsuranceCompanyWorksInId = rnd.Next(0, insuranceCompanies.Count()-1),
                        DateAdded = dateAdded,
                        
                        ProductsWorksWith = productList,
                        RecordValidDeadline = dateAdded.AddYears(rnd.Next(1, 5)),
                        
                        StatusID = rnd.Next(0,10),
                        TerritoryWorksAtId =  context.Territories.Find(rnd.Next(1, 8)).Id,                        
                    });
            }

            // Fill DB
            AgentContext ac = new AgentContext();
            agents.ForEach(a => ac.Agents.Add(a));
            ac.SaveChanges();
        }
    }
}