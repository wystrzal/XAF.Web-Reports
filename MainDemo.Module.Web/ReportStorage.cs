using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDemo.Module.Web
{
    public class ReportStorage
    {
        private ReportStorage() { }
        private static readonly Dictionary<string, ReportStorage> instance = new Dictionary<string, ReportStorage>();

        public IDokumentPdf Dokument { get; set; }
        public XafApplication Application { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public string PacjentEmail { get; set; }

        public static ReportStorage GetInstance(string userName)
        {
            if (userName == null)
            {
                return null;
            }

            if (!instance.ContainsKey(userName) || instance[userName] == null)
            {
                instance.Add(userName, new ReportStorage());
            }
            return instance[userName];
        }
    }
}
