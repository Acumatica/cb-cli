using Microsoft.VisualStudio.TestTools.UnitTesting;
using PX.Api.ContractBased.Maintenance.Cli.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

            IEnumerable<XElement> Entities = Doc.Elements().Single().Elements();

            string[] EntNames = Entities.Select(Sorter.GetName).ToArray();
            Assert.AreEqual("Default", EntNames[0]);
            Assert.AreEqual("ErsatzDoc", EntNames[1]);
            Assert.AreEqual("ErsatzDocInquiryResult", EntNames[2]);
            Assert.AreEqual("ExtLeadsInquiry", EntNames[3]);
            Assert.AreEqual("ExtLeadsInquiryResult", EntNames[4]);

            XElement FirstEntity = Entities.Skip(1).First();
            string[] FieldNames = FirstEntity.Element(FirstEntity.Name.Namespace + "Fields").Elements().Select(Sorter.GetName).ToArray();
            Assert.AreEqual("CommonRef", EntNames[0]);
            Assert.AreEqual("Result", EntNames[1]);
        }
    }
}
