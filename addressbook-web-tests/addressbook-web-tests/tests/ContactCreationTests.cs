using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Irina", "Novikova");

            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();
            app.Auth.Logout();
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();
            app.Auth.Logout();
        }

        [Test]
        public void IntValueContactCreationTest()
        {
            ContactData contact = new ContactData("", "");
            contact.FirstName = "2";
            contact.LastName = "3";

            app.Contacts.Create(contact);
            app.Navigator.GoToHomePage();
            app.Auth.Logout();
        }
    }
}
