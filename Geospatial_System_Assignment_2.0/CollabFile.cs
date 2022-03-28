using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ICT365_Assignment2
{
    /// <summary>
    /// Singleton class to retrieve collaborator xml File using lock method
    /// </summary>
    public class CollabFile
    {
        private static XDocument file = null;
        private static readonly object padlock = new object();

        private CollabFile()
        {
            XDocument file = XDocument.Load(HttpContext.Current.Server.MapPath("collaborator.xml"));
        }

        public static XDocument getFile
        {
            get
            {
                if (file == null)
                {
                    lock (padlock)
                    {
                        if (file == null)
                        {
                            file = XDocument.Load(HttpContext.Current.Server.MapPath("collaborator.xml"));
                        }
                    }
                }
                return file;
            }
        }
    }
}