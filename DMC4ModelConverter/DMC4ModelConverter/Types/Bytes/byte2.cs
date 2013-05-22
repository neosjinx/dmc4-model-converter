using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Bytes
{
    public class byte2
    {
        private byte _x;
        /// <summary>
        /// First part of the byte2
        /// </summary>
        public byte X { get { return _x; } set { _x = value; } }

        private byte _y;
        /// <summary>
        /// Second part of the byte2
        /// </summary>
        public byte Y { get { return _y; } set { _y = value; } }

        public byte2(byte x, byte y)
        {
            _x = x;
            _y = y;
        }

        public byte[] GetBytes()
        {
            return new byte[] { _x, _y };
        }
    }
}
