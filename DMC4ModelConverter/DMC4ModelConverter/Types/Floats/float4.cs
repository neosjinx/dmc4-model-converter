using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Floats
{
    public struct float4
    {
        private float _x;
        /// <summary>
        /// First part of the float4
        /// </summary>
        public float X { get { return _x; } set { _x = value; } }

        private float _y;
        /// <summary>
        /// Second part of the float4
        /// </summary>
        public float Y { get { return _y; } set { _y = value; } }

        private float _z;
        /// <summary>
        /// Third part of the float4
        /// </summary>
        public float Z { get { return _z; } set { _z = value; } }

        private float _w;
        /// <summary>
        /// Fourth part of the float4
        /// </summary>
        public float W { get { return _w; } set { _w = value; } }

        public float4(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }
    }
}
