using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
         
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Irina", "Bashmakova");

            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            
            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");
 
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void IntValueContactCreationTest()
        {
            ContactData contact = new ContactData("", "");
            contact.FirstName = "2";
            contact.LastName = "3";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();
            
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void CloneContactTest()
        {
            //ContactData contact = new ContactData("", "");
            //contact.FirstName = "2";
            //contact.LastName = "3";
            ContactData contact = app.Contacts.GetContactInformationFromEditForm(0);


            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
