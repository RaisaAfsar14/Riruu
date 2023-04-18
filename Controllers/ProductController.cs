using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using freshMart.Models;
using System.Data;
using System.IO;
using System.Data.Entity;


namespace freshMart.Controllers
{
    public class ProductController : Controller
    {
        FreshMartEntities1 db = new FreshMartEntities1();
         //private tbl_product tbPro;
        // GET: Product
        public ActionResult Index()
        {
            FreshMartEntities1 dbmodel = new FreshMartEntities1();
            return View(dbmodel.tbl_product.ToList());
        }

       

        // GET: Product/Details/5

        public ActionResult Details(int id)
        {
            using (FreshMartEntities1 db = new FreshMartEntities1())
            {
                return View(db.tbl_product.Where(x=> x.product_id == id).FirstOrDefault());

            }
            
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            tbl_product pro = new tbl_product();
            return View(pro);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(tbl_product pro)
        {
            try
            {
                

                string fileName = Path.GetFileNameWithoutExtension(pro.ImageFile.FileName);
                string extension = Path.GetExtension(pro.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                pro.product_image = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                pro.ImageFile.SaveAs(fileName);
                using (FreshMartEntities1 db = new FreshMartEntities1())
                {
                    db.tbl_product.Add(pro);
                    db.SaveChanges();
                }

                ModelState.Clear();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        
        public ActionResult Edit(int id)
        {
            using (FreshMartEntities1 db = new FreshMartEntities1())
            {
                return View(db.tbl_product.Where(x => x.product_id == id).FirstOrDefault());

            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbl_product pro)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(pro.ImageFile.FileName);
                string extension = Path.GetExtension(pro.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                pro.product_image = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                pro.ImageFile.SaveAs(fileName);
                using (FreshMartEntities1 db = new FreshMartEntities1())
                {
                    db.Entry(pro).State = EntityState.Modified;
                    db.SaveChanges();

                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (FreshMartEntities1 db = new FreshMartEntities1())
            {
                return View(db.tbl_product.Where(x => x.product_id == id).FirstOrDefault());

            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using(FreshMartEntities1 db = new FreshMartEntities1())
                {
                    tbl_product pro = db.tbl_product.Where(x => x.product_id == id).FirstOrDefault();
                    db.tbl_product.Remove(pro);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
