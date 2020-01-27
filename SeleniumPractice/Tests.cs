using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumPractice
{
    public class Tests
    {
        private TrojmiastoWebPage page { get; set; }
        private CarSpecification carInfo { get; set; }

        [SetUp]
        public void Setup()
        {
            page = new TrojmiastoWebPage();
        }

        [Test]
        public void VerifyAverageCarDetailsByModelName()
        {
            page.SearchCarByModelName("Megane");

            var finalCarList = new List<CarSpecification>();
            int i = 1;

            while (true)
            {
                var capacityList = page.GetCarDetails(DetailsType.Capacity);
                var yearList = page.GetCarDetails(DetailsType.Year);
                var mileageList = page.GetCarDetails(DetailsType.Mileage);
                var priceList = page.GetCarPrice();

                for (int j = 0; j < capacityList.Count; j++)
                {
                    carInfo = new CarSpecification
                    {
                        capacity = capacityList[j],
                        year = yearList[j],
                        mileage = mileageList[j],
                        price = priceList[j]
                    };

                    finalCarList.Add(carInfo);
                }
                try
                {
                    page.MoveToNextPage(++i);

                }
                catch (Exception)
                {
                    break;
                }
            }

            double capacityAverage = finalCarList.Where(x => x.capacity != 0).Average(x => x.capacity);
            double yearAverage = Math.Round(finalCarList.Where(x => x.year != 0).Average(x => x.year));
            double mileageAverage = Math.Round(finalCarList.Where(x => x.mileage != 0).Average(x => x.mileage));
            double costAverage = Math.Round(finalCarList.Where(x => x.price != 0).Average(x => x.price));

            Console.WriteLine("\nAverage year : " + yearAverage + " from the " + i + " pages\n" +
                                  "Average mileage : " + mileageAverage + " from the " + i + " pages \n" +
                                  "Average capacity : " + capacityAverage + " from the " + i + " pages" +
                                  "Average capacity : " + costAverage + " from the " + i + " pages");

            Console.WriteLine(finalCarList.Capacity);
        }     
    }
}