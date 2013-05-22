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


            }
        }

        public void SaveModel(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
