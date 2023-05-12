using ManHair.Model;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class AdminControlViewModel
    {
        private OrdersRepo orderRepo = new OrdersRepo();
        public ObservableCollection<OrdersViewModel> OrdersVM { get; set; } = new();
        public AdminControlViewModel()
        {
            foreach (Orders item1 in orderRepo.GetOrders())
            {
                OrdersViewModel ordersViewModel = new(item1);
                OrdersVM.Add(ordersViewModel);

            }

        }
    }
}
