using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgentRegisterOpenPart.Web.Models;
using AgentRegisterOpenPart.Web.Utils;

namespace AgentRegisterOpenPart.Web.BusinessLayer
{
    public class AgentSearch
    {
        static char[] Numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        /// <summary>
        /// 1. Splits the search string in words.
        /// 2. Implements the following search logic:
        ///     If a word contains digits, the search is done by the certificate number
        ///     Else search is done by:
        ///     If 1 word   
        ///         By LastName
        ///     If 2 words
        ///         By {FirstName, LastName} OR By {LastName, FirstName}
        ///     If 3 words 
        ///         By {FirstName, MiddleName, LastName} OR By {LastName, MiddleName, FirstName}
        ///     
        /// </summary>
        public static List<Agent> GetAgents(string searchText)
        {
            using (AgentContext db = AgentContext.getInstance())
            {
				IQueryable<Agent> agentsResult = db.Agents
					.Include("InsuranceCompanyWorksIn")
					.Include("TerritoryWorksAt")
					.Include("Status");

                searchText = searchText.Trim();
                if (searchText.ToCharArray().Any(x=>Numbers.Contains(x)))
                {
                    if (HasNotAllowedChars(searchText))
                    {
                        // Уточнить поиск
                        return null;
                    }
                    else
                    {
                        char firstNumber = searchText.ToCharArray().Where(x=>Numbers.Contains(x)).First();
                        int firstNumberIndex = searchText.IndexOf(firstNumber, 0);
                        char lastNumber = searchText.ToCharArray().Where(x=>Numbers.Contains(x)).Last();
                        int lastNumberIndex = searchText.LastIndexOf(lastNumber);
                        
                        // Ищем сертификат по числовой подстроке
                        agentsResult = agentsResult.Where(a =>
                            a.CertificateNumber.StartsWith(searchText.Substring(firstNumberIndex, lastNumberIndex-firstNumberIndex+1)));                            
                    }
                }
                else
                {
                    string[] words = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string w0, w1, w2;
                    if (words.Length == 1)
                    {                        
                        w0 = words[0];
						agentsResult = agentsResult.Where(a =>
                            a.LastName.StartsWith(w0));
                    }
                    else if (words.Length == 2)
                    {
                        w0 = words[0];
                        w1 = words[1];
						agentsResult = agentsResult.Where(a =>
                            (a.FirstName.StartsWith(w0) && a.LastName.StartsWith(w1)) ||
                            (a.FirstName.StartsWith(w1) && a.LastName.StartsWith(w0)));
                    }
                    else if (words.Length == 3)
                    {
                        w0 = words[0];
                        w1 = words[1];
                        w2 = words[2];
						agentsResult = agentsResult.Where(a =>
                            (a.FirstName.StartsWith(w1) && a.MiddleName.StartsWith(w2) && a.LastName.StartsWith(w0)) ||
                            (a.FirstName.StartsWith(w0) && a.MiddleName.StartsWith(w1) && a.LastName.StartsWith(w2)));
                    }
                    else
                    {
                        // Уточнить поиск
                        return null;
                    }
                }

                return agentsResult
                    .OrderBy(a => a.LastName)
                    .ThenBy( a =>  a.FirstName)
                    .ThenBy( a => a.MiddleName)
                    .Take(ConfigurationHelper.MaxAgentsSearchResultSetLength + 1)
                    .ToList();
            }
        }

        private static bool HasNotAllowedChars(string text)
        {
            // TODO: 
            return false;
        }
    }
}