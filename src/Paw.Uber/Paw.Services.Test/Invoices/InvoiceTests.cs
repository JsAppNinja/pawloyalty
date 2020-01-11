using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Common;
using Paw.Services.Messages;
using Paw.Services.Messages.Util.Users;
using Paw.Services.Messages.Web.Invoices;
using Paw.Services.Messages.Web.Skus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Paw.Services.Test.Invoices
{
    [TestClass]
    public class InvoiceTests
    {

        private Guid _InvoiceId = new Guid("{8ECECFC6-1B57-441D-8B0A-8F2975AAF27A}");

        [TestMethod]
        public void InvoiceSmokeTest()
        {
            // Step 1. Create invoice
            AddInvoice addInvoice = new AddInvoice() { Id = _InvoiceId, OwnerId = ProviderTestData.Wag_Weber_OwnerId, ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId };
            addInvoice.ExecuteNonQuery();

            // Step 2. Get sku from manager

            // Step 3. Add line items

            // Step 4. Fill out the questionaire

            // Step 5. 
        }


        [TestMethod]
        public void SkuTest()
        {
            // Step 1. Create Sku Full Groom
            
            var list = new GetSkuList() { ProviderId = ProviderTestData.Wag_RedwoodCity_ProviderId }.ExecuteList();
        }

        [TestMethod]
        public void NodeTest()
        {
            Guid providerId = ProviderTestData.Wag_RedwoodCity_ProviderId;
            Invoice invoice = new Invoice() { ProviderId = providerId };

            InvoiceItem p0 = new InvoiceItem() { InvoiceId = invoice.Id, Name ="Groom", Quantity = 2, Description = "Groom", Amount = 125.99m, SkuId=ProviderTestData.Wag_FullGroom1_SkuId, PetId = ProviderTestData.Wag_Smithwick_Dashee_PetId  };

            InvoiceItem p1 = new InvoiceItem() { InvoiceId = invoice.Id, ParentId = p0.Id, Name="Groom Adj", Description="Groom Adj", Quantity=2, Amount=-20.00m, SkuId = ProviderTestData.Wag_FullGroomAdjustment_SkuId };
            //InvoiceItem p2 = new InvoiceItem() { InvoiceId = invoice.Id, ParentId = p0.Id };

            //InvoiceItem p3 = new InvoiceItem() { InvoiceId = invoice.Id, ParentId = p2.Id };

            using (DataContext dataContext = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                dataContext.InvoiceSet.Add(invoice);

                dataContext.InvoiceItemSet.Add(p0);
                dataContext.InvoiceItemSet.Add(p1);
                //dataContext.InvoiceItemSet.Add(p2);
                //dataContext.InvoiceItemSet.Add(p3);

                dataContext.SaveChanges();


            }

            using (DataContext context = new DataContext())
            {
                var invoiceList = context.InvoiceItemSet.Where(x => x.InvoiceId == invoice.Id).ToList().FindAll(x => x.Parent == null);
            }

        }
    }
}
