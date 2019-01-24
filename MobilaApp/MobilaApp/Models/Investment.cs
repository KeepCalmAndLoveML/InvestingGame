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
	
		public Investment(GameInvestment investment)
		{
			Steps = new List<StepDescription>();
			int index = 1;
			foreach(var item in investment.History)
			{
				Steps.Add(new StepDescription(index, item));

				index++;
			}
		}

		public struct StepDescription
		{
			public readonly int Index;
			public readonly double InitialSum;
			public readonly GameFacility Facility;
			//public readonly string Result;

			public string Decision => Facility.Name;

			public StepDescription(int idx, GameInvestment investment)
			{
				Index = idx;
				InitialSum = investment.TotalMoney;
				Facility = investment.Facility;
			}
		}
	}
}
