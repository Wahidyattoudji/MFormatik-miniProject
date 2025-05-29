using MFormatik.Core.Models;
using MFormatik.Services.Abstracts;
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
            doc.Blocks.Add(new Paragraph(new Run(" "))); // Spacer

            // Table for Order Items
            Table table = new Table();
            doc.Blocks.Add(table);

            // Define columns
            table.Columns.Add(new TableColumn { Width = new GridLength(200) });
            table.Columns.Add(new TableColumn { Width = new GridLength(100) });
            table.Columns.Add(new TableColumn { Width = new GridLength(100) });
            table.Columns.Add(new TableColumn { Width = new GridLength(100) });

            // Add header row
            TableRowGroup trg = new TableRowGroup();
            table.RowGroups.Add(trg);

            TableRow header = new TableRow();
            header.Cells.Add(new TableCell(new Paragraph(new Run("Product"))) { FontWeight = FontWeights.Bold });
            header.Cells.Add(new TableCell(new Paragraph(new Run("Quantity"))) { FontWeight = FontWeights.Bold });
            header.Cells.Add(new TableCell(new Paragraph(new Run("Unit Price"))) { FontWeight = FontWeights.Bold });
            header.Cells.Add(new TableCell(new Paragraph(new Run("Total"))) { FontWeight = FontWeights.Bold });
            trg.Rows.Add(header);

            // Add order items
            foreach (var item in order.OrderItems)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.Product.Name))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.Quantity.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.UnitPrice.ToString("C")))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.Position.ToString("C")))));
                trg.Rows.Add(row);
            }

            // Total Summary
            decimal totalAmount = (decimal)order.TotalNet;
            doc.Blocks.Add(new Paragraph(new Run($"\nTotal Amount: {totalAmount:C}"))
            {
                FontWeight = FontWeights.Bold,
                FontSize = 14
            });

            // Print Dialog
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idpSource = doc;
                pd.PrintDocument(idpSource.DocumentPaginator, $"Order {order.Id}");
            }
        }
    }
}
