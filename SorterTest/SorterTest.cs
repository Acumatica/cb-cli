using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PX.Api.ContractBased.Maintenance.Cli.Utils;

namespace SorterTest
{
    [TestClass]
    public class SorterTest
    {
        [TestMethod]
        public void TetSort1()
        {
            XDocument Doc = XDocument.Load("Sample1.xml");
            Doc.Sort();

        }
    }
}
