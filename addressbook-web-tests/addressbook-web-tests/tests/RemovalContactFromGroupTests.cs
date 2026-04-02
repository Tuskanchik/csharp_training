using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    internal class RemovalContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovalContactFromGroupTest()
        {
            // условно предполагаем что есть хотя бы 1 не пустая группа
            // по хорошему надо дополнительно проверять 3 вещи:
            // 1. если контактов нет, то создавать
            // 2. если групп нет то создавать, и добавлять в неё контакт
            // 3. если группы есть но ни одна группа не содержит контактов, то добавлять контакт в группу
            GroupData group = app.Groups.GetNotEmptyGroup(); 
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList[0];

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
