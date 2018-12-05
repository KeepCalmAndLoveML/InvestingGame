using System;
using System.Collections.Generic;
using System.Text;

namespace InvestingGame.GameLogic
{
    public class Player
    {
        //Used for display
        readonly string Name;

        public List<Investment> Investments;

        public Player(string name)
        {
            this.Name = name;
            this.Investments = new List<Investment>();
        }

        public class Investment
        {
            //User-assignable, used for display
            //Same for every element in history
            readonly string Name;

            public double TotalMoney { get; set; }

            public Facility Facility { get; set; }

            public List<Investment> History;

            public Investment(string name, double initialSum)
            {
                this.Name = name;
                this.TotalMoney = initialSum;
                this.Facility = null;

                this.History = new List<Investment>();
            }
        }
    }
}
