using ErpSystemBeniSouef.Core.Contract;
using ErpSystemBeniSouef.Core.Contract.Invoice;
using ErpSystemBeniSouef.Core.Contract.Invoice.IDamageInvoiceService;
using ErpSystemBeniSouef.Core.Contract.Invoice.ReturnSupplir;
using ErpSystemBeniSouef.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErpSystemBeniSouef.Views.Pages.InvoiceAndsupplierRegion.InvoicePages
{
    /// <summary>
    /// Interaction logic for InvoicesRegion.xaml
    /// </summary>
    public partial class InvoicesRegion : Page
    {
        private readonly int _companyNo;

        public InvoicesRegion(int companyNo)
        {
            InitializeComponent(); 
            _companyNo = companyNo;
        }


        private void Cashinvoice_Click(object sender, RoutedEventArgs e)
        {
            var supplierService = App.AppHost.Services.GetRequiredService<ISupplierService>();
            var cashInvoiceService = App.AppHost.Services.GetRequiredService<ICashInvoiceService>();

            var Dashboard = new InvoicePages.Cashinvoice( supplierService , cashInvoiceService);
            MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(Dashboard);

        }

        private void Dueinvoice_Click(object sender, RoutedEventArgs e)
        {
            //var Dashboard = new InvoicePages.Due_billsPage();
            //MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(Dashboard);

        }

        private void Return_to_supplier_Click(object sender, RoutedEventArgs e)
        {
            var supplierService = App.AppHost.Services.GetRequiredService<ISupplierService>();
            var cashInvoiceService = App.AppHost.Services.GetRequiredService<IReturnSupplierInvoiceService>();

            var Dashboard = new InvoicePages.Return_to_supplie(supplierService, cashInvoiceService);
            MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(Dashboard);


        }

        private void ruin_Click(object sender, RoutedEventArgs e)
        {

          //  var supplierService = App.AppHost.Services.GetRequiredService<ISupplierService>();
            var cashInvoiceService = App.AppHost.Services.GetRequiredService<IDamageInvoiceService>();

            var Dashboard = new InvoicePages.DamageInvoic( cashInvoiceService);
            MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(Dashboard);


        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            var Dashboard = new InvoiceAndsupplierRegion(_companyNo);
            MainWindowViewModel.MainWindow.Frame.NavigationService.Navigate(Dashboard);
        }

    }
}
