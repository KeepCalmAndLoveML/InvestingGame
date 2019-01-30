using System;
using System.Collections.Generic;
using System.Text;

using GameInvestment = InvestingGame.GameLogic.Player.Investment;
using GameFacility = InvestingGame.GameLogic.Facility;

namespace MobilaApp.Models
{
	public class BriefInvestment
	{
		public GameInvestment BaseInvestment;

		public string Name { get; set; }
		public string OwnerName { get; set; }
		public string BriefDescription => $"{OwnerName} has {TotalMoney}$ invested in {BaseInvestment.Facility.Name}";

		public double TotalMoney { get; set; }
	}

	
	public class Investment
	{
		public List<StepDescription> Steps;
		public string OwnerName { get; set; }

		public Investment(GameInvestment investment)
		{
			OwnerName = investment.Owner.Name;
			//TODO: Maybe move this in the ViewModel?
			Steps = new List<StepDescription>();
			int index = 1;
			foreach(var item in investment.History)
			{
				Steps.Add(new StepDescription(index, item));

				index++;
			}

			Steps.Add(new StepDescription(index, investment));
		}

		//TODO: consider this
		//Maybe make this struct the Model
		//And put the list in the view model
		public struct StepDescription
		{
			public readonly string Index;
			public readonly double InitialSum;
			public readonly GameFacility Facility;
			public readonly string Result;
			
			public string Decision => Facility.Name;

			public StepDescription(int idx, GameInvestment investment)
			{
				Index = idx.ToString();
				InitialSum = investment.TotalMoney;
				Facility = investment.Facility;
				Result = investment.ReturnItem.HasValue ? investment.ReturnItem.Value.ReturnValue.ToString() : "";
			}
		}
	}
}
