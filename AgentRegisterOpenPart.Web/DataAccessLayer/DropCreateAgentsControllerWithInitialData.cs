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
            context.Database.ExecuteSqlCommand
            ("CREATE INDEX IX_Agents_CertificateNumber  ON Agents (CertificateNumber);"+ Environment.NewLine +
             "CREATE INDEX IX_Agents_FirstName          ON Agents (FirstName);" + Environment.NewLine +
             //"CREATE INDEX IX_Agents_MiddleName         ON Agents (MiddleName);" + Environment.NewLine +
             "CREATE INDEX IX_Agents_LastName           ON Agents (LastName);" +
             "CREATE INDEX IX_Territories_KLADRCode     ON Territories (KLADRCode);");
            
            context.Statuses.Add(new Status { Id = 1, Name = "Неактивен" });
            context.Statuses.Add(new Status { Id = 10, Name = "Активен" });

            context.Territories.Add(new Territory { KLADRCode = "0100000000000", Name = "Республика Адыгея" });
            context.Territories.Add(new Territory { KLADRCode = "0400000000000", Name = "Республика Алтай" });
            context.Territories.Add(new Territory { KLADRCode = "2200000000000", Name = "Алтайский край" });
            context.Territories.Add(new Territory { KLADRCode = "2800000000000", Name = "Амурская область" });
            context.Territories.Add(new Territory { KLADRCode = "2900000000000", Name = "Архангельская область" });
            context.Territories.Add(new Territory { KLADRCode = "3000000000000", Name = "Астраханская область" });
            context.Territories.Add(new Territory { KLADRCode = "9900000000000", Name = "Байконур" });
            context.Territories.Add(new Territory { KLADRCode = "0200000000000", Name = "Республика Башкортостан" });
            context.Territories.Add(new Territory { KLADRCode = "3100000000000", Name = "Белгородская область" });
            context.Territories.Add(new Territory { KLADRCode = "3200000000000", Name = "Брянская область" });
            context.Territories.Add(new Territory { KLADRCode = "0300000000000", Name = "Республика Бурятия" });
            context.Territories.Add(new Territory { KLADRCode = "3300000000000", Name = "Владимирская область" });
            context.Territories.Add(new Territory { KLADRCode = "3400000000000", Name = "Волгоградская область" });
            context.Territories.Add(new Territory { KLADRCode = "3500000000000", Name = "Вологодская область" });
            context.Territories.Add(new Territory { KLADRCode = "3600000000000", Name = "Воронежская область" });
            context.Territories.Add(new Territory { KLADRCode = "0500000000000", Name = "Республика Дагестан" });
            context.Territories.Add(new Territory { KLADRCode = "7900000000000", Name = "Еврейская автономная область" });
            context.Territories.Add(new Territory { KLADRCode = "7500000000000", Name = "Забайкальский край" });
            context.Territories.Add(new Territory { KLADRCode = "3700000000000", Name = "Ивановская область" });
            context.Territories.Add(new Territory { KLADRCode = "0600000000000", Name = "Республика Ингушетия" });
            context.Territories.Add(new Territory { KLADRCode = "3800000000000", Name = "Иркутская область" });
            context.Territories.Add(new Territory { KLADRCode = "0700000000000", Name = "Кабардино-Балкарская Республика" });
            context.Territories.Add(new Territory { KLADRCode = "3900000000000", Name = "Калининградская область" });
            context.Territories.Add(new Territory { KLADRCode = "0800000000000", Name = "Республика Калмыкия" });
            context.Territories.Add(new Territory { KLADRCode = "4000000000000", Name = "Калужская область" });
            context.Territories.Add(new Territory { KLADRCode = "4100000000000", Name = "Камчатский край" });
            context.Territories.Add(new Territory { KLADRCode = "0900000000000", Name = "Карачаево-Черкесская Республика" });
            context.Territories.Add(new Territory { KLADRCode = "1000000000000", Name = "Республика Карелия" });
            context.Territories.Add(new Territory { KLADRCode = "4200000000000", Name = "Кемеровская область" });
            context.Territories.Add(new Territory { KLADRCode = "4300000000000", Name = "Кировская область" });
            context.Territories.Add(new Territory { KLADRCode = "1100000000000", Name = "Республика Коми" });
            context.Territories.Add(new Territory { KLADRCode = "4400000000000", Name = "Костромская область" });
            context.Territories.Add(new Territory { KLADRCode = "2300000000000", Name = "Краснодарский край" });
            context.Territories.Add(new Territory { KLADRCode = "2400000000000", Name = "Красноярский край" });
            context.Territories.Add(new Territory { KLADRCode = "4500000000000", Name = "Курганская область" });
            context.Territories.Add(new Territory { KLADRCode = "4600000000000", Name = "Курская область" });
            context.Territories.Add(new Territory { KLADRCode = "4700000000000", Name = "Ленинградская область" });
            context.Territories.Add(new Territory { KLADRCode = "4800000000000", Name = "Липецкая область" });
            context.Territories.Add(new Territory { KLADRCode = "4900000000000", Name = "Магаданская область" });
            context.Territories.Add(new Territory { KLADRCode = "1200000000000", Name = "Республика Марий Эл" });
            context.Territories.Add(new Territory { KLADRCode = "1300000000000", Name = "Республика Мордовия" });
            context.Territories.Add(new Territory { KLADRCode = "7700000000000", Name = "Москва" });
            context.Territories.Add(new Territory { KLADRCode = "5000000000000", Name = "Московская область" });
            context.Territories.Add(new Territory { KLADRCode = "5100000000000", Name = "Мурманская область" });
            context.Territories.Add(new Territory { KLADRCode = "8300000000000", Name = "Ненецкий автономная область" });
            context.Territories.Add(new Territory { KLADRCode = "5200000000000", Name = "Нижегородская область" });
            context.Territories.Add(new Territory { KLADRCode = "5300000000000", Name = "Новгородская область" });
            context.Territories.Add(new Territory { KLADRCode = "5400000000000", Name = "Новосибирская область" });
            context.Territories.Add(new Territory { KLADRCode = "5500000000000", Name = "Омская область" });
            context.Territories.Add(new Territory { KLADRCode = "5600000000000", Name = "Оренбургская область" });
            context.Territories.Add(new Territory { KLADRCode = "5700000000000", Name = "Орловская область" });
            context.Territories.Add(new Territory { KLADRCode = "5800000000000", Name = "Пензенская область" });
            context.Territories.Add(new Territory { KLADRCode = "5900000000000", Name = "Пермский край" });
            context.Territories.Add(new Territory { KLADRCode = "2500000000000", Name = "Приморский край" });
            context.Territories.Add(new Territory { KLADRCode = "6000000000000", Name = "Псковская область" });
            context.Territories.Add(new Territory { KLADRCode = "6100000000000", Name = "Ростовская область" });
            context.Territories.Add(new Territory { KLADRCode = "6200000000000", Name = "Рязанская область" });
            context.Territories.Add(new Territory { KLADRCode = "6300000000000", Name = "Самарская область" });
            context.Territories.Add(new Territory { KLADRCode = "7800000000000", Name = "Санкт-Петербург" });
            context.Territories.Add(new Territory { KLADRCode = "6400000000000", Name = "Саратовская область" });
            context.Territories.Add(new Territory { KLADRCode = "1400000000000", Name = "Республика Саха (Якутия)" });
            context.Territories.Add(new Territory { KLADRCode = "6500000000000", Name = "Сахалинская область" });
            context.Territories.Add(new Territory { KLADRCode = "6600000000000", Name = "Свердловская область" });
            context.Territories.Add(new Territory { KLADRCode = "1500000000000", Name = "Республика Северная Осетия - Алания" });
            context.Territories.Add(new Territory { KLADRCode = "6700000000000", Name = "Смоленская область" });
            context.Territories.Add(new Territory { KLADRCode = "2600000000000", Name = "Ставропольский край" });
            context.Territories.Add(new Territory { KLADRCode = "6800000000000", Name = "Тамбовская область" });
            context.Territories.Add(new Territory { KLADRCode = "1600000000000", Name = "Республика Татарстан" });
            context.Territories.Add(new Territory { KLADRCode = "6900000000000", Name = "Тверская область" });
            context.Territories.Add(new Territory { KLADRCode = "7000000000000", Name = "Томская область" });
            context.Territories.Add(new Territory { KLADRCode = "7100000000000", Name = "Тульская область" });
            context.Territories.Add(new Territory { KLADRCode = "1700000000000", Name = "Республика Тыва" });
            context.Territories.Add(new Territory { KLADRCode = "7200000000000", Name = "Тюменская область" });
            context.Territories.Add(new Territory { KLADRCode = "1800000000000", Name = "Удмуртская Республика" });
            context.Territories.Add(new Territory { KLADRCode = "7300000000000", Name = "Ульяновская область" });
            context.Territories.Add(new Territory { KLADRCode = "2700000000000", Name = "Хабаровский край" });
            context.Territories.Add(new Territory { KLADRCode = "1900000000000", Name = "Республика Хакасия" });
            context.Territories.Add(new Territory { KLADRCode = "8600000000000", Name = "Ханты-Мансийский Автономный округ - Югра" });
            context.Territories.Add(new Territory { KLADRCode = "7400000000000", Name = "Челябинская область" });
            context.Territories.Add(new Territory { KLADRCode = "2000000000000", Name = "Чеченская Республика" });
            context.Territories.Add(new Territory { KLADRCode = "2100000000000", Name = "Чувашская Республика" });
            context.Territories.Add(new Territory { KLADRCode = "8700000000000", Name = "Чукотский автономная область" });
            context.Territories.Add(new Territory { KLADRCode = "8900000000000", Name = "Ямало-Ненецкий автономная область" });
            context.Territories.Add(new Territory { KLADRCode = "7600000000000", Name = "Ярославская область" });

            List<string> insuranceCompanies = new List<string> { 
                "Allianz РОСНО Жизнь", "АльфаСтрахование", "Альянс (страховая компания)", "ВСК", "ВТБ Страхование", "Гута-Страхование", 
                "Дженерали ППФ Страхование жизни", "Защита-Находка", "Ингосстрах", "Интач Страхование", "МАКС (страховая компания)", 
                "МетЛайф Алико (страховая компания)", "Находка Ре", "Национальная страховая группа", /*"Реестр субъектов страхового дела",*/
                "Ренессанс Страхование", "РЕСО-Гарантия", "Росгосстрах", "Россия (страховая компания)", "Согаз", "Согласие (компания)", 
                "Страховая группа МСК", "Страховое общество ЖАСО", "Уралсиб (СГ)", "Югория" };

            
            int k = 0;
            insuranceCompanies.ForEach(n => context.InsuranceCompanies.Add(new InsuranceCompany { Id = k++, Name = n }));

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
                        //Id = i,
                        AgencyAgreementNumber = rnd.Next(1000000).ToString(),
                        CertificateNumber = rnd.Next(1000000).ToString(),

                        FirstName = имя[rnd.Next(0, имя.Count() - 1)],
                        MiddleName = отчество[rnd.Next(0, отчество.Count() - 1)],
                        LastName = фамилия[rnd.Next(0, фамилия.Count() - 1)],

                        OrganizationHandedCertificate = insuranceCompanies[rnd.Next(0, insuranceCompanies.Count() - 1)],
                        InsuranceCompanyWorksInId = rnd.Next(0, context.InsuranceCompanies.Local.Count() - 1),
                        DateAddedToRegister = baseDate.AddMonths(rnd.Next(1,59)),

                        ProductsWorksWith = productList,
                        TimeLengthCertificateValid = rnd.Next(1, 40),
                        StatusID = statusID,

                        TerritoryWorksAtKLADRCode = territoryWorksAt.KLADRCode,
                    });
            }

            context.SaveChanges();
        }
    }
}