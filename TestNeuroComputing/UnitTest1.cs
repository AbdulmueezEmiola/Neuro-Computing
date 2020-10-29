using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Numerics;
namespace TestNeuroComputing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTransformPoints()
        {
            Vector3[] vectors = new Vector3[2];
            vectors[0] = new Vector3(1, 0, 0);
            vectors[1] = new Vector3(0, 1, 1);
            var points = TransformPoint.transformPoints(vectors, (float)Math.PI, (float)Math.PI / 3, (float)Math.PI / 2, 10,1,12);
            Assert.IsNotNull(points);
        }

        [TestMethod]
        public void TestGradientDescent()
        {
            Vector3[] vectors = new Vector3[2];
            Vector3[] vectors2 = new Vector3[2];
            var rand = new Random();
            for(int i = 0; i < 2; i++)
            {
                vectors[i] = new Vector3(rand.Next(10, 50), rand.Next(10, 50), rand.Next(10, 50));
                vectors2[i] = new Vector3(rand.Next(50, 100), rand.Next(50, 100), rand.Next(50, 100));
            }
            GradientDescent gradient = new GradientDescent(vectors, vectors2);
            //var values = gradient.calculateGradientDescent(0.0001f,1000000);
            //Assert.IsNotNull(values);
        }
        [TestMethod]
        public void MainFunctionToRun()
        {
            var values = FileToArray.Converter(@"C:\Users\emiol\Desktop\2nd year Assignment\Neuro Computing\Neuro Computing\TestNeuroComputing\TextFile1.txt");
            var values2 = FileToArray.Converter(@"C:\Users\emiol\Desktop\2nd year Assignment\Neuro Computing\Neuro Computing\TestNeuroComputing\TextFile2.txt");
            var resultPath = @"C:\Users\emiol\Desktop\2nd year Assignment\Neuro Computing\Neuro Computing\TestNeuroComputing\Result.txt";
            RelateTwoArrays relateTwoArrays = new RelateTwoArrays();
            relateTwoArrays.RelateTwoTuples(values.ToList(), values2.ToList());
            GradientDescent gradientDescent = new GradientDescent(relateTwoArrays.firstVector.ToArray(), relateTwoArrays.secondVector.ToArray());
            gradientDescent.calculateGradientDescent(0.0001f,350);
            var result = gradientDescent.result;
            using(StreamWriter writer = new StreamWriter(resultPath))
            {
                for (int i = 0; i < result.Count; i++)
                {
                    string value = (i + 1) + ". " + result.ElementAt(i).Item1 + " (" + result.ElementAt(i).Item2.alpha + "; " + result.ElementAt(i).Item2.beta + "; " +
                            result.ElementAt(i).Item2.gamma + "; " + result.ElementAt(i).Item2.deltaX+ "; " 
                            + result.ElementAt(i).Item2.deltaY + "; " + result.ElementAt(i).Item2.deltaZ +")";
                    writer.WriteLine(value);
                }
            }
        }
    }
}
