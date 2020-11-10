using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
namespace ClassLibrary1
{
    public static class TransformPoint
    {        

        private static Vector4 transformPoint(Vector3 vector,float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double M11 = Math.Cos(alpha) * Math.Cos(beta);
            double M12 = Math.Cos(alpha) * Math.Sin(beta) * Math.Sin(gamma) - Math.Sin(alpha) * Math.Cos(alpha);
            double M13 = Math.Cos(alpha) * Math.Sin(beta) * Math.Cos(gamma) + Math.Sin(alpha) * Math.Sin(gamma);
            double M14 = deltaX;
            double M21 = Math.Sin(alpha) * Math.Cos(beta);
            double M22 = Math.Sin(alpha) * Math.Sin(beta) * Math.Sin(gamma) + Math.Cos(alpha) * Math.Cos(gamma);
            double M23 = Math.Sin(alpha) * Math.Sin(beta) * Math.Cos(gamma) - Math.Cos(alpha) * Math.Sin(gamma);
            double M24 = deltaY;
            double M31 = -Math.Sin(beta);
            double M32 = Math.Cos(beta) * Math.Sin(gamma);
            double M33 = Math.Cos(beta) * Math.Cos(gamma);
            double M34 = deltaZ;
            double M41 = 0;
            double M42 = 0;
            double M43 = 0;
            double M44 = 1;            
            Matrix4x4 matrix = new Matrix4x4((float)M11,(float) M12,(float) M13,(float) M14, (float)M21,(float) M22,(float) M23, (float)M24,(float) M31,
                (float) M32, (float)M33, (float)M34, (float)M41, (float)M42,(float) M43, (float)M44);
            return Vector4.Transform(new Vector4(vector,1), Matrix4x4.Transpose(matrix));

        }
        public static void WriteTransformPointToFile(List<Tuple<string, Vector3>> tuple, string variablesAsString, string fileName)
        {
            var vectors = tuple.Select(x => x.Item2).ToArray();
            Variables variables = new Variables(variablesAsString);
            var results = transformPoints(vectors, (float)variables.alpha, (float)variables.beta, (float)variables.gamma, (float)variables.deltaX, (float)variables.deltaY, (float)variables.deltaZ);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                int i = 0;
                foreach(var result in results)
                {
                    string value = (++i) + ". (" + result.X + ", " + result.Y + ", " + result.Z + ")";
                    writer.WriteLine(value);
                }
            }
        }
        public static IEnumerable<Vector3> transformPoints(Vector3[] vectors,float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            foreach(Vector3 vector in vectors)
            {
                var newVector = transformPoint(vector, alpha, beta, gamma, deltaX, deltaY, deltaZ);
                yield return new Vector3(newVector.X, newVector.Y, newVector.Z);
            }
        }

    }
}
