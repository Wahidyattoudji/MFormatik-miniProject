using MFormatik.Application.MediatorService;

namespace MFormatik.ViewModels.OrderVms
{
    public class AddOrderVM : BaseOrderViewModel
    {
        private readonly IMediator _mediator;

        public AddOrderVM(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
