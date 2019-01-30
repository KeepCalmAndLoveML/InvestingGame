using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobilaApp.Models;
using MobilaApp.ViewModels;

namespace MobilaApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FacilityDetailsPage : ContentPage
	{
		FacilityDetailsViewModel model;

		public FacilityDetailsPage()
		{
			InitializeComponent();

			model = new FacilityDetailsViewModel();

			this.BindingContext = model;
		}

		public FacilityDetailsPage(Facility facility)
		{
			InitializeComponent();

			model = new FacilityDetailsViewModel(facility);

			InvestmentItems.ItemSelected += InvestmentSelected;
			this.BindingContext = model;
		}

		private async void InvestmentSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if(e.SelectedItem == null)
				return;

			var investment = (BriefInvestment)e.SelectedItem;
			await Navigation.PushAsync(new InvestmentPage(new InvestmentViewModel(new Investment(investment.BaseInvestment))));
		}
	}
}