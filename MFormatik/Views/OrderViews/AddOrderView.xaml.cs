using MFormatik.Services.Abstracts;
using MFormatik.ViewModels.OrderVms;
using System.Windows;

namespace MFormatik.Views.OrderViews
{
    /// <summary>
    /// Interaction logic for AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : Window, ICloseable
    {
        public AddOrderView(AddOrderVM addOrderVM)
        {
            InitializeComponent();
            DataContext = addOrderVM;
            addOrderVM.Closeable = this;
            Owner = App.Current.MainWindow;
        }
    }
}
