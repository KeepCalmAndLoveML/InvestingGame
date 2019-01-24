using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


using MobilaApp.Models;

namespace MobilaApp.ViewModels
{
    public class FacilityDetailsViewModel : BaseViewModel
    {
		public Facility BaseView { get; set; }
		public List<BriefInvestment> Investments { get; set; }
		
		public FacilityDetailsViewModel()
		{
			BaseView = new Facility();

			Title = "Details";
		}

		public FacilityDetailsViewModel(Facility facility)
		{
			BaseView = facility;

			Title = $"{BaseView.Name}'s Details";

			Investments = new List<BriefInvestment>();
			foreach(var investment in facility.BaseFacility.AllInvestments)
			{
				var other = Investments.FirstOrDefault(x => x.OwnerName == investment.Owner.Name);
				if(other != null)
					other.TotalMoney += investment.TotalMoney;
				else
				{
					Investments.Add(new BriefInvestment
					{
						Name = investment.Name,
						OwnerName = investment.Owner.Name,
						TotalMoney = investment.TotalMoney,
						BaseInvestment = investment
					});
				}
			}
		}
    }
}
