using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobilaApp.Models
{
	//Will be used as an ItemTemplate
    public class Facility
    {
		//This will be passed to the ViewModel
		public InvestingGame.GameLogic.Facility BaseFacility { get; set; }

		public Color BackgroundColor { get; set; }
		public Image Icon { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		//Each facility can get up to 6 keys and values
		//This should not be a dictionary, because it will use bindings!
		public string SampleKey { get; set; }

		public string Key2 { get; set; }
		public string Key3 { get; set; }
		public string Key4 { get; set; }
		public string Key5 { get; set; }
		public string Key6 { get; set; }

		public string SampleValue { get; set; }

		public string Value2 { get; set; }
		public string Value3 { get; set; }
		public string Value4 { get; set; }
		public string Value5 { get; set; }
		public string Value6 { get; set; }
    }
}
