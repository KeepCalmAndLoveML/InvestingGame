using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestingGame.GameLogic
{ 
    public class Facility
    {
        //Info for displaying
        public readonly string Name;
        public readonly string Description;

        //Key = Lower bound for multiplier
        //Value = Multiplier
        public readonly Dictionary<double, double> Boundaries;

        public readonly ReturnItemType FacilityReturnType;
        
        //This is always >= 0       
        public List<Player.Investment> AllInvestments { get; private set; }

        //Created for optimisation purposes, null if GetReturnItem() was not executed on this turn, else ReturnItem
        //Entirely Managed in GameManager class
        public ReturnItem? LastReturnItem { get; set; }

        public Facility(string name, string description, Dictionary<double, double> boundaries, ReturnItemType type)
        {
            this.Name = name;
            this.Description = description;
            this.FacilityReturnType = type;
			this.AllInvestments = new List<Player.Investment>();

			if(boundaries.ContainsKey(0d))
				this.Boundaries = boundaries;
			else
			{
				Boundaries = new Dictionary<double, double>();
				Boundaries.Add(0, 0);
				//throw new ArgumentException("Facility boundaries should contain 0 key");
			}
        }

        //TODO: Add dice throwing return type
        public enum ReturnItemType
        {
            Multiplier = 0,
            Additive = 1,
        }

        //Todo: refactor this into bool TryParse(out return)
        public static bool ReturnItemFromString(string s, out ReturnItemType returnItemType)
        {
            returnItemType = ReturnItemType.Additive;

            int value;
            if(int.TryParse(s, out value))
            {
                var returnItemTypesasInts = Enum.GetValues(typeof(ReturnItemType));
                foreach(object obj in returnItemTypesasInts)
                {
                    if(value == (int)obj)
                    {
                        returnItemType = (ReturnItemType)obj;
                        return true;
                    }
                }

                return false;
            }
            else
                return false;
        }

        public struct ReturnItem
        {
            public ReturnItemType ReturnType { get; private set; }
            public double ReturnValue { get; private set; }

            public ReturnItem(ReturnItemType type, double value)
            {
                ReturnType = type;
                ReturnValue = value;
            }
        }
    }
}
