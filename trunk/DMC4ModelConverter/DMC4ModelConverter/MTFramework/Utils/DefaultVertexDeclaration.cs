using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMC4ModelConverter.Types.Shorts;
using DMC4ModelConverter.Types.Bytes;

namespace DMC4ModelConverter.MTFramework.Utils
{
    public struct DefaultVertexDeclaration
    {
        //First 16 bytes
        private short4 position;
        public short4 Position { get{return position;} set{position = value;} }
        private byte4 bones;
        public byte4 Bones { get{return bones;} set{bones = value;} }
        private byte4 boneweights;
        public byte4 BoneWeights { get{return boneweights;} set{boneweights = value;} }
        //Second 16 bytes
        private byte4 normals;
        public byte4 Normals { get{return normals;} set{normals = value;} }
        private byte4 tangents;
        public byte4 Tangents { get{return tangents;} set{tangents = value;} }
        private short2 texcoord0;
        public short2 TEXCOORD0 { get{return texcoord0;} set{texcoord0 = value;} }
        private short2 texcoord1;
        public short2 TEXCOORD1 { get{return texcoord1;} set{texcoord1 = value;} }

        public DefaultVertexDeclaration(short4 position,byte4 bones,byte4 boneweights,byte4 normals,byte4 tangents,short2 texcoord0,short2 texcoord1)
        {
            this.position = position;
            this.bones = bones;
            this.boneweights = boneweights;
            this.normals = normals;
            this.tangents = tangents;
            this.texcoord0 = texcoord0;
            this.texcoord1 = texcoord1;
        }


        //Handlers
        public static DefaultVertexDeclaration FromByteCode(byte[] data)
        {
            short4 position = new short4(BitConverter.ToInt16(data,0),
                    BitConverter.ToInt16(data,2),
                    BitConverter.ToInt16(data,4),
                    BitConverter.ToInt16(data,6)
                );
            byte4 bones = new byte4(data[8],
                    data[9],
                    data[10],
                    data[11]);
            byte4 boneweights = new byte4(data[12],
                    data[13],
                    data[14],
                    data[15]);
            byte4 normals = new byte4(data[16],
                    data[17],
                    data[18],
                    data[19]);
            byte4 tangents = new byte4(data[20],
                    data[21],
                    data[22],
                    data[23]);
            short2 texcoord0 = new short2(BitConverter.ToInt16(data, 24),
                BitConverter.ToInt16(data, 26));
            short2 texcoord1 = new short2(BitConverter.ToInt16(data, 28),
                BitConverter.ToInt16(data, 30));

            DefaultVertexDeclaration vertexDeclaration = new DefaultVertexDeclaration(position,bones,boneweights,normals,tangents,texcoord0,texcoord1);
            return vertexDeclaration;
        }

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
