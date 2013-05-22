using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Shorts
{
    public class short4
    {
        private short _x;
        /// <summary>
        /// First part of the short4
        /// </summary>
        public short X { get { return _x; } set { _x = value; } }

        private short _y;
        /// <summary>
        /// Second part of the short4
        /// </summary>
        public short Y { get { return _y; } set { _y = value; } }

        private short _z;
        /// <summary>
        /// Third part of the short4
        /// </summary>
        public short Z { get { return _z; } set { _z = value; } }

        private short _w;
        /// <summary>
        /// Fourth part of the short4
        /// </summary>
        public short W { get { return _w; } set { _w = value; } }

        public short4(short x, short y, short z, short w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        public byte[] GetBytes()
        {
            byte[] xs = BitConverter.GetBytes(_x);
            byte[] ys = BitConverter.GetBytes(_y);
            byte[] zs = BitConverter.GetBytes(_z);
            byte[] ws = BitConverter.GetBytes(_w);
            if (BitConverter.IsLittleEndian)
                return new byte[] { xs[0], xs[1], ys[0], ys[1], zs[0], zs[1], ws[0], ws[1] };
            return null;
        }
    }
}
