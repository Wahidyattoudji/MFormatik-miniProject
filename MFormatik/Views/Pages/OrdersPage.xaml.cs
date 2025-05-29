using MFormatik.ViewModels;
using System.Windows.Controls;

namespace MFormatik.Views.Pages
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage(OrdersPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.NavigateToAddOrder();
            Loaded += (s, e) => viewModel.EnsureLoadOrders(); // Lazy load orders when the page is loaded
        }
    }
}
