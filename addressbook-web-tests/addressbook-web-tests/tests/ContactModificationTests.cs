using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]

        public void ContactModificationTest()
        {
            app.Contacts.CreateContactIfContactsListIsEmpty();
            
            ContactData contact = new ContactData("", "");
            contact.FirstName = "Irina";
            contact.LastName = "Novikova";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Modify(0, contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts[0].FirstName = contact.FirstName;
            oldContacts[0].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
