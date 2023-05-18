using ManHair.Model;
using ManHair.Model.Persistence;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ManHair.Model.Treatment;

namespace ManHair.ViewModel
{
    public class CustomerHomeViewModel : INotifyPropertyChanged
    {
        private OrdersRepo ordersRepo = new OrdersRepo();
        private CustomerRepo customerRepo = new CustomerRepo();
        private AuthenticationRepo authenticationRepo = new AuthenticationRepo();
        private AvailabilityRepo availabilityRepo = new AvailabilityRepo();
        private ObservableCollection<OrdersViewModel> ordersVM;
        public ObservableCollection<OrdersViewModel> OrdersVM
        {
            get => ordersVM;
            set
            {
                ordersVM = value;
                OnPropertyChanged(nameof(OrdersVM));
            }
        }
        private OrdersViewModel selectedOrder;
        public OrdersViewModel SelectedOrder
        {
            get => selectedOrder;
            set
            {

                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }
        public CustomerHomeViewModel()
        {
            OrdersVM = new ObservableCollection<OrdersViewModel>();
            foreach (Orders item in ordersRepo.GetCustomerOrders(customerRepo.getID(authenticationRepo.getEmail())))
            {
                OrdersViewModel ordersViewModel = new(item);
                OrdersVM.Add(ordersViewModel);  
            }
        }

        public void RemoveAuthentication()
        {
            authenticationRepo.Remove();
            UpdateOrdersVM();
        }
        public void UpdateOrdersVM()
        {
            OrdersVM.Clear();
            foreach (Orders item in ordersRepo.GetCustomerOrders(customerRepo.getID(authenticationRepo.getEmail())))
            {
                OrdersViewModel ordersViewModel = new(item);
                OrdersVM.Add(ordersViewModel);
            }
            OnPropertyChanged("OrdersVM");

        }
        public void CancelOrder()
        {

            ordersRepo.Remove(SelectedOrder.OrderID);
            availabilityRepo.Add(DateOnly.Parse(SelectedOrder.Date), TimeOnly.Parse(SelectedOrder.Time));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
