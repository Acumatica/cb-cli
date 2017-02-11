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
                XElement FieldsElement = elt.Element(Namespace + "Fields");
                IEnumerable<XElement> Fields = FieldsElement.Elements().OrderBy(GetName).ToArray();
                foreach (XElement e in Fields) e.Remove();
                FieldsElement.Add(Fields);

                elt.Remove();
            }
            root.Add(Entities);
        }

        internal static string GetName(XElement elt)
        {
            return elt.Attribute("name").Value;
        }
    }
}
