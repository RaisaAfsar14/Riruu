using freshMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace freshMart.Controllers
{
    
    public class HomeController : Controller
    {

        FreshMartEntities1 db = new FreshMartEntities1();
        

        public ActionResult HomeIndex()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authenticate");
            }
            else
            {
                return View(db.tbl_product.ToList());

            }



        }
        public ActionResult AddtoCart(int productId, string url)
        {
            if (Session["cart"] == null)
            {
                List<Models.Home.Item> cart = new List<Models.Home.Item>();
                var product = db.tbl_product.Find(productId);
                cart.Add(new Models.Home.Item()
                {
                    product = product,
                    Quiantity = 1
                }) ;
                Session["cart"] = cart;
            }
            else
            {
                List<Models.Home.Item> cart = (List<Models.Home.Item>)Session["cart"];
                var count = cart.Count();
                var product = db.tbl_product.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].product.product_id == productId)
                    {
                        int prevQ = cart[i].Quiantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Models.Home.Item()
                        {
                            product = product,
                            Quiantity = prevQ + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.product.product_id == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Models.Home.Item()
                            {
                                product = product,
                                Quiantity = 1
                            });
                        }

                    }
                }

                Session["cart"] = cart;
            }
            return Redirect(url);
        }
        public ActionResult RemoveFromCart(int productId)
        {

            List<Models.Home.Item> cart = (List<Models.Home.Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.product.product_id == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;

            return Redirect("HomeIndex");
        }

        public ActionResult Checkout()
        {

            return View();
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Models.Home.Item> cart = (List<Models.Home.Item>)Session["cart"];
                var product = db.tbl_product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.product.product_id == productId)
                    {
                        int prevQty = item.Quiantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Models.Home.Item()
                            {
                                product = product,
                                Quiantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}