using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.Infrastructure
{
    public class CustomerRepository : IRepository<Customer, string>
    {
        static List<Customer> customerList;

        public CustomerRepository()
        {
            if (customerList == null)
            {
                customerList = new List<Customer>();
                AddItemToList();
            }
        }

        private void AddItemToList()
        {
            Customer c1 = new Customer { CustomerID = "5Star", CompanyName = "SureshRamesh Corp",
                ContactName ="Suresh Mehta",City="Valsad" ,Country="India"};

            customerList.Add(c1);
            
            customerList.Add( new Customer
            {
                CustomerID = "6 Star",
                CompanyName = "Canarys automatios pve ltd",
                ContactName = "Sagar Kumar ",
                City = "Bangalore",
                Country = "Bangkok"
            });
            
            customerList.Add(new Customer
            {
                CustomerID = "7Star",
                CompanyName = "IBSFintech automatios pve ltd",
                ContactName = "Mohan Kumar ",
                City = "Bijapur",
                Country = "UK"
            });

            customerList.Add(new Customer
            {
                CustomerID = "8Star",
                CompanyName = "FintechFun automatios pve lts",
                ContactName = "Rajendra Kumar ",
                City = "Belagaum",
                Country = "China"
            });
        }
           
        public IQueryable<Customer> GetAll()
        {
            //language integrated query(LINQ)
            var query = from item in customerList select item;
            //
            return query.AsQueryable();
        }

        public Customer GetDetails(string identity)
        {
            //customerList.First(c => c);//will throw error if doesnot find values
            return customerList.FirstOrDefault(c => c.CustomerID.Equals(identity));//if tht values are not their in the 
            //first these will show the default value
        }













        public void CreateNew(Customer item)
        {
            if(item!=null)
            {
                customerList.Add(item);
            }
        }

        public void delete(string identity)
        {
            if(!string.IsNullOrEmpty(identity))
            {
                customerList.RemoveAll(c => c.CustomerID == identity);
            }
        }
        
        public void Update(Customer item)
        {
            if (item != null)
            {
                customerList.RemoveAll(c => c.CustomerID == item.CustomerID);
                customerList.Add(item);
            }
        }
    }
}