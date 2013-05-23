using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DMC4ModelConverter.MTFramework.Utils;
using DMC4ModelConverter.Interfaces;
using DMC4ModelConverter.Types.Floats;

namespace DMC4ModelConverter.MTFramework
{
    public class DMC4Model : IModel
    {
        //const addresses
        private const int HEADADDR_ADDR_VERT_START = 0x3C;
        private const int HEADADDR_ADDR_TRI_START = 0X44;
        private const int HEADADDR_MINBB_START = 0x60;
        private const int HEADADDR_MAXBB_START = 0x70;
        private int ADDR_VERT_START;
        private int ADDR_TRI_START;
        public DefaultVertexDeclaration[] Vertices { get; set; }
        public short[] Triangles { get; set; }
        public float3 MaxBoundingbox { get; set; }
        public float3 MinBoundingbox { get; set; }
        public DMC4Model()
        {

        }

        public void LoadModel(string filepath)
        {
            byte[] data = File.ReadAllBytes(filepath);

            //Fileheader
            byte[] filetype = data.Take(3).ToArray();
            string s = Encoding.UTF8.GetString(filetype, 0, filetype.Length);
            if (s == "MOD")//This is probably the correct file
            {
                //**************
                // Parse header
                //**************
                ADDR_VERT_START = BitConverter.ToInt32(data.Skip(HEADADDR_ADDR_VERT_START).Take(4).ToArray(),0);
                ADDR_TRI_START = BitConverter.ToInt32(data.Skip(HEADADDR_ADDR_TRI_START).Take(4).ToArray(), 0);
                MinBoundingbox = new float3(BitConverter.ToSingle(data.Skip(HEADADDR_MINBB_START).Take(12).ToArray(),0),
                    BitConverter.ToSingle(data.Skip(HEADADDR_MINBB_START).Take(12).ToArray(), 4),
                    BitConverter.ToSingle(data.Skip(HEADADDR_MINBB_START).Take(12).ToArray(),8));
                MaxBoundingbox = new float3(BitConverter.ToSingle(data.Skip(HEADADDR_MAXBB_START).Take(12).ToArray(), 0),
                    BitConverter.ToSingle(data.Skip(HEADADDR_MAXBB_START).Take(12).ToArray(), 4),
                    BitConverter.ToSingle(data.Skip(HEADADDR_MAXBB_START).Take(12).ToArray(), 8));

                //************
                // Parse data
                //************
                if((ADDR_VERT_START - ADDR_TRI_START)%32 == 0)
                {
                    int vertexCount = (ADDR_TRI_START - ADDR_VERT_START) / 32;
                    //Create vertices
                    Vertices = new DefaultVertexDeclaration[vertexCount];
                    for (int i = 0; i < vertexCount; i++)
                    {
                        int address = ADDR_VERT_START + i * 32;
                        DefaultVertexDeclaration vertex = DefaultVertexDeclaration.FromByteCode(data.Skip(address).Take(32).ToArray());
                        Vertices[i] = vertex;
                    }

                    //Create triangles
                    int indexCount = (data.Length - ADDR_TRI_START) / 2;
                    short[] indices = new short[indexCount];
                    for (int i = 0; i < indexCount; i++)
                    {
                        int address = ADDR_TRI_START + i * 2;
                        short index = (short)(BitConverter.ToInt16(data, address));
                        indices[i] = index;
                    }
                    //parse indices into faces/triangles
                    List<short> faces = new List<short>();
                    bool triangleMode = true;
                    bool forward = true;
                    for (int i = 0; i < indices.Length - 2; i++)
                    {
                        if (i > 0 && indices[i - 1] == indices[i])
                            triangleMode = true;
                        if (triangleMode)
                        {
                            //Triangle mode is enabled we are currently looking at triangles
                            if (forward)
                            {
                                faces.Add(indices[i]); 
                                faces.Add(indices[i+1]);
                                faces.Add(indices[i+2]);
                            }
                            else
                            {
                                faces.Add(indices[i + 2]);
                                faces.Add(indices[i + 1]);
                                faces.Add(indices[i]);
                            }
                            forward = !forward;
                        }

                        if ((i+3)<indices.Length && indices[i + 2] == indices[i + 3]){
                            forward = true;
                            triangleMode = false;
                            i += 3;//Skip some steps, faster analysis
                        }

                    }
                    Triangles = faces.ToArray();
                }

            }
        }

        public void SaveModel(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
