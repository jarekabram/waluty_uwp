using curr.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace curr.Controllers
{
    class WebsiteReader
    {
        static ObservableCollection<Rate> currencyCollection = new ObservableCollection<Rate>();
        public static Date SelectedDate;
        public static List<Rate> DownloadAllCurrencies(Date date)
        {
            List<PopularCurrency> deserializedCurrencies;
            List<Rate> rates = new List<Rate>();
            string url = "http://api.nbp.pl/api/exchangerates/tables/a/" + date.Year + "-" + lessThanTen(date.Month) + "-" + lessThanTen(date.Day) + "/?format=json";
            //string url = "http://api.nbp.pl/api/exchangerates/tables/a/?format=json";


            using (HttpClient httpClient = new HttpClient())
            using (HttpResponseMessage response = httpClient.GetAsync(url).Result)
            using (HttpContent content = response.Content)
            {
                string stringContent = content.ReadAsStringAsync().Result;
                if (stringContent.Contains("404 NotFound - Not Found - Brak danych"))
                {
                    throw new Exception("Try again");
                }
                deserializedCurrencies = JsonConvert.DeserializeObject<List<PopularCurrency>>(stringContent);

                foreach (var objects in deserializedCurrencies)
                    foreach (var obj in objects.Rates)
                        rates.Add(obj);
                    
            }
            return rates;
        }

        public static List<DateTime> DownloadAllCurrenciesWithDates()
        {
            List<PopularCurrency> deserializedCurrencies;
            List<DateTime> dates = new List<DateTime>();

            string url = "http://api.nbp.pl/api/exchangerates/tables/a/last/14/?format=json";
            using (HttpClient httpClient = new HttpClient())
            using (HttpResponseMessage response = httpClient.GetAsync(url).Result)
            using (HttpContent content = response.Content)
            {
                string stringContent = content.ReadAsStringAsync().Result;
                if (stringContent == null)
                {
                    throw new TaskCanceledException("Null Content");
                }
                deserializedCurrencies = JsonConvert.DeserializeObject<List<PopularCurrency>>(stringContent);
            }
            foreach (var elem in deserializedCurrencies)
                dates.Add(elem.EffectiveDate);

            return dates;
        }

        public static ObservableCollection<Rate> GetCurrencries(Date date)
        {
            currencyCollection.Clear();
            foreach (var item in DownloadAllCurrencies(date))
            {
                currencyCollection.Add(item);
            }

            return currencyCollection;
        }
        private static string lessThanTen(int number)
        {
            if (number < 10)
            {
                return "0" + number;
            }
            return number.ToString();
        }
        private static void printCollection(PopularCurrency popularCurrency)
        {
        }

    }
}
