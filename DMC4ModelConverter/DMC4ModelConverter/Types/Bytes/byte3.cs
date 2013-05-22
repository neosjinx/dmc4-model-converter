using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Bytes
{
    public class byte3
    {
        private byte _x;
        /// <summary>
        /// First part of the byte3
        /// </summary>
        public byte X { get { return _x; } set { _x = value; } }

        private byte _y;
        /// <summary>
        /// Second part of the byte3
        /// </summary>
        public byte Y { get { return _y; } set { _y = value; } }

        private byte _z;
        /// <summary>
        /// Third part of the byte3
        /// </summary>
        public byte Z { get { return _z; } set { _z = value; } }

        public byte3(byte x, byte y,byte z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public byte[] GetBytes()
        {
            return new byte[] { _x, _y ,_z};
        }
    }
}
