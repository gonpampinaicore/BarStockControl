using BarStockControl.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using QuestPDF.Previewer;
using System.IO;

namespace BarStockControl.Forms.Invoices
{
    public partial class InvoiceForm : Form
    {
        private readonly InvoiceDto _invoice;

        public InvoiceForm(InvoiceDto invoice)
        {
            InitializeComponent();
            _invoice = invoice;
            LoadInvoice();
        }

        private void LoadInvoice()
        {
            lblOrderId.Text = $"NÚMERO DE ORDEN: {_invoice.OrderId}";
            lblEvent.Text = _invoice.EventName;
            lblCashier.Text = _invoice.CashierName;
            lblCashRegister.Text = _invoice.CashRegisterName;
            lblDate.Text = _invoice.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            lblPayment.Text = _invoice.PaymentMethod;
            lblStatus.Text = _invoice.Status;
            lblTotal.Text = _invoice.Total.ToString("C2");

            foreach (var item in _invoice.Items)
            {
                dgvItems.Rows.Add(
                    item.DrinkName,
                    item.Quantity,
                    item.UnitPrice.ToString("C2"),
                    item.Discount.ToString("C2"),
                    item.Subtotal.ToString("C2")
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrintPdf_Click(object sender, EventArgs e)
        {
            try
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    sfd.Title = "Guardar factura como PDF";
                    sfd.FileName = $"Factura_{_invoice.OrderId}_{_invoice.CreatedAt:yyyyMMddHHmm}.pdf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GeneratePdfQuestPdf(sfd.FileName);
                        MessageBox.Show("Factura guardada como PDF correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups, algo salió mal. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GeneratePdfQuestPdf(string filePath)
        {
            var invoice = _invoice;
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.Header().Column(col =>
                    {
                        col.Item().Text($"NÚMERO DE ORDEN: {invoice.OrderId}").FontSize(22).Bold().AlignCenter();
                        col.Item().Text("Factura").FontSize(20).Bold().AlignCenter();
                    });
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Evento: {invoice.EventName}");
                        col.Item().Text($"Cajero: {invoice.CashierName}");
                        col.Item().Text($"Caja: {invoice.CashRegisterName}");
                        col.Item().Text($"Fecha: {invoice.CreatedAt:dd/MM/yyyy HH:mm}");
                        col.Item().Text($"Pago: {invoice.PaymentMethod}");
                        col.Item().Text($"Estado: {invoice.Status}");
                        col.Item().PaddingVertical(10);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });
                            table.Header(header =>
                            {
                                header.Cell().Element(c => CellStyle(c)).Text("Trago").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Cantidad").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Precio Unitario").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Descuento").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Subtotal").Bold();
                            });
                            foreach (var item in invoice.Items)
                            {
                                table.Cell().Element(c => CellStyle(c)).Text(item.DrinkName);
                                table.Cell().Element(c => CellStyle(c)).Text(item.Quantity.ToString());
                                table.Cell().Element(c => CellStyle(c)).Text(item.UnitPrice.ToString("C2"));
                                table.Cell().Element(c => CellStyle(c)).Text(item.Discount.ToString("C2"));
                                table.Cell().Element(c => CellStyle(c)).Text(item.Subtotal.ToString("C2"));
                            }
                        });
                        col.Item().PaddingTop(10).AlignRight().Text($"Total: {invoice.Total.ToString("C2")}").FontSize(14).Bold();
                    });
                });
            }).GeneratePdf(filePath);
        }

        private QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
        {
            return container.Padding(2).Border(1).BorderColor("#DDD");
        }
    }
}
