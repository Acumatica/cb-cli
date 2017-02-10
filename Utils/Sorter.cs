using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PX.Api.ContractBased.Maintenance.Cli.Utils
{
    static class Sorter
    {
        public static void Sort(this XDocument original)
        {
            XElement root = original.Elements().Single();
            XNamespace Namespace = root.Name.Namespace;

            List<XElement> Entities = root.Elements().Except(new[] { root.Element(Namespace + "ExtendsEndpoint") }).OrderBy(GetName).ToList();
            foreach (XElement elt in Entities) elt.Remove();
            root.Add(Entities);
        }

        private static string GetName(XElement elt)
        {
            return elt.Attribute("name").Value;
        }
    }
}
