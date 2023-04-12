namespace Nistec.Printing.View
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class RTFStreamer : ISerializable
    {
        private string _var0;

        public RTFStreamer()
        {
        }

        public RTFStreamer(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                if ((entry.Name == "RTF") && (entry.Value is string))
                {
                    this._var0 = (string) entry.Value;
                    return;
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RTF", this._var0);
        }

        internal string mtd52
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }
    }
}

