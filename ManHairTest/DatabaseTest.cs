using ManHair.Model;
using ManHair.ViewModel;
using ManHair.ViewModel.Repositories;

namespace ManHairTest
{
    [TestClass]
    public class DatabaseTest
    {
        Availability av;
        Orders o;
        Customer c1;
        Customer c2;
        Treatment t;

        [TestInitialize]
        public void TestInitialize()
        {
            DateOnly DO = new DateOnly(1999, 9, 13);
            TimeOnly TO = new TimeOnly(15, 20, 00);


            av = new Availability(DO, TO);
            //o = new Orders(142,DO,TO,205,5,52);
            c1 = new Customer("sune@mail.dk", "Test");
            c2 = new Customer(52,"Test",51674558,"TestForID@gmail.com","Test");
            t = new Treatment(Treatment.TreatmentType.HairCut | Treatment.TreatmentType.Shaving, 205);
        }

        [TestMethod]
        public void GetCustomerIDFromDatabase()
        {
            // #### ARRANGE ####
            CustomerRepo repo = new CustomerRepo();

            // #### ASSERT ####
            Assert.AreEqual("3", repo.GetID(c1.Email).ToString());  
        }

        [TestMethod]
        public void AccessGrantedForCustomer()
        {
            // #### ARRANGE #### 
            CustomerRepo repo = new CustomerRepo();

            // #### ACT ####
            repo.AddCustomer(c2.Name, c2.Phone, c2.Email, c2.Password);

            // #### ASSERT ####
            Assert.AreEqual("True", repo.AuthenticateUser(c2).ToString());
            repo.RemoveCustomer(c2.Email);
        }
        
        [TestMethod]
        public void MakeAnOrder()
        {
            // #### ARRANGE #### 
            OrdersRepo repo = new OrdersRepo();

            // #### ACT ####
            repo.AddOrder(6, av.Date.ToString(), av.Time.ToString(), t.Price, (int)t.Types);
            foreach(Orders order in repo.GetCustomerOrders(6))
            {
                o = new Orders(order.OrderID, order.Date, order.Time, order.Price, order.Treatment, order.CustomerID);
            }

            // #### ASSERT ####
            Assert.AreEqual("56;13/09/1999;15:20:00;205;6;6", o.ToString());
            repo.RemoveOrder(6);

        }
    }
}