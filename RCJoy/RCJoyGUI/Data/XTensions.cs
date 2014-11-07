using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tahorg.RCJoyGUI
{
    public static class XTensions
    {
        public static string AttributeValue(this XElement el, string name)
        {
            var att = el.Attribute(name);
            return att == null ? null : att.Value;
        }

        public static void WriteStreamLE(this Stream __out, short val)
        {
            __out.WriteByte((byte) (val & 0xFF));
            __out.WriteByte((byte) ((val >> 8) & 0xFF));
        }

        public static void WriteStreamLE(this Stream __out, ushort val)
        {
            __out.WriteByte((byte) (val & 0xFF));
            __out.WriteByte((byte) ((val >> 8) & 0xFF));
        }

        public static void WriteStreamLE(this Stream __out, int val)
        {
            __out.WriteByte((byte) (val & 0xFF));
            __out.WriteByte((byte) ((val >> 8) & 0xFF));
            __out.WriteByte((byte) ((val >> 16) & 0xFF));
            __out.WriteByte((byte) ((val >> 24) & 0xFF));
        }

        public static void WriteStreamLE(this Stream __out, uint val)
        {
            __out.WriteByte((byte) (val & 0xFF));
            __out.WriteByte((byte) ((val >> 8) & 0xFF));
            __out.WriteByte((byte) ((val >> 16) & 0xFF));
            __out.WriteByte((byte) ((val >> 24) & 0xFF));
        }

        public static void WriteStreamByteString(this Stream __out, string val)
        {
            byte[] vals = Encoding.ASCII.GetBytes(val);

            __out.Write(vals, 0, val.Length);
            __out.WriteByte(0);
        }
    }
}
