using MFormatik.Core.Models;

namespace MFormatik.Services.Abstracts
{
    public interface IOrderPrinter
    {
        void Print(Order order);
    }
}
