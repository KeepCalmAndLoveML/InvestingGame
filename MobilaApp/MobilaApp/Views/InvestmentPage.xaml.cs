using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobilaApp.ViewModels;

namespace MobilaApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InvestmentPage : ContentPage
	{
		public InvestmentPage()
		{
			InitializeComponent();
		}
		
		public InvestmentPage(InvestmentViewModel viewModel)
		{
			InitializeComponent();

			this.BindingContext = viewModel;
			//Although this is set in Xaml, this code is still required for the thing to work
			//TODO: Fix this bug
			StepsView.ItemsSource = viewModel.Investment.Steps;			
		}
	}
}