//using System;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigator.GoToAddNewPage();
            contactHelper.FillContactForm(new ContactData("Igor", "Novikov"));
            contactHelper.SubmitContactCreation();
            navigator.GoToHomePage();
            loginHelper.Logout();
        }
    }
}
