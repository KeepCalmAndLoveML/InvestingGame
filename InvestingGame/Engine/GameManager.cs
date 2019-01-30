using System;
using System.Collections.Generic;
using System.Linq;

using InvestingGame.GameLogic;

namespace InvestingGame.Engine
{
    //Players are hard-coded
    //TODO: Add some user settings for players
    public static class GameManager
    {
		public static Facility DefaultFacility { get; private set; }

        public const int PlayerCount = 4;
        public const int InvestementsPerPlayer = 3;
        public const double PlayersInitialSum = 10;

		private static bool Initialized = false;

		private static Random RandomNumberGenerator = new Random();

        public static List<Facility> Facilities;
        public static List<Player> Players;

        public static string DataPath = "data.xml";

        //Starts the game
        public static void Initialize(string newPath = "")
        {
			if(Initialized)
				return;

            if(!string.IsNullOrEmpty(newPath))
                DataPath = newPath;

            GameLoader gameLoader = new GameLoader(DataPath);
            Facilities = gameLoader.GetFacilities();

			Initialize();

			Initialized = true;
        }

		public static Facility GetRandomFacility()
		{
			return Facilities[RandomNumberGenerator.Next(0, Facilities.Count)];
		}

		public static void Initialize(System.IO.Stream stream)
		{
			if(Initialized)
				return;

			GameLoader gameLoader = new GameLoader(stream);
			Facilities = gameLoader.GetFacilities();

			Initialize();

			Initialized = true;
		}

		private static void Initialize()
		{
			DefaultFacility = Facilities[0];

			Players = new List<Player>();
			for(int i = 1; i <= PlayerCount; i++)
			{
				string name = $"Player {i}";
				if(i == 1)
					name = "You";

				Players.Add(new Player($"Player {i}"));
				for(int j = 1; j <= InvestementsPerPlayer; j++)
				{
					Players[i - 1].Investments.Add(new Player.Investment(Players[i - 1], $"Investment {j}", PlayersInitialSum));

					Facility facility = GetRandomFacility();
					Communication.MakeInvestment(Players[i - 1].Investments[j - 1], facility);
				}
			}

		}

		public static class Communication
        {
            public static void MakeInvestment(Player.Investment investment, Facility facility)
            {
				//The investment is taken off the previous facility 
				if (investment.Facility != null)
					investment.Facility.AllInvestments.Remove(investment);

				//Then set to a new one
                investment.Facility = facility;
				facility.AllInvestments.Add(investment);
            }
        }

		//This is called after the end of the turn
        public static bool ExecuteTurn()
        {
            if(!CheckPlayers())
                return false;

			//Nullifiy all the return items, as a new turn has begun
			Facilities.ForEach(x => x.LastReturnItem = null);

            foreach(var player in Players)
            {
				foreach(var investment in player.Investments)
				{
					//investment is modified in this method, so no problems
					ExectueTurn(investment);
				}
            }

            return true;
        }

        private static bool CheckPlayers()
        {
            //For each player
            //For each investement
            //If there is an investment without any facilities
            //return false
            return Players.Where(p => p.Investments.Where(x => x.Facility == null).Count() > 0).Count() == 0;
        }

        private static void ExectueTurn(Player.Investment investment)
        {			
			//First find the return value
			//For optimisation purposes, we save the return item for the facilities
            Facility.ReturnItem returnItem;
            if(investment.Facility.LastReturnItem.HasValue)
                returnItem = investment.Facility.LastReturnItem.Value;
            else
                returnItem = GetReturnItem(investment.Facility);

			//Save previous value to history
			//Update the return item
			investment.ReturnItem = investment.Facility.LastReturnItem;
			investment.SaveToHistory();

			if(returnItem.ReturnType == Facility.ReturnItemType.Additive)
                investment.TotalMoney += returnItem.ReturnValue;
            else if(returnItem.ReturnType == Facility.ReturnItemType.Multiplier)
                investment.TotalMoney *= returnItem.ReturnValue;
        }

        private static Facility.ReturnItem GetReturnItem(Facility facility)
        {
            double totalMoney = facility.AllInvestments.Sum(x => x.TotalMoney);

            KeyValuePair<double, double> lowerBound = new KeyValuePair<double, double>();
			//lowerBound < total money invested, lowerBound.Key -> max
            foreach(KeyValuePair<double, double> pair in facility.Boundaries)
            {
                if(pair.Key < totalMoney)
                    lowerBound = pair;
            }

            var res = new Facility.ReturnItem(facility.FacilityReturnType, lowerBound.Value);
            facility.LastReturnItem = res;
            return res;
        }
    }
}
