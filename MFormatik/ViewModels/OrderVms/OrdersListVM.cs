using CommunityToolkit.Mvvm.Input;
using MFormatik.Application.Helpers;
using MFormatik.Application.MediatorService;
using MFormatik.Core.Models;
using MFormatik.Helpers;
using MFormatik.Services.Abstracts;
using MFormatik.Views.OrderViews;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace MFormatik.ViewModels.OrderVms
{
    public class OrdersListVM : BaseOrderViewModel
    {
        private readonly IMediator _mediator;
        private readonly Lazy<Task> _initializeTask;

        #region Properties
        private Order _selectedOrder;
        private ObservableCollection<Order> _ordersList;
        private string? _searchText;
        private readonly DispatcherTimer _searchTimer;
        #endregion

        #region Fields
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Order> OrdersList
        {
            get => _ordersList;
            set
            {
                if (OrdersList != value)
                {
                    _ordersList = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SearchText
        {
            get => _searchText ?? "";
            set
            {
                if (SearchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    _searchTimer.Stop();  // The Timer Stop because the user has write a letter
                    _searchTimer.Start(); // The Timer Start because the user Stop writing
                }
            }
        }

        #endregion

        #region Commands
        public ICommand OpenAddOrederCommand { get; }
        public ICommand PrintOrderCommand { get; }
        public ICommand DeleteOrderCommand { get; }
        public ICommand DateFiltringCommand { get; }
        public ICommand GroupByFiltringCommand { get; }

        public ICommand SearchCommand { get; }
        public ICommand ReloadCommand { get; }
        #endregion

        #region UI 
        private bool _emptyDataGridMsg;
        public bool ShowEmptyDataGridMsg
        {
            get { return _emptyDataGridMsg; }
            set
            {
                _emptyDataGridMsg = value;
                OnPropertyChanged();
            }
        }
        private int _totalOrders;
        public int TotalOrders
        {
            get { return _totalOrders; }
            set
            {
                _totalOrders = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DataFeltring
        private string _orderByOption;
        public string OrderByOption
        {
            get => _orderByOption;
            set
            {
                if (_orderByOption == value) return;
                _orderByOption = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate ?? DateTime.Now.Date;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    if (value < StartDate)
                    {
                        MsgHelper.ShowError("La date de fin ne peut pas être antérieure à la date de début.", "Wrong Date");
                        _endDate = StartDate.Value.AddDays(1);
                        return;
                    }
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public OrdersListVM(IMediator mediator)
        {
            _mediator = mediator;
            OpenAddOrederCommand = new RelayCommand(OpenAddOrderWindow);
            DeleteOrderCommand = new RelayCommand(DeleteOrde);
            PrintOrderCommand = new RelayCommand(PrintOrder);
            DateFiltringCommand = new RelayCommand(DateFiltring);
            GroupByFiltringCommand = new RelayCommand(GroupByFiltring);
            SearchCommand = new RelayCommand(SearchOrder);
            ReloadCommand = new RelayCommand(() => Reload(null));
            #region Search Command Initial
            _searchTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(800)
            };
            _searchTimer.Tick += SearchTimer_Tick;
            #endregion
            _mediator.Subscribe("ReloadOrdersList", Reload);
            _initializeTask = new Lazy<Task>(() => LoadDataAsync());
        }

        private async void Reload(object obj)
        {
            await LoadDataAsync();
        }

        private void OpenAddOrderWindow()
        {
            bool isWindowOpen = App.Current.Windows.OfType<AddOrderView>().Any();
            if (!isWindowOpen)
            {
                var addOrderView = App.ServiceProvider.GetRequiredService<AddOrderView>();
                addOrderView.Show();
            }
        }

        private async void DeleteOrde()
        {
            if (SelectedOrder == null)
            {
                MsgHelper.ShowNoSelectionError("Commande");
            }
            if (MsgHelper.DeleteConfirmation("Commande"))
            {
                await _mediator.OrderService.DeleteOrderAsync(SelectedOrder);
                await LoadDataAsync();
            }
        }

        private async void GroupByFiltring()
        {
            if (OrderByOption == null) return;

            switch (OrderByOption)
            {
                case "Client":
                    OrdersList = await _mediator.OrderService.GroupByClientAsync();
                    break;
                case "Date de Commande":
                    OrdersList = await _mediator.OrderService.GroupByOrderDateAsync();
                    break;
                case "Total Net":
                    OrdersList = await _mediator.OrderService.GroupByTotalNetAsync();
                    break;
                default:
                    Reload(null);
                    return;
            }
            ShowEmptyDataGridMsg = DataGridHelper.IsEmpty(OrdersList);
        }

        private async void DateFiltring()
        {
            if (EndDate == null) return;

            if (StartDate <= EndDate)
            {
                OrdersList = await _mediator.OrderService.FilterByDateAsync(StartDate.Value, EndDate.Value);
                ShowEmptyDataGridMsg = DataGridHelper.IsEmpty(OrdersList);
            }
            else
            {
                MsgHelper.ShowError("La date de fin ne peut pas être antérieure à la date de début.", "Wrong Date");
            }
        }

        private void PrintOrder()
        {
            if (SelectedOrder == null)
            {
                MsgHelper.ShowNoSelectionError("Commande");
                return;
            }
            var printer = App.ServiceProvider.GetRequiredService<IOrderPrinter>();
            printer.Print(SelectedOrder);
        }

        private async void SearchOrder()
        {
            string SearchItem = SearchText;
            OrdersList = await _mediator.OrderService.SearchOrdersAsync(SearchItem);
            ShowEmptyDataGridMsg = DataGridHelper.IsEmpty(OrdersList);
        }

        private async Task LoadDataAsync()
        {
            OrdersList = await _mediator.OrderService.GetAllOrdersAsync();
            ShowEmptyDataGridMsg = DataGridHelper.IsEmpty(OrdersList);
            TotalOrders = OrdersList.Count;
        }

        public async Task EnsureDataLoadedAsync() => await _initializeTask.Value;

        private void SearchTimer_Tick(object? sender, EventArgs e)
        {
            _searchTimer.Stop();
            SearchOrder();
        }
    }
}
