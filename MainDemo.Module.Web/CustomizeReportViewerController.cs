using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ReportsV2.Web;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDemo.Module.Web
{
    public class CustomizeReportViewerController : ViewController<DetailView>, IXafCallbackHandler
    {
        public CustomizeReportViewerController()
        {
            TargetViewId = ReportsAspNetModuleV2.ReportViewDetailViewWebName;
        }

        public void ProcessAction(string parameter)
        {
            var item = View.GetItems<ReportWebViewerDetailItem>()[0];
            if (item != null)
            {
                var currentReport = (XtraReport)item.GetType().GetField("report", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(item);
                string reportPath = @"c:\\Temp\Test.pdf";
                currentReport.ExportToPdf(reportPath);
            }
        }

        protected override void OnActivated()
        {
            base.OnViewControlsCreated();
            if (View.Id == "ReportViewer_DetailView_V2")
            {
                var item = View.GetItems<ReportWebViewerDetailItem>()[0];
                if (item != null)
                {
                    View.CustomizeViewItemControl<ReportWebViewerDetailItem>(this, CustomizeReportViewerViewItem);
                }
            }
        }

        public void CustomizeReportViewerViewItem(ReportWebViewerDetailItem reportWebViewerDetailItem)
        {
            var item = reportWebViewerDetailItem;
            XafCallbackManager callbackManager = ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager;
            callbackManager.RegisterHandler("CustomizeReportViewerController", this);
            item.ReportViewer.ClientSideEvents.CustomizeMenuActions = @"function(s,e) {
                e.Actions.push({  
                    text: 'Email',  
                    imageClassName: 'custom-image',  
                    disabled: ko.observable(false),  
                    visible: true,
                    hasSeparator: true,
                    clickAction: function() { " + callbackManager.GetScript("CustomizeReportViewerController", "") + "}});}";
        }
    }
}
