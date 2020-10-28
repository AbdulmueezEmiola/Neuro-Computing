using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            var values = gradient.calculateGradientDescent(0.0001f, 0.000000000001, 1000000);
            Assert.IsNotNull(values);
        }
        [TestMethod]
        public void MainFunctionToRun()
        {
            var values = FileToArray.Converter(@"C:\Users\emiol\Desktop\2nd year Assignment\Neuro Computing\Neuro Computing\TestNeuroComputing\TextFile1.txt");
            var values2 = FileToArray.Converter(@"C:\Users\emiol\Desktop\2nd year Assignment\Neuro Computing\Neuro Computing\TestNeuroComputing\TextFile2.txt");
            RelateTwoArrays relateTwoArrays = new RelateTwoArrays();
            relateTwoArrays.RelateTwoTuples(values.ToList(), values2.ToList());
            GradientDescent gradientDescent = new GradientDescent(relateTwoArrays.firstVector.ToArray(), relateTwoArrays.secondVector.ToArray());
            var variable = gradientDescent.calculateGradientDescent(0.0001f, 0.000000000001, 1000);
            var points = values.Select(x => x.Item2).ToArray();
            var transformedPoints = TransformPoint.transformPoints(points, (float)variable.alpha, (float)variable.beta, (float)variable.gamma,
                (float)variable.deltaX, (float)variable.deltaY,(float)variable.deltaZ);
        }
    }
}
