using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ReportsV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDemo.Module.Web
{
    public class ShowReportController : ViewController
    {
        private PrintSelectionBaseController printSelectionBaseController;

        protected override void OnActivated()
        {
            base.OnActivated();

            printSelectionBaseController = Frame.GetController<PrintSelectionBaseController>();
            if (printSelectionBaseController != null)
            {
                printSelectionBaseController.ShowInReportAction.Execute += ShowInReportAction_Execute;
            }
        }

        private void ShowInReportAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var reportStorage = ReportStorage.GetInstance(SecuritySystem.CurrentUserName);
            reportStorage.Dokument = e.SelectedObjects[0] as IDokumentPdf;
            reportStorage.PacjentEmail = "Email";
        }

        protected override void OnDeactivated()
        {
            printSelectionBaseController = Frame.GetController<PrintSelectionBaseController>();
            if (printSelectionBaseController != null)
            {
                printSelectionBaseController.ShowInReportAction.Execute -= ShowInReportAction_Execute;
            }
            base.OnDeactivated();
        }
    }
}
