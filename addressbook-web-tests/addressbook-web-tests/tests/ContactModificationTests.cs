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
            
            ContactData newData = new ContactData("", "");
            newData.FirstName = "Irina";
            newData.LastName = "Novikova";

            app.Contacts.Modify(1, newData);
        }
    }
}
