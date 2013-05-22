using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMC4ModelConverter.Types.Shorts;
using DMC4ModelConverter.Types.Bytes;

namespace DMC4ModelConverter.MTFramework.Utils
{
    public class DefaultVertexDeclaration
    {
        //First 16 bytes
        public short4 Position { get; set; }
        public byte4 Bones { get; set; }
        public byte4 BoneWeights { get; set; }
        //Second 16 bytes
        public byte4 Normals { get; set; }
        public byte4 Tangents { get; set; }
        public short2 TEXCOORD0 { get; set; }
        public short2 TEXCOORD1 { get; set; }


        //Handlers
        public byte[] ToMODFormat()
        {
            //Get bytes of all components
            byte[] pos = Position.GetBytes();
            byte[] bones = Bones.GetBytes();
            byte[] bonews = BoneWeights.GetBytes();
            byte[] norms = Normals.GetBytes();
            byte[] tangs = Tangents.GetBytes();
            byte[] uv0s = TEXCOORD0.GetBytes();
            byte[] uv1s = TEXCOORD1.GetBytes();

            //Combine all component data
            byte[] vert = new byte[32];
            System.Buffer.BlockCopy(pos, 0, vert, 0, 8);
            System.Buffer.BlockCopy(bones, 0, vert, 8, 4);
            System.Buffer.BlockCopy(bonews, 0, vert, 12, 4);
            System.Buffer.BlockCopy(norms, 0, vert, 16, 4);
            System.Buffer.BlockCopy(tangs, 0, vert, 20, 4);
            System.Buffer.BlockCopy(uv0s, 0, vert, 24, 4);
            System.Buffer.BlockCopy(uv1s, 0, vert, 28, 4);

            return vert;
        }
    }
}
