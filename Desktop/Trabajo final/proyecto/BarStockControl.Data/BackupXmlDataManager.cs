
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BarStockControl.Data
{
    public class BackupXmlDataManager : XmlDataManager
    {
        public BackupXmlDataManager(string path) : base(path)
        {
        }
    }
}
