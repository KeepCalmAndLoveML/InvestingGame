using System;
using System.Collections.Generic;
using System.Text;

namespace InvestingGame.Engine.GameLogic
{
    public class Facility
    {
        //Info for drawing
        public readonly string Name; 
        public readonly string Description;

        //Key = Lower bound for multiplier
        //Value = Multiplier
        private readonly Dictionary<double, double> Boundaries;

        private readonly ReturnItemType FacilityReturnType;

        //TODO: Refactor this into List<Investment>
        //This is always >= 0       
        public double TotalMoneyInvested { get; private set; }

        public Facility(string name, string description, Dictionary<double, double> boundaries, ReturnItemType type)
        {
            this.Name = name;
            this.Description = description;
            this.FacilityReturnType = type;

            if(boundaries.ContainsKey(0d))
                this.Boundaries = boundaries;          
            else
                throw new ArgumentException("Facility boundaries should contain 0 key");
            
        }
        
        public ReturnItem GetReturnItem()
        {
            KeyValuePair<double, double> lowerBound;
            foreach(KeyValuePair<double, double> pair in Boundaries)
            {
                if(pair.Key < TotalMoneyInvested)
                    lowerBound = pair;
            }

            return new ReturnItem(FacilityReturnType, lowerBound.Value);
        }


        //TODO: Add dice throwing return type
        public enum ReturnItemType
        {
            Multiplier,
            Additive,
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
