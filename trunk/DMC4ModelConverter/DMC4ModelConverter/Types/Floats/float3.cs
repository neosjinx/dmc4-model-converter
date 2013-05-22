using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Floats
{
    public class float3
    {
        private float _x;
        /// <summary>
        /// First part of the float3
        /// </summary>
        public float X { get { return _x; } set { _x = value; } }

        private float _y;
        /// <summary>
        /// Second part of the float3
        /// </summary>
        public float Y { get { return _y; } set { _y = value; } }

        private float _z;
        /// <summary>
        /// Third part of the float3
        /// </summary>
        public float Z { get { return _z; } set { _z = value; } }

        public float3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
