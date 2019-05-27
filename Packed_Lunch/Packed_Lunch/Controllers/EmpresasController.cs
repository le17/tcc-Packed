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
    public class EmpresasController : Controller
    {
        public char[] Cnpj;
        public int Id_empresa;

        private Packed_Lunch_4_1Entities db = new Packed_Lunch_4_1Entities();
        private object cnpj;

        public EmpresasController(object cnpj)
        {
            this.cnpj = cnpj;
        }

        public EmpresasController()
        {
        }

        // GET: Empresas
        public ActionResult Index()
        {
            return View(db.Empresas.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details()
        {
            Empresa empresa = db.Empresas.Find(Session["IDUsuario"]);
            
            if (Session["CNPJUsuarioLogado"] != null && empresa != null)
            {
                return View(empresa);
            }
            return HttpNotFound();
        }
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Empresa empresa = db.Empresas.Find(id);
            //if (empresa == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(empresa);
        

        // GET: Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cnpj,Nome,Endereco,Cidade,Telefone,Login,Senha")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Empresas.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Empresa,Cnpj,Nome,Endereco,Cidade,Telefone,Login,Senha")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Empresas.Find(id);
            db.Empresas.Remove(empresa);
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
        public ActionResult Login(Empresa u,object sender,EventArgs e)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Packed_Lunch_4_1Entities()) // Nome do entity localizado no Empresa.Context
                {
                    //var login = from a in db.empresas select a;
                    var v = db.Empresas.Where(a => a.Login.Equals(u.Login) && a.Senha.Equals(u.Senha)).FirstOrDefault();
                                    
                    //Id_empresa = v.Id_Empresa;
                    Cnpj = v.Cnpj.ToCharArray();
                    TempData["Id_empresa"] = v.Id_Empresa;
                    TempData["Id_empresa_log"] = v.Id_Empresa;
                    if (v != null)
                    {
                            Session["IDUsuario"] = v.Id_Empresa;
                            Session["CNPJUsuarioLogado"] = v.Cnpj.ToString();
                            Session["NomedaEmpresa"] = v.Nome.ToString();
                            

                            return RedirectToAction("Details","Empresas");
                        
                        
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
