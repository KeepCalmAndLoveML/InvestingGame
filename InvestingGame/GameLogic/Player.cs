using System;
using System.Collections.Generic;
using System.Text;

namespace InvestingGame.GameLogic
{
    public class Player
    {
        //Used for display
        public readonly string Name;

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
			public readonly string Name;
			public readonly Player Owner;

			public double TotalMoney { get; set; }

			public Facility Facility
			{
				get => _facility;
				set
				{
					if(value != null)
					{
						ReturnItem = _facility.LastReturnItem;
						_facility = value;
					}
				}
			}

			public Facility.ReturnItem? ReturnItem { get; set; }

			public List<Investment> History;
			private Facility _facility;

			public Investment(Player source, string name, double initialSum)
			{
				Name = name;
				TotalMoney = initialSum;
				Owner = source;
				_facility = Engine.GameManager.DefaultFacility;
				ReturnItem = _facility.LastReturnItem;

				History = new List<Investment>();
			}

			public void SaveToHistory()
			{
				Investment copy = new Investment(this.Owner, this.Name, this.TotalMoney);
				copy.Facility = this.Facility;
				this.History.Add(copy);
			}
		}
	}
}
