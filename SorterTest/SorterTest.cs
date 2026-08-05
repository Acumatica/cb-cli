using PX.Api.ContractBased.Maintenance.Cli.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace SorterTest
{
    public class SorterTest
    {
        [Fact]
        public void TestSort1()
        {
            XDocument Doc = XDocument.Load("Sample1.xml");
            Doc.Sort();

            IEnumerable<XElement> Entities = Doc.Elements().Single().Elements();

            string[] EntNames = Entities.Select(Sorter.GetName).ToArray();
            Assert.Equal("Default", EntNames[0]);
            Assert.Equal("ErsatzDoc", EntNames[1]);
            Assert.Equal("ErsatzDocInquiryResult", EntNames[2]);
            Assert.Equal("ExtLeadsInquiry", EntNames[3]);
            Assert.Equal("ExtLeadsInquiryResult", EntNames[4]);

            XElement FirstEntity = Entities.Skip(1).First();
            string[] FieldNames = FirstEntity.Element(FirstEntity.Name.Namespace + "Fields").Elements().Select(Sorter.GetName).ToArray();
            Assert.Equal("CommonRef", FieldNames[0]);
            Assert.Equal("Result", FieldNames[1]);

            string[] MappingsFields = FirstEntity.Element(FirstEntity.Name.Namespace + "Mappings").Elements().Single().Elements().Select(Sorter.GetField).ToArray();
            Assert.Equal("CompanyName", MappingsFields[0]);
            Assert.Equal("DisplayName", MappingsFields[1]);
            Assert.Equal("Email", MappingsFields[2]);
        }
    }
}
