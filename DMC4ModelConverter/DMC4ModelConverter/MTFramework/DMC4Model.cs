using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DMC4ModelConverter.MTFramework.Utils;

namespace DMC4ModelConverter.MTFramework
{
    public class DMC4Model : IModel
    {
        //const addresses
        private const int HEADADDR_ADDR_VERT_START = 0x3C;
        private const int HEADADDR_ADDR_TRI_START = 0X44;
        private int ADDR_VERT_START;
        private int ADDR_TRI_START;
        public DefaultVertexDeclaration[] Vertices { get; set; }

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

                //************
                // Parse data
                //************
                if((ADDR_VERT_START - ADDR_TRI_START)%32 == 0)
                {
                    int vertexCount = (ADDR_VERT_START - ADDR_TRI_START) / 32;
                    //Create vertices
                    Vertices = new DefaultVertexDeclaration[vertexCount];
                    for (int i = 0; i < vertexCount; i++)
                    {
                        int address = ADDR_VERT_START + i * 32;
                        DefaultVertexDeclaration vertex = DefaultVertexDeclaration.FromByteCode(data.Skip(address).Take(32).ToArray());
                        Vertices[i] = vertex;
                    }
                }

            }
        }

        public void SaveModel(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
