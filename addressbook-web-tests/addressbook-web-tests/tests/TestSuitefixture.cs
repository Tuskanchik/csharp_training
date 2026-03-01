using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [SetUpFixture]
    public class TestSuitefixture
    {

        ApplicationManager app = ApplicationManager.GetInstance();

        [SetUp]
        public void InitApplicationManager()
        {
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        public void StopApplicationManager()
        {
            
            app.Auth.Logout();
        }
    }
}
