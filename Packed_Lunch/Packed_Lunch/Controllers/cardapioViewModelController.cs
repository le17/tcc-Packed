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
    public class cardapioViewModelController : Controller
    {
        // GET: cardapioViewModel
        public ActionResult Index()
        {
            Packed_Lunch_4_1Entities db = new Packed_Lunch_4_1Entities();
            List<cardapioViewModel> Cardapio = new List<cardapioViewModel>();

            var CardapioRestaurante = (from car in db.Cardapios
                                       join com in db.Compoems on car.Id_Cardapio equals com.Id_Cardapio_fk
                                       join prod in db.Produtoes on com.Id_Produto_fk equals prod.Id_Produto
                                       join rest in db.Restaurantes on car.Id_Restaurante_fk equals Session["IDRestaurante"]
                                       select new
                                 {car.Data_ini,car.Data_Fim,car.Restaurante,prod.Nome,prod.Descricao,prod.Valor}).ToList();
            foreach (var item in CardapioRestaurante)
            {
                cardapioViewModel cVM = new cardapioViewModel();
                cVM.Data_ini = item.Data_ini;
                cVM.Data_Fim = item.Data_Fim;
                cVM.Nome = item.Nome;
                cVM.Descricao = item.Descricao;
                cVM.Valor = item.Valor;
                Cardapio.Add(cVM);
            }
            return View(Cardapio);
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
        public ActionResult Create(cardapioViewModel cardapioViewModel,Restaurante restaurante)
        {
            Cardapio car = new Cardapio();
            Compoem com = new Compoem();
            Produto prod = new Produto();

            var id_logado = TempData["Id_restaurante"];
            cardapioViewModel.Id_Restaurante_fk = Convert.ToInt32(id_logado);
            //Cadastro itens tbl.cardapio;
            if (ModelState.IsValid)
            {
                using (Packed_Lunch_4_1Entities db = new Packed_Lunch_4_1Entities())
                {                  
                    car.Data_ini = cardapioViewModel.Data_ini;
                    car.Data_Fim = cardapioViewModel.Data_Fim;
                    car.Id_Restaurante_fk =Convert.ToInt32 (id_logado);
                    db.Cardapios.Add(car);
                    db.SaveChanges();

                    //verificação para saber se cardapio foi adicionado;
                    var carda = db.Cardapios.Where(a => a.Data_ini.Equals(car.Data_ini)).FirstOrDefault();
                    if (carda.Id_Cardapio.ToString() != null)
                    {
                        prod.Nome = cardapioViewModel.Nome;
                        prod.Descricao = cardapioViewModel.Descricao;
                        prod.Valor = cardapioViewModel.Valor;
                        db.Produtoes.Add(prod);
                        db.SaveChanges();
                    }
                    var produt = from p in db.Produtoes select p;
                    if(produt!= null)
                    {
                        com.Id_Cardapio_fk = car.Id_Cardapio;
                        com.Id_Produto_fk = prod.Id_Produto;
                        db.Compoems.Add(com);
                        db.SaveChanges();
                        return RedirectToAction("Details", "Restaurantes");
                    }


                }

            }

            return View();
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}

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
