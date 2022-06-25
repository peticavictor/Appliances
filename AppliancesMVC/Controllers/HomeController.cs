using AppliancesMVC.Data;
using AppliancesMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Appliances.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.LoggedUser1 = Request.Cookies["LoggedUser1"];
            var user = _context.User.FirstOrDefault(x => x.Name == Request.Cookies["LoggedUser1"]);

            if (user == null)
            {
                ViewBag.NrProduse = 0;
            }
            else
            {
                var cos = _context.Cart.FirstOrDefault(x => x.IsPayed == 0 && x.UserId == user.Id);
                if (cos != null)
                    ViewBag.NrProduse = _context.CartAppliance.Where(x => x.CartId == cos.Id).Count();
                else
                    ViewBag.NrProduse = 0;
            }

            return View(_context.Appliance.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Autorizare(User user)
        {
            var md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(user.Password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            user.Password = Convert.ToBase64String(hashBytes);

            var existingUser = _context.User.FirstOrDefault(x => x.Name == user.Name && x.Password == user.Password);

            if (existingUser != null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(30);

                Response.Cookies.Append("LoggedUser1", user.Name, option);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ConnectionError = "Wrong Login or Password";
            return View("Login");
        }
        public IActionResult Logout(User user)
        {
            Response.Cookies.Delete("LoggedUser1");

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AddToCartAsync(int id)
        {
            var user = _context.User.FirstOrDefault(x => x.Name == Request.Cookies["LoggedUser1"]);
            var cart = new Cart();
            var cartAppliances = new CartAppliance();
            var appliance = _context.Appliance.FirstOrDefault(item => item.Id == id);

            if (user == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                cart = _context.Cart.FirstOrDefault(e => e.IsPayed == 0 && e.UserId == user.Id);
            }

            if (cart == null)
            {
                cart = new Cart();
                cart.IsPayed = 0;
                cart.UserId = user.Id;
                cart.CreatedOn = DateTime.Now;

                _context.Add(cart);
                await _context.SaveChangesAsync();
            }

            cartAppliances = _context.CartAppliance.FirstOrDefault(item => item.ApplianceId == id  && item.Cart.UserId == user.Id && item.CartId == cart.Id);
            if (cartAppliances == null)
            {
                cartAppliances = new CartAppliance();
                cartAppliances.Quantity = 1;
                cartAppliances.ApplianceId = id;
                cartAppliances.CartId = cart.Id;
                cartAppliances.TotalPrice = (int)appliance.Price;
                _context.Add(cartAppliances);
            }
            else
            {
                cartAppliances.Quantity += 1;
                cartAppliances.TotalPrice += (int)appliance.Price;
                _context.Update(cartAppliances);
            }

            //var cartAppliances = new CartAppliance
            //{
            //    Quantity = 1,
            //    ApplianceId = id,
            //    CartId = cart.Id,
            //    //TotalPrice = (int)appliance.Price
            //};

            //_context.Add(cartAppliances);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        } 
        
    }
}
