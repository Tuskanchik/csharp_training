using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

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
            ReturnToHomePage();

            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }



        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            InitContactRemoval();
            ReturnToHomePage();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (v + 2) + "]/td")).Click();

            return this;
        }

        public ContactHelper InitContactRemoval()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (v + 2) + "]/td[8]/a/img")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
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
            contactCache = null;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public bool CheckContactsListIsNotEmpty()
        {
            return IsElementPresent(By.Name("selected[]"));                          
        }

        public ContactHelper CreateContactIfContactsListIsEmpty()
        {
            if (CheckContactsListIsNotEmpty() == false)
            {
                ContactData contact = new ContactData("", "");
                Create(contact);
            }
            return this;
        }

        private List<ContactData> contactCache = null;

        public int GetContactsCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        internal List<ContactData> GetContactsList()
        {
            if (contactCache == null) {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> rows = driver.FindElements(By.Name("entry"));
                foreach (IWebElement row in rows)
                {
                    IList<IWebElement> cells = row.FindElements(By.TagName("td"));

                    string lastName = cells[1].Text;
                    string firstName = cells[2].Text;

                    contactCache.Add(new ContactData(firstName, lastName) {
                        Id = cells[0].FindElement(By.TagName("input")).GetAttribute("id")
                    });
                }
            }
            return new List<ContactData>(contactCache);


            //List<ContactData> contacts = new List<ContactData>();
            //manager.Navigator.GoToHomePage();
            //ICollection<IWebElement> rows = driver.FindElements(By.Name("entry"));
            //foreach (IWebElement row in rows)
            //{
            //    IList<IWebElement> cells = row.FindElements(By.TagName("td"));

            //    string lastName = cells[1].Text;
            //    string firstName = cells[2].Text;

            //    contacts.Add(new ContactData(firstName, lastName));
            //}
            //return contacts;

        }

        internal ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
            };
        }

        internal ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
