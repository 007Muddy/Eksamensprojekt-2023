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
        private OrderRepo orderRepo = new OrderRepo();
        private CustomerRepo customerRepo = new CustomerRepo();
        private AvailabilityRepo availabilityRepo = new AvailabilityRepo();
        private ObservableCollection<OrderViewModel> orderVM;
        public ObservableCollection<OrderViewModel> OrderVM
        {
            get => orderVM;
            set
            {
                orderVM = value;
                OnPropertyChanged(nameof(OrderVM));
            }
        }
        private OrderViewModel selectedOrder;
        public OrderViewModel SelectedOrder
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
            OrderVM = new ObservableCollection<OrderViewModel>();
            foreach (Order item in orderRepo.RetrieveOrders())
            {
                OrderViewModel ordersViewModel = new(item);
                OrderVM.Add(ordersViewModel);

            }

        }
        public void RemoveAuthentication()
        {
            customerRepo.RemoveAuthentication();
        }
        public void UpdateOrdersVM()
        {
            OrderVM.Clear();
            foreach (Order item in orderRepo.RetrieveOrders())
            {
                OrderViewModel ordersViewModel = new(item);
                OrderVM.Add(ordersViewModel);
            }
            OnPropertyChanged("OrdersVM");

        }
        public void CancelOrder()
        {

            orderRepo.RemoveOrder(SelectedOrder.OrderID);
            availabilityRepo.AddAvailability(DateOnly.Parse(SelectedOrder.Date), TimeOnly.Parse(SelectedOrder.Time));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
