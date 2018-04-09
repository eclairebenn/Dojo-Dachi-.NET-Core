using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace DojoDachi.Controllers
{
    public class DachiController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("fullness") == null)
            {
                HttpContext.Session.SetInt32("fullness", 20);  
                HttpContext.Session.SetInt32("happiness", 20);  
                HttpContext.Session.SetInt32("meals", 3);  
                HttpContext.Session.SetInt32("energy", 50);
                HttpContext.Session.SetString("img", "dragon");
            }
    
            int? fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.fullness = (int)fullness;
            int? happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.happiness = (int)happiness;
            int? meals = HttpContext.Session.GetInt32("meals");
            ViewBag.meals = (int)meals;
            int? energy = HttpContext.Session.GetInt32("energy");
            ViewBag.energy = (int)energy;
            string img = HttpContext.Session.GetString("img");
            ViewBag.img = img;

            if((int)energy >= 100 && (int)happiness >=100 && (int)fullness >= 100)
            {
                TempData["message"] = $"You won!";
                HttpContext.Session.SetString("img", "win");
                ViewBag.endgame = true;
            }

            else if((int)fullness <= 0 || (int)happiness <= 0)
            {
                TempData["message"] = $"You lost :(";
                HttpContext.Session.SetString("img", "loss");
                ViewBag.endgame = true;
            }

            else
            {
                ViewBag.endgame = false;
            }

            return View("index");
        }

        [HttpPost]
        [Route("feed")]
        public IActionResult Feed()
        {
            Random rand = new Random();
            int enjoy = rand.Next(1,5);
            int? meals = HttpContext.Session.GetInt32("meals");
            if((int)meals <= 0)
            {
                TempData["message"] = $"You don't have any meals to feed your DojoDachi!";
                HttpContext.Session.SetString("img", "mad");
                return RedirectToAction("Index");
            }

            int takemeal = (int)meals -1;
            HttpContext.Session.SetInt32("meals", takemeal);     

            if(enjoy == 4)
            {
                TempData["message"] = $"Your Dachi didn't like that... Meals -1";
                HttpContext.Session.SetString("img", "mad");
                return RedirectToAction("Index");               
            }

            int? fullness = HttpContext.Session.GetInt32("fullness");
            int addfull = rand.Next(5,11);
            int Newfullness = (int)fullness + addfull;
            HttpContext.Session.SetInt32("fullness", Newfullness);

            TempData["message"] = $"You have fed your DojoDachi! Fullness +{addfull}, Meals -1";
            HttpContext.Session.SetString("img", "food");

            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("play")]
        public IActionResult Play()
        {
            Random rand = new Random(); 
            int enjoy = rand.Next(1,5);           
            int? energy = HttpContext.Session.GetInt32("energy");
            if((int)energy <= 0)
            {
                TempData["message"] = $"Your DojoDachi doesn't have any energy!";
                HttpContext.Session.SetString("img", "mad");
                return RedirectToAction("Index");
            }

            int takeenergy = (int)energy -5;
            HttpContext.Session.SetInt32("energy", takeenergy);     

            if(enjoy == 4)
            {
                TempData["message"] = $"Your Dachi didn't like that... Energy -5";
                HttpContext.Session.SetString("img", "mad");
                return RedirectToAction("Index");               
            }

            int? happiness = HttpContext.Session.GetInt32("happiness");
            int addhapp = rand.Next(5,11);
            int Newhappiness = (int)happiness + addhapp;
            HttpContext.Session.SetInt32("happiness", Newhappiness);

            TempData["message"] = $"You have played with your DojoDachi! Happiness +{addhapp}, Energy -5";
            HttpContext.Session.SetString("img", "play");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("work")]
        public IActionResult Work()
        {
            Random rand = new Random();          
            int? energy = HttpContext.Session.GetInt32("energy");
            if((int)energy <= 0)
            {
                TempData["message"] = $"Your DojoDachi doesn't have any energy!";
                HttpContext.Session.SetString("img", "mad");
                return RedirectToAction("Index");
            }

            int takeenergy = (int)energy -5;
            HttpContext.Session.SetInt32("energy", takeenergy);

            int? meals = HttpContext.Session.GetInt32("meals");
            int addmeal = rand.Next(1,4);
            int Newmeals = (int)meals + addmeal;
            HttpContext.Session.SetInt32("meals", Newmeals);

            TempData["message"] = $"You have made your DojoDachi work! Meals +{addmeal}, Energy -5";
            HttpContext.Session.SetString("img", "work");

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            
            int? energy = HttpContext.Session.GetInt32("energy");
            int addenergy = (int)energy + 15;
            HttpContext.Session.SetInt32("energy", addenergy);

            int? fullness = HttpContext.Session.GetInt32("fullness");
            int Newfullness = (int)fullness - 5;
            HttpContext.Session.SetInt32("fullness", Newfullness);

            int? happiness = HttpContext.Session.GetInt32("happiness");
            int Newhappiness = (int)happiness - 5;
            HttpContext.Session.SetInt32("happiness", Newhappiness);

            TempData["message"] = $"Your DojoDach took a nap! Energy +15, Fullness -5, Happiness -5";
            HttpContext.Session.SetString("img", "sleep");


            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("reset")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }

    }
}