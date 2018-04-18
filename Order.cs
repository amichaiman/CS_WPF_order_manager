using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Order
    {
        private string _orderNumber;
        private string _jobNumber;
        private string _store;
        private string _link;
        private string _trackingLink;
        private string _extraInfo;

        public string Link
        {
            get => _link;
            set
            {
                if (!Regex.IsMatch(value, @"^\S+$"))
                {
                    throw new InvalidValue("Invalid Link");
                }
                _link = value;
            }
        }
        public string TrackingLink
        {
            get => _trackingLink;
            set
            {
                if (!Regex.IsMatch(value, @"^\S+$"))
                {
                    throw new InvalidValue("Invalid Tracking Link");
                }
                _trackingLink = value;
            }
        }
        public string OrderNumber
        {
            get => _orderNumber;
            set
            {
                if (!Regex.IsMatch(value, @"^[0-9A-Z]*$"))
                {
                    throw new InvalidValue("Invalid Order Number");
                }
                _orderNumber = value;
            }
        }
        public string JobNumber
        {
            get => _jobNumber;
            set
            {
                if (!Regex.IsMatch(value, @"\d+"))
                {
                    throw new InvalidValue("Invalid Job Number");
                }
                _jobNumber = value;
            }
        }
        public string ExtraInfo
        {
            get => _extraInfo;
            set
            {
                if (!Regex.IsMatch(value, @".+"))
                {
                    throw new InvalidValue("Invalid Extra Info");
                }
                _extraInfo = value;
            }
        }
        public string Store
        {
            get => _store;
            set
            {
                if (!Regex.IsMatch(value,@".+"))
                {
                    throw new InvalidValue("Invalid Store");
                }
                _store = value;
            }
        }

        public Order(string link, string trackingLink, string orderNumber, string jobNumber, string extraInfo, string store)
        {
            Link = link;
            TrackingLink = trackingLink;
            OrderNumber = orderNumber;
            JobNumber = jobNumber;
            ExtraInfo = extraInfo;
            Store= store;
        }


        public override string ToString()
        {
            return OrderNumber;
        }


        public bool ContainsExpression(string expression)
        {
            if (Link.Contains(expression) || TrackingLink.Contains(expression) || OrderNumber.Contains(expression) || JobNumber.Contains(expression) || ExtraInfo.Contains(expression) || Store.Contains(expression))
            {
                return true;
            }
            return false;
        }
    }
}
