using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Shorts
{
    public class short3
    {
        private short _x;
        /// <summary>
        /// First part of the short3
        /// </summary>
        public short X { get { return _x; } set { _x = value; } }

        private short _y;
        /// <summary>
        /// Second part of the short3
        /// </summary>
        public short Y { get { return _y; } set { _y = value; } }

        private short _z;
        /// <summary>
        /// Third part of the short3
        /// </summary>
        public short Z { get { return _z; } set { _z = value; } }

        public short3(byte x, byte y, byte z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public byte[] GetBytes()
        {
            byte[] xs = BitConverter.GetBytes(_x);
            byte[] ys = BitConverter.GetBytes(_y);
            byte[] zs = BitConverter.GetBytes(_z);
            if (BitConverter.IsLittleEndian)
                return new byte[] { xs[0], xs[1], ys[0], ys[1], zs[0], zs[1] };
            return null;
        }
    }
}
