using MFormatik.ViewModels.OrderVms;
using System.Windows;

namespace MFormatik.Views.OrderViews
{
    /// <summary>
    /// Interaction logic for AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : Window
    {
        public AddOrderView(AddOrderVM addOrderVM)
        {
            InitializeComponent();
            DataContext = addOrderVM;
        }
    }
}
