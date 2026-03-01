using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) 
            : base(manager) 
        { 
        }
        
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToAddNewPage();
            FillContactForm(contact);
            SubmitContactCreation();

            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            CheckContactsListIsNotEmpty();
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }



        public ContactHelper Remove(int v)
        {
            CheckContactsListIsNotEmpty();
            SelectContact(v);
            InitContactRemoval();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + ++v + "]/td")).Click();

            return this;
        }

        public ContactHelper InitContactRemoval()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + ++v + "]/td[8]/a/img")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public void FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
        }
        public void SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[19]")).Click();
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper CheckContactsListIsNotEmpty()
        {
            if (IsElementPresent(By.Name("selected[]")))
            {
                return this;
            }
            else
            {
                ContactData contact = new ContactData("", "");
                Create(contact);
                manager.Navigator.GoToHomePage();
                return this;
            }
        }
    }
}
