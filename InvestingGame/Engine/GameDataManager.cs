using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

using InvestingGame.GameLogic;

namespace InvestingGame.Engine
{
    public class GameDataManager
    {
        private XDocument BaseDocument; 

        public GameDataManager(string path)
        {
            BaseDocument = XDocument.Load(path);
        }

        public void AddFacility(Facility facility)
        {
            var facilityElement = BaseDocument.Element("Facilities");
            XElement boundaries = new XElement("Boundaries");
            foreach(KeyValuePair<double, double> pair in facility.Boundaries)
            {
                boundaries.Add(new XElement("KeyValuePair",
                    new XAttribute("Key", pair.Key), 
                    new XAttribute("Value", pair.Value)));
            }

            XElement next = new XElement("Facility",
                new XAttribute("Name", facility.Name),
                new XAttribute("Description", facility.Description),
                new XAttribute("Type", ( (int)facility.FacilityReturnType ).ToString()),
                boundaries);

            facilityElement.Add(next);
        }

        public void SaveTo(string path)
        {
            BaseDocument.Save(path);
        }
    }
}
