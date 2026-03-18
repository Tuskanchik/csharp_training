using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformationTableAndEditForm()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }


        [Test]
        public void TestContactInformationViewFormAndEditForm()
        {
            ContactData fromEditForm = app.Contacts.GetContactInformationFromEditForm(0);
            string editFormDataModyfyed = app.Contacts.ProvideContactInformationInViewModeStyle(fromEditForm);
            string fromViewForm = app.Contacts.GetContactInformationFromViewForm(0);
            //Console.WriteLine(editFormDataModyfyed);
            //Console.WriteLine(fromViewForm);

            Assert.AreEqual(editFormDataModyfyed, fromViewForm);
        }

    }
}
