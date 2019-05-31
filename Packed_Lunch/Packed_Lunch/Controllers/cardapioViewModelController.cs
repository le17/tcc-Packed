using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Packed_Lunch.Controllers
{
    public class cardapioViewModelController : Controller
    {
        // GET: cardapioViewModel
        public ActionResult Index()
        {
            return View();
        }

        // GET: cardapioViewModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: cardapioViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cardapioViewModel/Create
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

        // GET: cardapioViewModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: cardapioViewModel/Edit/5
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

        // GET: cardapioViewModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: cardapioViewModel/Delete/5
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
