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

			this.BindingContext = model;
		}
	}
}