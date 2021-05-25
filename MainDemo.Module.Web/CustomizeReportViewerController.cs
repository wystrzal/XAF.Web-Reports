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
    public class CustomizeReportViewerController : ViewController<DetailView>
    {
        public CustomizeReportViewerController()
        {
            TargetViewId = ReportsAspNetModuleV2.ReportViewDetailViewWebName;
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
            ReportStorage.GetInstance(SecuritySystem.CurrentUserName).Application = Application;
            var item = reportWebViewerDetailItem;
            item.ReportViewer.ClientSideEvents.CustomizeMenuActions = "onCustomizeMenuActions";
            item.ReportViewer.ClientSideEvents.CustomizeMenuActions = @"function(s,e) {
                e.Actions.push({  
                    text: 'Wyślij dokument',  
                    imageClassName: 'custom-image',  
                    disabled: ko.observable(false),  
                    visible: true,
                    hasSeparator: true,
                    clickAction: function() { s.PerformCustomDocumentOperation('" + SecuritySystem.CurrentUserName + "'); }});}";
        }
    }
}
