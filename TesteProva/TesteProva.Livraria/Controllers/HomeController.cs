using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Web.Mvc;
using TesteProva.Livraria.Models;
using System.Net;

namespace TesteProva.Livraria.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();//Connection
        public ActionResult Index()
        {
            return View();
        }
        //Here list all book
        //For this method get all books
        //Get - To List view
        public ActionResult ListarLivros()
        {
            var listar = db.Livros.Include(x => x.Categorias).OrderBy(x=>x.Titulo);
            return View(listar.ToList());
        }

        [HttpPost]
        public ActionResult ListaLivros(Livros livros)
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

        [Authorize(Roles ="ADMIN")]
        public ActionResult Registro()
        {

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(Livros livros,string Titulo, int ExemplarNumero)
        {

            if (ModelState.IsValid)
            {
                var TSearch = db.Livros.Where(x => x.Titulo.Contains(Titulo)).FirstOrDefault();
                var exl = db.Livros.Where(x => x.ExemplarNumero == ExemplarNumero).FirstOrDefault();
                if (TSearch == null)
                {
                    if (exl == null)
                    {
                        livros.Data = DateTime.Now;
                        db.Livros.Add(livros);
                        db.SaveChanges();
                        TempData["Msg"] = "Livro registrado com sucesso!";
                        return RedirectToAction("ListaRegistrada", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "Este numero de Exemplar já existe! Tente registrar outro";
                    }
                }
                else
                {
                    TempData["Error"] = "Você não pode registrar um livro de mesmo titulo!";
                }
                

                
            }
            else
            {
                TempData["Error"] = "Erro no regisrto!";
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult ListaRegistrada()
        {

            var resultado =  db.Livros.Include(x => x.Categorias).ToList();
            return View(resultado.OrderBy(x=>x.Titulo));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListaRegistrada(string search)
        {
           
           
            if (search != "")
            {
                var resultado = db.Livros.Where(x => x.Titulo.Contains(search)).ToList();
                return View(resultado);
            }
            else
            {
                return View(db.Livros.ToList());
            }
           
        }


        //Lista os livros para edição
        [Authorize(Roles = "ADMIN")]
        public ActionResult EditarLivro(int? id)
        {
            Livros livros = db.Livros.Find(id);
            if (livros == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            return View(livros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public ActionResult EditarLivro(Livros livros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livros).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Msg"] = "Livro atualizado com successo";

                return RedirectToAction("ListaRegistrada", "Home");
            }
            return View(livros);
        }
        [Authorize(Roles = "ADMIN")]
        public ActionResult ExcluirLivro(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livros registro = db.Livros.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            return View(registro);
        }
       
        [ValidateAntiForgeryToken]
        [HttpPost,ActionName("ExcluirLivro")]
        public ActionResult ExcluirLivroRegistrado(int id)
        {
            Livros registros = db.Livros.Find(id);
            db.Livros.Remove(registros);
            db.SaveChanges();
            TempData["Msg"] = "Livro excludo com sucesso!";
            return RedirectToAction("ListaRegistrada", "Home");
           
        }

        [Authorize(Roles = "ADMIN")]
        public  ActionResult AreaAdmin()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}