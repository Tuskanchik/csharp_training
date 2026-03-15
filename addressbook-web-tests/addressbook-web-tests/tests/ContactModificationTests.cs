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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
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

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactModificationTest(ContactData contact)
        {
            app.Contacts.CreateContactIfContactsListIsEmpty();

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
