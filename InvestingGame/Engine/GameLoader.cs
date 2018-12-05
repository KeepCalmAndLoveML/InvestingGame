using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Linq;

using InvestingGame.GameLogic;

namespace InvestingGame.Engine
{
    public class GameLoader
    {
        private XDocument BaseDocument;
        
        public GameLoader(string path)
        {
            BaseDocument = XDocument.Load(path);
        }
        
        //This works!
        public List<Facility> GetFacilities()
        {
            List<Facility> res = new List<Facility>();

            var elements = BaseDocument.Element("Facilities").Elements();
            foreach(var facility in elements)
            {
                string name = facility.Attribute("Name").Value;
                string description = facility.Attribute("Description").Value;
                string type = facility.Attribute("Type").Value;
                Facility.ReturnItemType returnItemType;
                if (!Facility.ReturnItemFromString(type, out returnItemType)) //Invalid type
                    continue;

                var boundariesElements = facility.Element("Boundaries").Elements(); //Key Value pairs
                Dictionary<double, double> boundaries = new Dictionary<double, double>();

                double key, value;
                foreach(var element in boundariesElements)
                {
                    string sKey = element.Attribute("Key").Value;
                    string sValue = element.Attribute("Value").Value;

                    if (double.TryParse(sKey, out key))
                    {
                        if (double.TryParse(sValue, out value))
                        {
                            boundaries.Add(key, value);
                        }
                    }
                }

                res.Add(new Facility(name, description, boundaries, returnItemType));
            }

            return res;
        }
    }
}
