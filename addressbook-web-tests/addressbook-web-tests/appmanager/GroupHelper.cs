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
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int v, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(v);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(GroupData oldData, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(oldData.Id);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper Remove(int v)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(v);
            DeleteSelectedGroups();
            ReturnToGroupsPage();
            groupCache = null;
            return this;
        }

        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(group.Id);
            DeleteSelectedGroups();
            ReturnToGroupsPage();
            groupCache = null;
            return this;
        }
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + (index + 1)+ "]/input")).Click();
            return this;
        }

        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
            return this;
        }
        public GroupHelper DeleteSelectedGroups()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
        public bool CheckGroupsListIsNotEmpty()
        {
            //return IsElementPresent(By.Name("selected[]"));
            return GroupData.GetAll().Count > 0;
        }

        private bool CheckForAGroupWithAddedUsers()
        {
            List<GroupData> groups = GroupData.GetAll();
            bool SomeGroupExistContacts = false;
            foreach (GroupData group in groups)
            {
                if (group.GetContacts().Count > 0)
                {
                    SomeGroupExistContacts = true;
                    break;
                }
            }
            return SomeGroupExistContacts;
        }
        public GroupHelper CreateGroupIfGroupsListIsEmpty()
        {
            if (CheckGroupsListIsNotEmpty() == false)
            {
                GroupData groupData = new GroupData("qqq");
                groupData.Header = "www";
                groupData.Footer = "eee";

                Create(groupData);
            }
            return this;
        }

        private void AddContactToGroupIfNoOneGroupHaveContacts()
        {
            if (CheckForAGroupWithAddedUsers() == false)
            {
                manager.Contacts.AddFistContactToFirstGroup();
            }
        }
        public GroupData GetNotEmptyGroup()
        {
            manager.Contacts.CreateContactIfContactsListIsEmpty();
            CreateGroupIfGroupsListIsEmpty();
            AddContactToGroupIfNoOneGroupHaveContacts();

            List<GroupData> groups = GroupData.GetAll();
            GroupData group = null;
            foreach (GroupData g in groups)
            {
                if (g.GetContacts().Count > 0)
                {
                    group = g;
                    break;
                }
            }
            return group;
        }
        public GroupData GetGroupThatDoesntHaveAllContacts()
        {
            manager.Contacts.CreateContactIfContactsListIsEmpty();
            CreateGroupIfGroupsListIsEmpty();
            CreateGroupIfAllGroupsHaveAllContacts();
            GroupData group = null;
            List<GroupData> groups = GroupData.GetAll();
            int usersCount = ContactData.GetAll().Count();
            foreach (GroupData g in groups)
            {
                if (g.GetContacts().Count() < usersCount)
                {
                    group = g;
                    break;
                }
            }
            return group;
        }

        private void CreateGroupIfAllGroupsHaveAllContacts()
        {
            GroupData group = null;
            List<GroupData> groups = GroupData.GetAll();
            int usersCount = ContactData.GetAll().Count();
            foreach (GroupData g in groups)
            {
                if (g.GetContacts().Count() < usersCount)
                {
                    group = g;
                    break;
                }
            }
            if (group == null)
            {
                Create(new GroupData("Default Group"));
            }
        }

        private List<GroupData> groupCache = null;
        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData(element.Text) {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value"),
                    });

                    //GroupData group = new GroupData(element.Text) {
                    //    Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    //};
                    //groupCache.Add(group);

                    //GroupData group = new GroupData(element.Text);
                    //group.Id = element.FindElement(By.TagName("input")).GetAttribute("value");
                    //groupCache.Add(group);
                }
            }
            return new List<GroupData>(groupCache);


            //Изначальный вариант до кэширования

            //List<GroupData> groups = new List<GroupData>();
            //manager.Navigator.GoToGroupsPage();
            //ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
            //foreach (IWebElement element in elements)
            //{
            //    groups.Add(new GroupData(element.Text));
            //}
            //return groups;
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }




    }
}
