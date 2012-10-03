using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class AgentInfoController : Controller
    {
        private AgentInfoContext db = new AgentInfoContext();

        //
        // GET: /AgentInfo/

        public ActionResult Index()
        {
            return View(db.Agents.ToList());
        }

        //
        // GET: /AgentInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            AgentInfo agentinfo = db.Agents.Find(id);
            if (agentinfo == null)
            {
                return HttpNotFound();
            }
            return View(agentinfo);
        }

        //
        // GET: /AgentInfo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AgentInfo/Create

        [HttpPost]
        public ActionResult Create(AgentInfo agentinfo)
        {
            if (ModelState.IsValid)
            {
                db.Agents.Add(agentinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agentinfo);
        }

        //
        // GET: /AgentInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AgentInfo agentinfo = db.Agents.Find(id);
            if (agentinfo == null)
            {
                return HttpNotFound();
            }
            return View(agentinfo);
        }

        //
        // POST: /AgentInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(AgentInfo agentinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentinfo);
        }

        //
        // GET: /AgentInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AgentInfo agentinfo = db.Agents.Find(id);
            if (agentinfo == null)
            {
                return HttpNotFound();
            }
            return View(agentinfo);
        }

        //
        // POST: /AgentInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AgentInfo agentinfo = db.Agents.Find(id);
            db.Agents.Remove(agentinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}