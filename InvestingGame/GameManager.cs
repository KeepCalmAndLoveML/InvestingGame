using System;
using System.Collections.Generic;
using System.Linq;

using InvestingGame.Engine.GameLogic;

namespace InvestingGame.Engine
{
    public static class GameManager
    {
        private static List<Facility> Facilities;
        private static List<Player> Players;      
        
        //Starts the game
        //TODO: Add loader from .txt or .xaml file here
        public static void Initialize()
        {
            Facilities = new List<Facility>();
            Players = new List<Player>();
        }

        public static void ExecuteTurn()
        {
            foreach(var player in Players)
            {
                foreach(var investment in player.Investments)
                {
                    //investment is modified in this method, so no problems
                    ExectueTurn(investment);
                }
            }
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
