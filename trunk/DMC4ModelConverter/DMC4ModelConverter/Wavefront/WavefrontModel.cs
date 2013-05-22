using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMC4ModelConverter.MTFramework;
using System.IO;

using DMC4ModelConverter.Types.Floats;

namespace DMC4ModelConverter.Wavefront
{

    public class WavefrontModel : IModel
    {
        public float3[] Vertices { get; set; }
        public float3[] Normals { get; set; }
        public float3[] TextureCoordinates { get; set; }
        public short[] Triangles { get; set; }

        public WavefrontModel()
        {

        }

        public static WavefrontModel FromDMC4Model(DMC4Model model)
        {

            return null;
        }


        public void LoadModel(string filepath)
        {
            throw new NotImplementedException();
        }

        public void SaveModel(string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath, false))
            {
                //Print a header
                writer.WriteLine("# DMC4 Model Converter - Converted Model");
                //Print vertices
                writer.WriteLine("# List of Vertices, with (x,y,z) coordinates");
                foreach (var vertex in Vertices)
                {
                    writer.WriteLine(PrintVertex(vertex));
                }
                writer.WriteLine("");
                //Print texture coordinates
                writer.WriteLine("# Texture coordinates, in (u ,v ,w) coordinates, these will vary between 0 and 1");
                foreach (var texcoord in TextureCoordinates)
                {
                    writer.WriteLine(PrintTextureCoord(texcoord));
                }
                writer.WriteLine("");
                //Print normals
                writer.WriteLine("# Normals in (x,y,z) form");
                foreach (var normal in Normals)
                {
                    writer.WriteLine(PrintNormal(normal));
                }
                writer.WriteLine("");
                //Print triangles, a.k.a. faces
                writer.WriteLine("# Face Definitions");

                for(int i = 0; i < Triangles.Length; i+=3)
                {
                    short[] face = Triangles.Skip(i).Take(3).ToArray();
                    writer.WriteLine(PrintTriangle(face));
                }

            }
        }

        //Data to string

        public string PrintVertex(float3 vertex)
        {
            return "v " + vertex.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Z.ToString(CultureInfo.InvariantCulture);
        }
        public string PrintNormal(float3 normal)
        {
            return "vn " + normal.X.ToString(CultureInfo.InvariantCulture) + " " + normal.Y.ToString(CultureInfo.InvariantCulture) + " " + normal.Z.ToString(CultureInfo.InvariantCulture);
        }
        public string PrintTextureCoord(float3 texcoord)
        {
            return "vt " + texcoord.X.ToString(CultureInfo.InvariantCulture) + " " + texcoord.Y.ToString(CultureInfo.InvariantCulture) + " " + texcoord.Z.ToString(CultureInfo.InvariantCulture);
        }
        public string PrintTriangle(short[] triangle)
        {
            if (triangle.Length == 3)
            {
                string printedTriangle = "f";
                foreach (var index in triangle)
                {
                    printedTriangle += " " + index + @"/" + index + @"/" + index; // v/vt/vn -- should be the same, might change in the future
                }
                return printedTriangle;
            }
            return null;
        }

    }
}
