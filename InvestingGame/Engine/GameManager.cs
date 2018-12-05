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
        public const int PlayerCount = 4;
        public const int InvestementsPerPlayer = 3;
        public const double PlayersInitialSum = 10;

        private static List<Facility> Facilities;
        private static List<Player> Players;

        public static string DataPath = "data.xml";

        //Starts the game
        //TODO: Add loader from .txt or .xaml file here
        public static void Initialize(string newPath = "")
        {
            if(!string.IsNullOrEmpty(newPath))
                DataPath = newPath;

            GameLoader gameLoader = new GameLoader(DataPath);
            Facilities = gameLoader.GetFacilities();

            Players = new List<Player>();
            for(int i = 1; i <= PlayerCount; i++)
            {
                Players.Add(new Player($"Player {i}"));
                for(int j = 1; j <= InvestementsPerPlayer; j++)
                {
                    Players.Last().Investments.Add(new Player.Investment($"Investment {j}", PlayersInitialSum));
                }
            }
        }

        public static bool ExecuteTurn()
        {
            if(!CheckPlayers())
                return false;

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
            //Save previous value to history
            investment.History.Add(investment);

            Facility.ReturnItem returnItem;
            if(investment.Facility.LastReturnItem.HasValue)
                returnItem = investment.Facility.LastReturnItem.Value;
            else
                returnItem = GetReturnItem(investment.Facility);

            if(returnItem.ReturnType == Facility.ReturnItemType.Additive)
                investment.TotalMoney += returnItem.ReturnValue;
            else if(returnItem.ReturnType == Facility.ReturnItemType.Multiplier)
                investment.TotalMoney *= returnItem.ReturnValue;
        }

        private static Facility.ReturnItem GetReturnItem(Facility facility)
        {
            double totalMoney = facility.TotalInvestments.Sum(x => x.TotalMoney);

            KeyValuePair<double, double> lowerBound = new KeyValuePair<double, double>();
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
