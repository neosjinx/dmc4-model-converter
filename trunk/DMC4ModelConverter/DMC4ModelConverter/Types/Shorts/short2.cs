using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Shorts
{
    public class short2
    {
        private short _x;
        /// <summary>
        /// First part of the short2
        /// </summary>
        public short X { get { return _x; } set { _x = value; } }

        private short _y;
        /// <summary>
        /// Second part of the short2
        /// </summary>
        public short Y { get { return _y; } set { _y = value; } }

        public short2(short x, short y)
        {
            _x = x;
            _y = y;
        }

        public byte[] GetBytes()
        {
            byte[] xs = BitConverter.GetBytes(_x);
            byte[] ys = BitConverter.GetBytes(_y);
            if(BitConverter.IsLittleEndian)
                return new byte[] { xs[0],xs[1],ys[0],ys[1] };
            return null;
        }
    }
}
