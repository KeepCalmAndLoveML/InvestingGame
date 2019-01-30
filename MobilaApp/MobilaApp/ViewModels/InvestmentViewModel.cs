using System;
using System.Collections.Generic;
using System.Text;

using MobilaApp.Models;

namespace MobilaApp.ViewModels
{
    public class InvestmentViewModel
    {
		public readonly Investment Investment;
		public string Title { get; private set; }

		public InvestmentViewModel()
		{

		}

		public InvestmentViewModel(Investment investment)
		{
			Investment = investment;
			Title = investment.OwnerName + "'s investment";
		}
    }
}
