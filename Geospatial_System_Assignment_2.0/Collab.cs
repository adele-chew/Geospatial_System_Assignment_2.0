using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ICT365_Assignment2
{
    /// <summary>
    /// collaborator class
    /// </summary>
    public class Collab
    {
        private string name, type, time, pic;           //strings for collaborator's name, support type, support time, and relative url to picture
        private double lat, lon;                        //double variables for latitude and longitude

        //default constructor
        public Collab() { }         

        //constructor with values
        public Collab(string name, string type, string time, double lat, double lon, string pic)
        {
            this.name = name;
            this.type = type;
            this.time = time;
            this.lat = lat;
            this.lon = lon;
            this.pic = pic; 
        }

        //getter/setter for name variable
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            } 
        }

        //getter/setter for type variable
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        //getter/setter for time variable
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }

        //getter/setter for latitude variable
        public double Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
            }
        }

        //getter/setter for longitude variable
        public double Lon
        {
            get
            {
                return lon; 
            }
            set
            {
                lon = value;
            }
        }

        //getter/setter for picture variable
        public string Pic
        {
            get
            {
                return pic; 
            }
            set
            {
                pic = value;
            }
        }

        //function to add object to xml 
        public void AddToXML()
        {
            XDocument file = CollabFile.getFile;                                        //call to collaborator file singleton
            file.Root.Add(new XElement("Collaborator", new XAttribute("name", name),    //create new element collaborator with attributes name,
                                                       new XAttribute("type", type),    //type,
                                                       new XAttribute("time", time),    //time,
                                                       new XAttribute("lat", lat),      //latitude,
                                                       new XAttribute("lon", lon),      //longitude,
                                                       new XAttribute("pic", pic)));    //and picture

            file.Save(HttpContext.Current.Server.MapPath("collaborator.xml"));          //save object to xml file 
        }
    }
}