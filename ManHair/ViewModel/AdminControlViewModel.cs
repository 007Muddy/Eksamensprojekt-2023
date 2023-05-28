using ManHair.Model;
using ManHair.Model.Persistence;
using ManHair.View;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class AdminControlViewModel : INotifyPropertyChanged
    {
        private OrdersRepo ordersRepo = new OrdersRepo();
        private CustomerRepo customerRepo = new CustomerRepo();
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
        public AdminControlViewModel()
        {
            OrdersVM = new ObservableCollection<OrdersViewModel>();
            foreach (Orders item in ordersRepo.RetrieveOrders())
            {
                OrdersViewModel ordersViewModel = new(item);
                OrdersVM.Add(ordersViewModel);

            }

        }
        public void RemoveAuthentication()
        {
            customerRepo.RemoveAuthentication();
        }
        public void UpdateOrdersVM()
        {
            OrdersVM.Clear();
            foreach (Orders item in ordersRepo.RetrieveOrders())
            {
                OrdersViewModel ordersViewModel = new(item);
                OrdersVM.Add(ordersViewModel);
            }
            OnPropertyChanged("OrdersVM");

        }
        public void CancelOrder()
        {

            ordersRepo.RemoveOrder(SelectedOrder.OrderID);
            availabilityRepo.AddAvailability(DateOnly.Parse(SelectedOrder.Date), TimeOnly.Parse(SelectedOrder.Time));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
