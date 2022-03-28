using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ICT365_Assignment2;
using System.Xml.Linq;  

namespace Assignment2_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Collaborator_Object()
        {
            //test to validate the object is created properly
            Collab person = new Collab("James", "Carer", "8:00-19:00", 1.357309, 103.859931, "../Content/Images/Sam.png");

            Assert.AreEqual("James", person.Name);
            Assert.AreEqual("Carer", person.Type);
            Assert.AreEqual("8:00-19:00", person.Time);
            Assert.AreEqual(1.357309, person.Lat);
            Assert.AreEqual(103.859931, person.Lon);
            Assert.AreEqual("../Content/Images/Sam.png", person.Pic);
        }
    }
}
