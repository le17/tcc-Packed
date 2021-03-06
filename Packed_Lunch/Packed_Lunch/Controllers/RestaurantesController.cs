﻿using System;
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
    public class RestaurantesController : Controller
    {
        private Packed_Lunch_4_1Entities db = new Packed_Lunch_4_1Entities();

        // GET: Restaurantes
        public ActionResult Index()
        {
            var restaurante = db.Restaurantes;
            return View(restaurante.ToList());
        }

        // GET: Restaurantes/Details/5
        public ActionResult Details()
        {
            Restaurante restaurante = db.Restaurantes.Find(Session["IDRestaurante"]);

            if (Session["CNPJRestauranteLogado"] != null && restaurante != null)
            {
                return View(restaurante);
            }
            return HttpNotFound();
        }

        // GET: Restaurantes/Create
        public ActionResult Create()
        {
            //ViewBag.Id_horario_fk = new SelectList(db.Horario_limite, "Id_Horario", "Id_Horario");
            return View();
        }

        // POST: Restaurantes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cnpj,Nome,Endereco,Cidade,Telefone,Login,Senha,Id_horario_fk")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Restaurantes.Add(restaurante);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.Id_horario_fk = new SelectList(db.Horario_limite, "Id_Horario", "Id_Horario", restaurante.Id_horario_fk);
            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Restaurantes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Restaurante,Cnpj,Nome,Endereco,Cidade,Telefone,Login,Senha,Id_horario_fk")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_horario_fk = new SelectList(db.Horario_limite, "Id_Horario", "Id_Horario", restaurante.Id_horario_fk);
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurante restaurante = db.Restaurantes.Find(id);
            db.Restaurantes.Remove(restaurante);
            db.SaveChanges();
            return RedirectToAction("Create");
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
        public ActionResult Login(Restaurante u)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Packed_Lunch_4_1Entities()) // Nome do entity localizado no Empresa.Context
                {

                    //var login = from a in db.empresas select a;
                    var v = db.Restaurantes.Where(a => a.Login.Equals(u.Login) && a.Senha.Equals(u.Senha)).FirstOrDefault();
                    if (v != null)
                    {
                        
                        
                            TempData["Id_restaurante"] = v.Id_Restaurante;
                            Session["IDRestaurante"] = v.Id_Restaurante;
                            Session["CNPJRestauranteLogado"] = v.Cnpj.ToString();
                            Session["NomedaEmpresa"] = v.Nome.ToString();
                            return RedirectToAction("Details", "Restaurantes");
                        
                        
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
