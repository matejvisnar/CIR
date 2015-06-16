using DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIR_skladisce.Models
{
    public class IzdelekController : Controller
    {
        private DeloSPodatki db;

        public IzdelekController()
        {
            db = new DeloSPodatki();
        }

        //
        // GET: /Izdelek/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Izdelek/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Izdelek/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Izdelek/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Izdelek/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Izdelek/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Izdelek/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Izdelek/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
