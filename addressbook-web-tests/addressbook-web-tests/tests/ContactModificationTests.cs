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
            ContactData oldData = oldContacts[0];

            app.Contacts.Modify(0, contact);
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts[0].FirstName = contact.FirstName;
            oldContacts[0].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData cntct in newContacts)
            {
                if (cntct.Id == oldData.Id)
                {
                    Assert.AreEqual(cntct.FirstName, contact.FirstName);
                    Assert.AreEqual(cntct.LastName, contact.LastName);
                }
            }
        }
    }
}
