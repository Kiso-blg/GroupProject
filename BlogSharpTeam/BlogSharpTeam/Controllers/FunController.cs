using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSharpTeam.Controllers
{
    public class FunController : Controller
    {
        // GET: Fun
        static int rowsCount = 3;
        static int colsCount = 9;
        static string[,] fruits = GenerateRandomFruits();
        static int score = 0;
        static int dynamiteCount = CountDynamites();
        static bool gameOver = false;
        static bool victory = false;

        static string[,] GenerateRandomFruits()
        {
            var rand = new Random();
            fruits = new string[rowsCount, colsCount];

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    var r = rand.Next(9);

                    if (r < 2)
                    {
                        fruits[row, col] = "apple";
                    }
                    else if (r < 4)
                    {
                        fruits[row, col] = "banana";
                    }
                    else if (r < 6)
                    {
                        fruits[row, col] = "orange";
                    }
                    else if (r < 8)
                    {
                        fruits[row, col] = "kiwi";
                    }
                    else
                    {
                        fruits[row, col] = "dynamite";
                    }
                }
            }

            return fruits;
        }

        static int CountDynamites()
        {
            int count = 0;

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    if (fruits[row, col] == "dynamite")
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public ActionResult Reset()
        {
            fruits = GenerateRandomFruits();
            gameOver = false;
            victory = false;
            score = 0;
            dynamiteCount = CountDynamites();
            return RedirectToAction("FunIndex");
        }

        public ActionResult FireTop(int position)
        {
            return Fire(position, 0, 1);
        }

        public ActionResult FireBottom(int position)
        {
            return Fire(position, rowsCount - 1, -1);
        }

        private ActionResult Fire(int position, int startRow, int step)
        {
            var col = position * (colsCount - 1) / 100;
            var row = startRow;

            while (row >= 0 && row < rowsCount)
            {
                var fruit = fruits[row, col];

                if (fruit == "apple" || fruit == "banana" || fruit == "orange" || fruit == "kiwi")
                {
                    score++;
                    fruits[row, col] = "empty";

                    if (score + dynamiteCount == 27)
                    {
                        victory = true;
                    }

                    break;
                }
                else if (fruit == "dynamite")
                {
                    gameOver = true;
                    break;
                }

                row = row + step;
            }

            return RedirectToAction("FunIndex");
        }

        public ActionResult FunIndex()
        {
            ViewBag.rowsCount = rowsCount;
            ViewBag.colsCount = colsCount;
            ViewBag.fruits = fruits;
            ViewBag.score = score;
            ViewBag.dynamiteCount = dynamiteCount;
            ViewBag.victory = victory;
            ViewBag.gameOver = gameOver;
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}