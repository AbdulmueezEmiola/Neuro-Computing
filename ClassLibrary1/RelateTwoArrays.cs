using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
namespace ClassLibrary1
{
    public class RelateTwoArrays
    {
        public List<Vector3> firstVector { get; set; }
        public List<Vector3> secondVector { get; set; }
        public RelateTwoArrays()
        {
            firstVector = new List<Vector3>();
            secondVector = new List<Vector3>();
        }
        public void RelateTwoTuples(List<Tuple<string, Vector3>> tuple1, List<Tuple<string, Vector3>> tuple2)
        {
            for(int i = 0; i < tuple1.Count; i++)
            {
                var value = tuple2.Where(x => x != null).FirstOrDefault(x => x.Item1 == tuple1[i].Item1);
                if (value!=default)
                {
                    firstVector.Add(tuple1[i].Item2);
                    secondVector.Add(value.Item2);
                    tuple1[i] = null;
                    tuple2[tuple2.IndexOf(value)] = null;
                }
            }
            int count = firstVector.Count;
            //interpolateLagrangeMany(tuple1.Where(x => x != null).Select(x=>x.Item2).ToList(), firstVector, secondVector,count);
            //interpolateLagrangeMany(tuple2.Where(x => x != null).Select(x => x.Item2).ToList(), secondVector, firstVector, count);
        }
        private void interpolateLagrangeMany(List<Vector3> vectorMain, List<Vector3> vectors, List<Vector3> vectors2, int initialCount)
        {
            foreach(Vector3 vector in vectorMain)
            {
                interpolateLagrange(vector, vectors, vectors2, initialCount);
            }
        }
        private float interpolate(float x, float[]X, float[] y, int count)
        {
            float sum = 0;
            for(int i = 0; i < count; i++)
            {                
                float product = 1;
                for(int j = 0; j < count; j++)
                {
                    if(j != i)
                    {
                        product *= (x - X[j]) / (X[i] - X[j]);
                    }
                }
                sum += product * y[i];
            }
            return sum;
        }
        private void interpolateLagrange(Vector3 vector, List<Vector3> vectors, List<Vector3>vectors2, int initialCount)
        {
            float X = interpolate(vector.X, vectors.Select(x => x.X).ToArray(), vectors2.Select(x => x.X).ToArray(), initialCount);
        }
    }
}
