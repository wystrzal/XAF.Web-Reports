using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts;
using System;
using System.Collections.Generic;
using System.IO;
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

        private IObjectSpace objectSpace;
        private string userName;

        public override DocumentOperationResponse PerformOperation(DocumentOperationRequest request, PrintingSystemBase initialPrintingSystem, PrintingSystemBase printingSystemWithEditingFields)
        {
            userName = request.CustomData;

            var reportStorage = ReportStorage.GetInstance(userName);
            objectSpace = reportStorage.Application.CreateObjectSpace();

            var dokumentPdf = objectSpace.GetObject(reportStorage.Dokument);
            var memoryStream = new MemoryStream();
            printingSystemWithEditingFields.ExportToPdf(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            reportStorage.MemoryStream = memoryStream;

            if (memoryStream != null)
            {
                if (memoryStream.Length > 0)
                {
                    if (dokumentPdf.DokumentPdf is null)
                    {
                        dokumentPdf.DokumentPdf = objectSpace.CreateObject<FileData>();
                    }
                    dokumentPdf.DokumentPdf.LoadFromStream($"Dokument.Pdf", memoryStream);

                    //var emailService = new EmailService("email", "password");
                    //emailService.SendEmail(memoryStream, dokumentPdf.PacjentEmail);
                    objectSpace.CommitChanges();
                }
            }

            return base.PerformOperation(request, initialPrintingSystem, printingSystemWithEditingFields);
        }
    }
}
