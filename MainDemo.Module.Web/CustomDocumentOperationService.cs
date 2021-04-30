using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDemo.Module.Web
{
    public class CustomDocumentOperationService : DocumentOperationService
    {
        public override bool CanPerformOperation(DocumentOperationRequest request)
        {
            return true;
        }

        public override DocumentOperationResponse PerformOperation(DocumentOperationRequest request, PrintingSystemBase initialPrintingSystem, PrintingSystemBase printingSystemWithEditingFields)
        {
            string reportPath = @"c:\\Temp\Test.pdf";
            printingSystemWithEditingFields.ExportToPdf(reportPath);
            return base.PerformOperation(request, initialPrintingSystem, printingSystemWithEditingFields);
        }
    }
}
