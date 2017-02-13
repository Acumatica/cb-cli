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

            IEnumerable<XElement> Entities = root.Elements().Except(new[] { root.Element(Namespace + "ExtendsEndpoint") }).OrderBy(GetName).ToArray();

            foreach (XElement elt in Entities)
            {
                elt.Element(Namespace + "Fields").SortFields();
                elt.Element(Namespace + "Mappings")?.SortMapings();

                elt.Remove();
            }
            root.Add(Entities);
        }

        private static void SortFields(this XElement FieldsElement)
        {
            IEnumerable<XElement> Fields = FieldsElement.Elements().OrderBy(GetName).ToArray();
            foreach (XElement e in Fields) e.Remove();
            FieldsElement.Add(Fields);
        }

        internal static string GetName(XElement elt)
        {
            return elt.Attribute("name").Value;
        }

        private static void SortMapings(this XElement MappingsElement)
        {
            IEnumerable<XElement> Mappings = MappingsElement.Elements().OrderBy(GetField).ToArray();
            foreach (XElement e in Mappings)
            {
                SortMapings(e);
                e.Remove();
            }
            MappingsElement.Add(Mappings);
        }

        internal static string GetField(XElement elt)
        {
            return elt.Attribute("field").Value;
        }
    }
}
