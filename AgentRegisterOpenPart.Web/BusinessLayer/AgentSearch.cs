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
            using (AgentContext db = new AgentContext())
            {
				IQueryable<Agent> agentsResult = db.Agents
					.Include("InsuranceCompanyWorksIn")
					.Include("TerritoryWorksAt")
					.Include("Status");

                searchText = searchText.Trim();
                if (HasDigits(searchText))
                {
                    if (HasNotAllowedChars(searchText))
                    {
                        // Уточнить поиск
                        return null;
                    }
                    else
                    {
                        // Ищем сертификат по числовой подстроке
                        agentsResult = agentsResult.Where(a =>
                            a.CertificateNumber.Contains(searchText));
                    }
                }
                else
                {
                    string[] words = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string w0, w1, w2;
                    if (words.Length == 1)
                    {                        
                        w0 = words[0].ToLower();
						agentsResult = agentsResult.Where(a =>
                            a.LastName.ToLower() == w0);
                    }
                    else if (words.Length == 2)
                    {
                        w0 = words[0].ToLower();
                        w1 = words[1].ToLower();
						agentsResult = agentsResult.Where(a =>
                            (a.FirstName.ToLower() == w0 && a.LastName.ToLower() == w1) ||
                            (a.FirstName.ToLower() == w1 && a.LastName.ToLower() == w0));
                    }
                    else if (words.Length == 3)
                    {
                        w0 = words[0].ToLower();
                        w1 = words[1].ToLower();
                        w2 = words[2].ToLower();
						agentsResult = agentsResult.Where(a =>
                            (a.FirstName.ToLower() == w1 && a.MiddleName.ToLower() == w2 && a.LastName.ToLower() == w0) ||
                            (a.FirstName.ToLower() == w0 && a.MiddleName.ToLower() == w1 && a.LastName.ToLower() == w2));
                    }
                    else
                    {
                        // Уточнить поиск
                        return null;
                    }
                }

                return agentsResult
                    .OrderBy(a => a.LastName + " " + a.FirstName + " " + a.MiddleName)
                    .Take(ConfigurationHelper.MaxAgentsSearchResultSetLength + 1)
                    .ToList();
            }
        }

        public static List<Agent> GetAgentsOld(string searchText)
        {
            using (AgentContext db = new AgentContext())
            {
                IQueryable<Agent> agentsResult = null;

                searchText = searchText.Trim();
                if (HasDigits(searchText))
                {
                    if (HasNotAllowedChars(searchText))
                    {
                        // Уточнить поиск
                        return null;
                    }
                    else
                    {
                        // Ищем сертификат по числовой подстроке
                        agentsResult = db.Agents.Where(a =>
                            a.CertificateNumber.Contains(searchText));
                    }
                }
                else
                {
                    string[] words = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string w0, w1, w2;
                    if (words.Length == 1)
                    {
                        w0 = words[0].ToLower();
                        agentsResult = db.Agents.Include("InsuranceCompanyWorksIn").Where(a =>
                            a.LastName.ToLower() == w0);
                    }
                    else if (words.Length == 2)
                    {
                        w0 = words[0].ToLower();
                        w1 = words[1].ToLower();
                        agentsResult = db.Agents.Include("InsuranceCompanyWorksIn").Where(a =>
                            (a.FirstName.ToLower() == w0 && a.LastName.ToLower() == w1) ||
                            (a.FirstName.ToLower() == w1 && a.LastName.ToLower() == w0));
                    }
                    else if (words.Length == 3)
                    {
                        w0 = words[0].ToLower();
                        w1 = words[1].ToLower();
                        w2 = words[2].ToLower();
                        agentsResult = db.Agents.Include("InsuranceCompanyWorksIn").Where(a =>
                            (a.FirstName.ToLower() == w1 && a.MiddleName.ToLower() == w2 && a.LastName.ToLower() == w0) ||
                            (a.FirstName.ToLower() == w0 && a.MiddleName.ToLower() == w1 && a.LastName.ToLower() == w2));
                    }
                    else
                    {
                        // Уточнить поиск
                        return null;
                    }
                }
                
                return agentsResult
                    .OrderBy(a => a.LastName + " " + a.FirstName + " " + a.MiddleName)
                    .Take(ConfigurationHelper.MaxAgentsSearchResultSetLength + 1)
                    .ToList();
            }
        }


        private static bool HasDigits(string text)
        {
            foreach (char c in text)
            {
                if (c >= '0' && c <= '9')
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasNotAllowedChars(string text)
        {
            // TODO: 
            return false;
        }

    }
}