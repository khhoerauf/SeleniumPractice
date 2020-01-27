using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SeleniumPractice
{
    public enum DetailsType
    {
        Year = 3,
        Mileage = 5,
        Capacity = 7
    }

    class TrojmiastoWebPage
    {
        IWebDriver _driver = new ChromeDriver(@"C:\Temp");
        public TrojmiastoWebPage()
        {
            var url = "https://ogloszenia.trojmiasto.pl/motoryzacja-sprzedam/";
            _driver.Url = (url);
            _driver.Manage().Window.Maximize();
        }

        public void SearchCarByModelName(string carModelName)
        {
            _driver.FindElement(By.CssSelector
               ("input.oglSearchbar__input.input.input--has--clear.ui-autocomplete-input")).SendKeys(carModelName);
            _driver.FindElement(By.CssSelector("button.oglSearchbar__btn.btn.btn--orange")).Click();
        }

        public List<int> GetCarDetails(DetailsType detail)
        {
            var detailsList = new List<int>();
            var listOfElement = _driver.FindElements(By.CssSelector("div.list__item__content"));

            foreach (var car in listOfElement)
            {
                var text = car.Text;
                string[] carDetails = text.Replace("\r", string.Empty).Replace("cm3", string.Empty).Split('\n');

                try
                {
                    detailsList.Add(GetNumericCarDetails(carDetails[(int)detail]));
                }
                catch (Exception)
                {
                    detailsList.Add(0);
                }
            }

            return detailsList;
        }

        public List<int> GetCarPrice()
        {
            var priceList = new List<int>();
            var prices = _driver.FindElements(By.CssSelector("p.list__item__price__value"));

            foreach (var price in prices)
            {
                var text = price.Text;
                int cost = int.Parse(Regex.Replace(text, "\\D", string.Empty));

                priceList.Add(cost);
            }

            return priceList;
        }

        public void MoveToNextPage(int page)
        {
            _driver.FindElement(By.XPath("//*[@href='?strona=" + page + "']")).Click();
        }

        private int GetNumericCarDetails(string input)
        {
            var detailValue = Regex.Replace(input, "\\D", string.Empty);

            return int.Parse(detailValue);
        }
    }
}
