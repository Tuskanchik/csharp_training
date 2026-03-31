using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30)) 
                //тут неплохо бы знать ограничения для полей, и указать количество символов соответственно этим ограничениям
                {
                    MiddleName = GenerateRandomString(30),
                    Address = GenerateRandomString(30),
                    HomePhone = GenerateRandomPhone(),
                    MobilePhone = GenerateRandomPhone(),
                    WorkPhone = GenerateRandomPhone()
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }


        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                    .Deserialize(new StreamReader(@"contacts.xml"));
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void ContactCreationTest(ContactData contact)
        {
            //ContactData contact = new ContactData("Irina", "Bashmakova");

            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

        //[Test]
        //public void EmptyContactCreationTest()
        //{
        //    ContactData contact = new ContactData("", "");
 
        //    List<ContactData> oldContacts = app.Contacts.GetContactsList();

        //    app.Contacts.Create(contact);
        //    app.Navigator.GoToHomePage();

        //    Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

        //    List<ContactData> newContacts = app.Contacts.GetContactsList();
        //    oldContacts.Add(contact);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    Assert.AreEqual(oldContacts, newContacts);
        //}

        //[Test]
        //public void IntValueContactCreationTest()
        //{
        //    ContactData contact = new ContactData("", "");
        //    contact.FirstName = "2";
        //    contact.LastName = "3";

        //    List<ContactData> oldContacts = app.Contacts.GetContactsList();

        //    app.Contacts.Create(contact);
        //    app.Navigator.GoToHomePage();
            
        //    Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

        //    List<ContactData> newContacts = app.Contacts.GetContactsList();
        //    oldContacts.Add(contact);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    Assert.AreEqual(oldContacts, newContacts);
        //}

        //[Test]
        //public void CloneContactTest()
        //{
        //    //ContactData contact = new ContactData("", "");
        //    //contact.FirstName = "2";
        //    //contact.LastName = "3";
        //    ContactData contact = app.Contacts.GetContactInformationFromEditForm(0);


        //    List<ContactData> oldContacts = ContactData.getAll();

        //    app.Contacts.Create(contact);
        //    app.Navigator.GoToHomePage();

        //    Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

        //    List<ContactData> newContacts = ContactData.getAll();
        //    oldContacts.Add(contact);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    Assert.AreEqual(oldContacts, newContacts);
        //}

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUi = app.Contacts.GetContactsList();
            DateTime end = DateTime.Now;
            Console.Write(end.Subtract(start));

            start = DateTime.Now;
            List<ContactData> fromDb = ContactData.GetAll();
            end = DateTime.Now;
            Console.Write(end.Subtract(start));
        }
    }
}
