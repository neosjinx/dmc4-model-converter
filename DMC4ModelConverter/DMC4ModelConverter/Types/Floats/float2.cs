using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC4ModelConverter.Types.Floats
{
    public class float2
    {
        private float _x;
        /// <summary>
        /// First part of the float2
        /// </summary>
        public float X { get { return _x; } set { _x = value; } }

        private float _y;
        /// <summary>
        /// Second part of the float2
        /// </summary>
        public float Y { get { return _y; } set { _y = value; } }

        public float2(float x, float y)
        {
            _x = x;
            _y = y;
        }
    }
}
