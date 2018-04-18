using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApp2
{
    class XmlManager
    {
        public string Filename { get; set; }
        public XmlManager(string file)
        {
            Filename = file;
        }
        public void Read(SortedList<string, Order> Orders)
        {

            XmlTextReader reader = new XmlTextReader(Filename);
            reader.WhitespaceHandling = WhitespaceHandling.None;

            while (reader.Name != "Order")
            {
                reader.Read();
            }

            while (reader.Name == "Order")
            {
                reader.ReadStartElement("Order");
                string link = reader.ReadElementString("Link");
                string trackingLink = reader.ReadElementString("TrackingLink");
                string orderNumber = reader.ReadElementString("OrderNumber");
                string jobNumber = reader.ReadElementString("JobNumber");
                string extraInfo = reader.ReadElementString("ExtraInfo");
                string store = reader.ReadElementString("Store");

                Order order = new Order(link, trackingLink, orderNumber, jobNumber, extraInfo, store);

                Orders.Add(order.OrderNumber, order);
                reader.ReadEndElement();
            }
            reader.Close();
        }

        public void Write(SortedList<string, Order> Orders)
        {
            XmlTextWriter writer = new XmlTextWriter(Filename, Encoding.Unicode);
            writer.Formatting = Formatting.Indented;

            // start the document
            writer.WriteStartDocument();


            // write the data
            foreach (Order order in Orders.Values)
            {
                writer.WriteStartElement("Order");
                writer.WriteElementString("Link", order.Link);
                writer.WriteElementString("TrackingLink", order.Link);
                writer.WriteElementString("OrderNumber", order.OrderNumber);
                writer.WriteElementString("JobNumber", order.JobNumber);
                writer.WriteElementString("ExtraInfo", order.ExtraInfo);
                writer.WriteElementString("Store", order.Store);

                writer.WriteEndElement();
            }
            // end the document
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
