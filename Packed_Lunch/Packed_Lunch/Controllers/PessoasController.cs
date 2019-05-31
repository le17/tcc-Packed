using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Packed_Lunch.Models;


namespace Packed_Lunch.Controllers
{
    public class PessoasController : Controller
    {
        
        public int id_empresa_logada;
        private Packed_Lunch_4_1Entities db = new Packed_Lunch_4_1Entities();

        // GET: Pessoas
        public ActionResult Index()
        {
            var id_empresa_log = TempData["Id_empresa_log"];
            int log = Convert.ToInt32(id_empresa_log);
            if (id_empresa_log != null)
            {
                var func = from f in db.Pessoas
                           orderby f.Id_Pessoa
                           where f.Id_empresa_fk == log
                           select f;
                return View(func);
            }
            return HttpNotFound();
        }
//            if (ModelState.IsValid)
//            {
////                using (var db = new Packed_Lunch_4_1Entities()) // Nome do entity localizado no Empresa.Context
////                {
////                    var log = TempData["Id_empresa_log"];
////                    id_empresa_logada = Convert.ToInt32(log);
                    


////                    if (log == null)
////                    {
////                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
////                    }

////                    Pessoa funcionarios = db.Pessoas.Find(e.Id_Pessoa);

////                    if (e.Id_empresa_fk == u.Id_Empresa && u.Id_Empresa == id_empresa_logada )
////                    {
//////                        Pessoa funcionarios = db.Pessoas.Find(e.Id_Pessoa);


////                        return View(funcionarios);
////                    }
                     
//                    //if (pessoa.Id_empresa_fk = id_empresa_logada)
//                    //    return View(db.Pessoas.ToList().Where(u.Id_empresa_fk == id_empresa_logada));





//                }
//            }
            
      //  }



        // GET: Pessoas/Details/5
        public ActionResult Details()
        {
            Pessoa pessoa = db.Pessoas.Find(Session["IDUsuario"]);

            if (Session["CPFUsuarioLogado"] != null && pessoa != null)
            {
                return View(pessoa);
            }
            return HttpNotFound();
        }
            

        //    if (pessoa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pessoa);
        //}

        // GET: Pessoas/Create
        public ActionResult Create()
        {

            return View();
        }







        

        // POST: Pessoas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Pessoa,Cpf,Nome,Login,Senha,Id_empresa_fk")] Pessoa pessoa, EmpresasController empresa)
        {
            var id_logado = TempData["Id_empresa"];
            pessoa.Id_empresa_fk =Convert.ToInt32(id_logado);
            //id_empresa_logada = Convert.ToInt32(id_logado);
            if (ModelState.IsValid)
            {
                //pessoa.Id_empresa_fk = id_empresa;
                db.Pessoas.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_empresa_fk = new SelectList(db.Empresas, "Id_Empresa", "Cnpj", pessoa.Id_empresa_fk);
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Pessoa,Cpf,Nome,Login,Senha,Id_empresa_fk")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_empresa_fk = new SelectList(db.Empresas, "Id_Empresa", "Cnpj", pessoa.Id_empresa_fk);
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoa);
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
        public ActionResult Login(Pessoa u, object sender, EventArgs e)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Packed_Lunch_4_1Entities()) // Nome do entity localizado no Empresa.Context
                {
                    //var login = from a in db.empresas select a;
                    var v = db.Pessoas.Where(a => a.Login.Equals(u.Login) && a.Senha.Equals(u.Senha)).FirstOrDefault();

                    //Id_empresa = v.Id_Empresa;
                    
                    if (v != null)
                    {
                        Session["IDUsuario"] = v.Id_Pessoa;
                        Session["CPFUsuarioLogado"] = v.Cpf.ToString();
                        Session["NomedaEmpresa"] = v.Nome.ToString();


                        return RedirectToAction("Details", "Pessoas");


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
