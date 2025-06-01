using MFormatik.Core.Models;
using MFormatik.Services.Abstracts;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MFormatik.Services
{
    public class OrderPrinter : IOrderPrinter
    {
        public void Print(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            FlowDocument doc = CreateFlowDocument(order);

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idpSource = doc;
                pd.PrintDocument(idpSource.DocumentPaginator, $"Order {order.Id}");
            }
        }

        private FlowDocument CreateFlowDocument(Order order)
        {
            var usCulture = CultureInfo.GetCultureInfo("en-US"); // دولار أمريكي

            FlowDocument doc = new FlowDocument
            {
                PagePadding = new Thickness(50),
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 12,
                ColumnWidth = double.PositiveInfinity
            };

            // Title
            Paragraph title = new Paragraph(new Run($"Order Receipt - {order.Id}"))
            {
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };
            doc.Blocks.Add(title);

            // Client Details
            doc.Blocks.Add(new Paragraph(new Run($"Client: {order.Client.FullName}")));
            doc.Blocks.Add(new Paragraph(new Run($"Order Date: {order.OrderDate:d}")));
            doc.Blocks.Add(new Paragraph(new Run(" ")));

            // Table for Order Items
            Table table = new Table
            {
                CellSpacing = 0,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1)
            };
            doc.Blocks.Add(table);

            for (int i = 0; i < 6; i++)
                table.Columns.Add(new TableColumn { Width = new GridLength(i == 0 ? 200 : 100) });

            TableRowGroup trg = new TableRowGroup();
            table.RowGroups.Add(trg);

            TableRow header = new TableRow();
            string[] headers = { "Article", "Unit Price", "Quantity", "Discount %", "Amount", "Net Amount" };
            foreach (var h in headers)
                header.Cells.Add(CreateBorderedCell(h, FontWeights.Bold));
            trg.Rows.Add(header);

            foreach (var item in order.OrderItems)
            {
                TableRow row = new TableRow();
                row.Cells.Add(CreateBorderedCell(item.Product.Name));
                row.Cells.Add(CreateBorderedCell(item.Product.UnitPrice.ToString("C", usCulture)));
                row.Cells.Add(CreateBorderedCell(item.Quantity.ToString()));
                row.Cells.Add(CreateBorderedCell(item.DiscountRate.ToString()));

                decimal total = item.Quantity * item.Product.UnitPrice;
                decimal discount = (order.DiscountRate ?? 0) != 0
                    ? total * ((decimal)order.DiscountRate.Value / 100)
                    : 0;
                decimal totalNet = total - discount;

                row.Cells.Add(CreateBorderedCell(total.ToString("C", usCulture)));
                row.Cells.Add(CreateBorderedCell(totalNet.ToString("C", usCulture)));

                trg.Rows.Add(row);
            }

            decimal remiseTotal = (decimal)(order.DiscountRate ?? 0);
            decimal orderTotal = (decimal)order.Total;
            decimal totalAmount = (decimal)order.TotalNet;

            doc.Blocks.Add(new Paragraph(new Run($"\nRemise : {remiseTotal}%"))
            {
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 5, 0, 0)
            });

            doc.Blocks.Add(new Paragraph(new Run($"\nTotal : {orderTotal.ToString("C", usCulture)}"))
            {
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 8, 0, 0)
            });

            doc.Blocks.Add(new Paragraph(new Run($"\nTotal Amount: {totalAmount.ToString("C", usCulture)}"))
            {
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Foreground = Brushes.DarkGreen,
                Margin = new Thickness(0, 0, 0, 0)
            });

            return doc;
        }

        private TableCell CreateBorderedCell(string text, FontWeight fontWeight = default)
        {
            return new TableCell(new Paragraph(new Run(text)))
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                FontWeight = fontWeight == default ? FontWeights.Normal : fontWeight,
                Padding = new Thickness(5)
            };
        }
    }
}
