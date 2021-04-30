using System;

using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using MainDemo.Module.BusinessObjects;

namespace MainDemo.Module.Win.Controllers {
    public class WinDateEditCalendarController : ObjectViewController<DetailView, Contact> {
        protected override void OnActivated() {
            base.OnActivated();
            View.CustomizeViewItemControl(this, SetCalendarView, nameof(Contact.Birthday));
        }
        private void SetCalendarView(ViewItem viewItem) {
            DateEdit dateEdit = (DateEdit)viewItem.Control;
            dateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
        }
    }
}
