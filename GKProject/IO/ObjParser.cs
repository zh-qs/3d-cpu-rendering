using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using System.Globalization;
using GKProject.Drawing;

namespace GKProject.IO
{
    public static class ObjParser
    {
        public static Dictionary<string, Solid> ParseMeshes(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int i = 0;

            Dictionary<string, Solid> solids = new Dictionary<string, Solid>();
            Dictionary<string, Material> materialDictionary = null;

            

            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;

            while (lines[i][0] != 'm') i++;
            if (lines[i][0] == 'm') //mtllib
            {
                string[] split = lines[i].Split(' ');
                materialDictionary = ParseMaterials(filename.Substring(0, filename.LastIndexOf('\\') + 1) + split[1]);
                i++;
            }

            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();

            while (i < lines.Length)
            {
                while (i < lines.Length && lines[i][0] != 'o') i++;
                if (i < lines.Length && lines[i][0] == 'o')
                {
                    string solidName = lines[i].Split(' ')[1];
                    i++;

                    List<Triangle> faces = new List<Triangle>();

                    string materialName = "";

                    while (lines[i][0] != 'v') i++;
                    while (lines[i][0] == 'v' && lines[i][1] != 'n')
                    {
                        string[] split = lines[i].Split(' ');
                        vertices.Add(new Vector3(float.Parse(split[1], nfi), float.Parse(split[2], nfi), float.Parse(split[3], nfi)));
                        i++;
                    }

                    Vector3[] vArray = vertices.ToArray();

                    while (lines[i][0] != 'v') i++;
                    while (lines[i][0] == 'v' && lines[i][1] == 'n')
                    {
                        string[] split = lines[i].Split(' ');
                        normals.Add(new Vector3(float.Parse(split[1], nfi), float.Parse(split[2], nfi), float.Parse(split[3], nfi)));
                        i++;
                    }

                    Vector3[] vnArray = normals.ToArray();

                    for (int j=0;j<vnArray.Length;++j)
                    {
                        vnArray[j] = Vector3.Normalize(vnArray[j]);
                    }

                    while (lines[i][0] != 'f' && lines[i][0] != 'u') i++;
                    while (i < lines.Length && (lines[i][0] == 'f' || lines[i][0] == 'u'))
                    {
                        if (lines[i][0] == 'u') // usemtl
                        {
                            materialName = lines[i].Split(' ')[1];
                            i++;
                            while (lines[i][0] != 'f' && lines[i][0] != 'u') i++;
                        }
                        else
                        {
                            string[] split = lines[i].Split(' ', '/');
                            faces.Add(new Triangle(vArray[int.Parse(split[1]) - 1], vArray[int.Parse(split[4]) - 1], vArray[int.Parse(split[7]) - 1],
                                vnArray[int.Parse(split[3]) - 1], vnArray[int.Parse(split[6]) - 1], vnArray[int.Parse(split[9]) - 1], materialDictionary[materialName]));
                            i++;
                        }
                    }
                    solids.Add(solidName, new Mesh(faces));
                }
            }
            return solids;
        }

        public static Dictionary<string, Material> ParseMaterials(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int i = 0;
            var dict = new Dictionary<string, Material>();

            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;

            while (i < lines.Length)
            {
                while (i < lines.Length && (lines[i].Length == 0 || lines[i][0] != 'n')) i++;
                if (i < lines.Length && lines[i].Length > 0 && lines[i][0] == 'n') // newmtl
                {
                    string materialName = lines[i].Split(' ')[1];
                    i++;

                    Material material = new Material();
                    while (i < lines.Length && (lines[i].Length == 0 || lines[i][0] != 'n'))
                    {
                        if (lines[i].Length == 0)
                        {
                            i++;
                            continue;
                        }
                        string[] split = lines[i].Split(' ');
                        switch (split[0])
                        {
                            case "Ns":
                                material.Ns = float.Parse(split[1], nfi);
                                break;
                            case "Ka":
                                material.Ka = new Vector3(float.Parse(split[1], nfi), float.Parse(split[2], nfi), float.Parse(split[3], nfi));
                                break;
                            case "Kd":
                                material.Kd = new Vector3(float.Parse(split[1], nfi), float.Parse(split[2], nfi), float.Parse(split[3], nfi));
                                break;
                            case "Ks":
                                material.Ks = new Vector3(float.Parse(split[1], nfi), float.Parse(split[2], nfi), float.Parse(split[3], nfi));
                                break;
                        }
                        i++;
                    }
                    dict.Add(materialName, material);
                }

            }
            return dict;
        }
    }
}
