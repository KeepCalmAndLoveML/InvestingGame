using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobilaApp.Models;
using MobilaApp.Views;
using MobilaApp.ViewModels;

namespace MobilaApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FacilitiesPage : ContentPage
	{
		private List<Facility> Facilities;
		BaseViewModel viewModel;
		
		public FacilitiesPage()
		{
			InitializeComponent();

			viewModel = new BaseViewModel();
			
			Items.ItemSelected += OnItemSelected;
		}

		async void LoadItemsAsync()
		{
			try
			{
				bool flag = await viewModel.DataStore.LoadItemsAsync("data.xml");

				if(flag)
				{
					Facilities = new List<Facility>();
					foreach(var facility in viewModel.DataStore.items)
					{
						Facilities.Add(FromGameLogic(facility));
					}
				}
				else
				{
					Facilities = new List<Facility>();
					Facilities.Add(new Facility
					{
						Name = "Перевозки",
						Description = "Инвестиции в малый бизнес",

						SampleKey = "Сумма денег",
						SampleValue = "Доходность",

						Key2 = "10 млн",
						Value2 = "+100%",

						Key3 = "21 млн",
						Value3 = "+50%",

						Key4 = "35 млн",
						Value4 = "+10%",

						BackgroundColor = Color.DarkGreen,
					});
				}	
			}
			catch (Exception e)
			{
				DisplayAlert("Exception!", e.Message, "OK");

				Facilities = new List<Facility>();
				Facilities.Add(new Facility
				{
					Name = "Перевозки",
					Description = "Инвестиции в малый бизнес",

					SampleKey = "Сумма денег",
					SampleValue = "Доходность",

					Key2 = "10 млн",
					Value2 = "+100%",

					Key3 = "21 млн",
					Value3 = "+50%",

					Key4 = "35 млн",
					Value4 = "+10%",

					BackgroundColor = Color.DarkGreen,
				});
			}
			finally
			{
				Items.ItemsSource = Facilities;
			}
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			//Manually Deselect Item
			Facility facility = args.SelectedItem as Facility;
			if(facility != null)
			{
				Items.SelectedItem = null;

				await Navigation.PushAsync(new FacilityDetailsPage(facility));
			}
		}

		protected override void OnAppearing()
		{
			LoadItemsAsync();

			base.OnAppearing();	
		}

		public static Facility FromGameLogic(InvestingGame.GameLogic.Facility facility)
		{
			Facility res = new Facility
			{
				BackgroundColor = Color.DarkGreen,
				Name = facility.Name,
				Description = facility.Description,
				BaseFacility = facility
			};

			res.SampleKey = "Money invested";
			res.SampleValue = facility.FacilityReturnType == InvestingGame.GameLogic.Facility.ReturnItemType.Additive ? "Addition" : "Multiplier";

			List<double> keys = facility.Boundaries.Keys.ToList();

			Type t = typeof(Facility);
			for(int i = 0; i < keys.Count; i++)
			{
				var key = t.GetProperty($"Key{i + 2}");
				key.SetValue(res, keys[i].ToString());

				var value = t.GetProperty($"Value{i + 2}");
				value.SetValue(res, facility.Boundaries[keys[i]].ToString());
			}

			return res;
		}
	}
}