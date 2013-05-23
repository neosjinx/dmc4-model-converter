using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMC4ModelConverter.MTFramework;
using DMC4ModelConverter.Interfaces;
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
            WavefrontModel convertedModel = new WavefrontModel();
            float3[] vertices = new float3[model.Vertices.Length];
            float3[] normals = new float3[model.Vertices.Length];
            float3[] texcoords = new float3[model.Vertices.Length];
            for (int i = 0; i < model.Vertices.Length; i++)
            {
                vertices[i] = new float3((float)model.Vertices[i].Position.X / (float)model.Vertices[i].Position.W,
                    (float)model.Vertices[i].Position.Y / (float)model.Vertices[i].Position.W,
                    (float)model.Vertices[i].Position.Z / (float)model.Vertices[i].Position.W);
                normals[i] = new float3((float)model.Vertices[i].Normals.X / (float)model.Vertices[i].Normals.W,
                    (float)model.Vertices[i].Normals.Y / (float)model.Vertices[i].Normals.W,
                    (float)model.Vertices[i].Normals.Z / (float)model.Vertices[i].Normals.W);
                texcoords[i] = new float3((float)model.Vertices[i].TEXCOORD0.X / (float)short.MaxValue,
                    (float)model.Vertices[i].TEXCOORD0.Y / (float)short.MaxValue,
                    0.0f);
                
            }
            short[] tris = new short[model.Triangles.Length];
            for (int i = 0; i < model.Triangles.Length; i++)
            {
                tris[i] = (short)(model.Triangles[i] + 1);//Triangle indexing in Wavefront starts from 1
            }
            //Remap vertices (denormalization)
            foreach (var vertex in vertices)
            {
                vertex.X = vertex.X * (model.MaxBoundingbox.X - model.MinBoundingbox.X) + model.MinBoundingbox.X;
                vertex.Y = vertex.Y * (model.MaxBoundingbox.Y - model.MinBoundingbox.Y) + model.MinBoundingbox.Y;
                vertex.Z = vertex.Z * (model.MaxBoundingbox.Z - model.MinBoundingbox.Z) + model.MinBoundingbox.Z;
            }

            convertedModel.Vertices = vertices;
            convertedModel.Normals = normals;
            convertedModel.TextureCoordinates = texcoords;
            convertedModel.Triangles = tris;

            return convertedModel;
        }


        public void LoadModel(string filepath)
        {
            using (StreamReader reader = new StreamReader(filepath))
            {

            }
        }

        public void SaveModel(string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath, false))
            {
                //Print a header
                writer.WriteLine("# DMC4 Model Converter - Converted Model");
                //Print vertices
                if (Vertices != null && Vertices.Length > 0)
                {
                    writer.WriteLine("# List of Vertices, with (x,y,z) coordinates");
                    foreach (var vertex in Vertices)
                    {
                        writer.WriteLine(PrintVertex(vertex));
                    }
                    writer.WriteLine("");
                }
                //Print texture coordinates
                if (TextureCoordinates != null && TextureCoordinates.Length > 0)
                {
                    writer.WriteLine("# Texture coordinates, in (u ,v ,w) coordinates, these will vary between 0 and 1");
                    foreach (var texcoord in TextureCoordinates)
                    {
                        writer.WriteLine(PrintTextureCoord(texcoord));
                    }
                    writer.WriteLine("");
                }
                //Print normals
                if (Normals != null && Normals.Length > 0)
                {
                    writer.WriteLine("# Normals in (x,y,z) form");
                    foreach (var normal in Normals)
                    {
                        writer.WriteLine(PrintNormal(normal));
                    }
                    writer.WriteLine("");
                }
                //Print triangles, a.k.a. faces
                if (Triangles != null && Triangles.Length > 0)
                {
                    writer.WriteLine("# Face Definitions");

                    for (int i = 0; i < Triangles.Length; i += 3)
                    {
                        short[] face = Triangles.Skip(i).Take(3).ToArray();
                        writer.WriteLine(PrintTriangle(face));
                    }

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
