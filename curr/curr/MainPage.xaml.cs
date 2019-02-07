using curr.Controllers;
using curr.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace curr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>


    public sealed partial class MainPage : Page
    {
        private WebsiteReader websiteReader;
        private ObservableCollection<Rate> PopularCurrencies;

        public MainPage()
        {
            this.InitializeComponent();
            websiteReader = new WebsiteReader();
            prepareBeginningWindow();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WebsiteReader.SelectedDate = (Date)e.AddedItems[0];
            PopularCurrencies = WebsiteReader.GetCurrencries(WebsiteReader.SelectedDate);
            currency_grid.ItemsSource = PopularCurrencies;
        }

        private void prepareBeginningWindow()
        {
            var today = new Date
            {
                Day = DateTime.Today.Day,
                Month = DateTime.Today.Month,
                Year = DateTime.Today.Year
            };
            try
            {
                PopularCurrencies = WebsiteReader.GetCurrencries(today);
            }
            catch (Exception e)
            {
                Helper.Message(e.Message);
            }
            List<DateTime> dateTimeList = WebsiteReader.DownloadAllCurrenciesWithDates();
            var datetime = dateTimeList.Select(x => new Date
            {
                Day = x.Day,
                Month = x.Month,
                Year = x.Year
            }).ToList();
            date_list_view.ItemsSource = datetime;

        }
        
    }
}
