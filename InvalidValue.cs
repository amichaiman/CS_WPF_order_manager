using System;
using System.Runtime.Serialization;

namespace WpfApp2
{
    [Serializable]
    internal class InvalidValue : Exception
    {
        public InvalidValue()
        {
        }

        public InvalidValue(string message) : base(message)
        {
        }

        public InvalidValue(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidValue(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}