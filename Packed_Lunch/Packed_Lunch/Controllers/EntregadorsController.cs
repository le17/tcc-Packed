using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Packed_Lunch.Models;

namespace Packed_Lunch.Controllers
{
    public class EntregadorsController : Controller
    {
        private Packed_Lunch_4_1Entities db = new Packed_Lunch_4_1Entities();

        // GET: Entregadors
        public ActionResult Index()
        {
            return View(db.Entregadors.ToList());
        }

        // GET: Entregadors/Details/5
        public ActionResult Details()
        {
            Entregador entregador = db.Entregadors.Find(Session["IDUsuario"]);

            if (Session["CPFUsuarioLogado"] != null && entregador != null)
            {
                return View(entregador);
            }
            return HttpNotFound();
        }

        // GET: Entregadors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entregadors/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Entregador,Cpf,Nome,Login,Senha")] Entregador entregador)
        {
            if (ModelState.IsValid)
            {
                db.Entregadors.Add(entregador);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(entregador);
        }

        // GET: Entregadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entregador entregador = db.Entregadors.Find(id);
            if (entregador == null)
            {
                return HttpNotFound();
            }
            return View(entregador);
        }

        // POST: Entregadors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Entregador,Cpf,Nome,Login,Senha")] Entregador entregador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entregador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(entregador);
        }

        // GET: Entregadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entregador entregador = db.Entregadors.Find(id);
            if (entregador == null)
            {
                return HttpNotFound();
            }
            return View(entregador);
        }

        // POST: Entregadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entregador entregador = db.Entregadors.Find(id);
            db.Entregadors.Remove(entregador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Entregador u)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Packed_Lunch_4_1Entities()) // Nome do entity localizado no Empresa.Context
                {

                    //var login = from a in db.empresas select a;
                    var v = db.Entregadors.Where(a => a.Login.Equals(u.Login) && a.Senha.Equals(u.Senha)).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.Equals("Entregador"))
                        {
                            Session["IDUsuario"] = v.Id_Entregador;
                            Session["CPFUsuarioLogado"] = v.Cpf.ToString();
                            Session["NomedaEmpresa"] = v.Nome.ToString();
                            return RedirectToAction("Details", "Entregadors");
                        }
                        ////if (v.func.Equals("func"))
                        //{
                        //    Session["nomeUsuarioLogado"] = v.login.ToString();
                        //    return RedirectToAction("funcionario", "Usuario");
                        //}
                    }
                }

            }

            return View(u);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
