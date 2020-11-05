using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ClassLibrary1
{
    public class GradientDescent
    {
        public Vector3[] PointsA { get; set; }
        public Vector3[] PointsB { get; set; }

        public List<Tuple<double,Variables>> result { get; set; }
        private Functions functions { get; set; }
        public GradientDescent(Vector3[] pointsA, Vector3[] pointsB)
        {
            PointsA = pointsA;
            PointsB = pointsB;
            functions = new Functions(pointsA, pointsB);
            result = new List<Tuple<double, Variables>>();
        }
        public void calculateGradientDescent(double step, double distanceToCheck, double count)
        {
            Variables initial = new Variables()
            {
                alpha = 0,
                beta = 0,
                gamma = 0,
                deltaX = 0,
                deltaY = 0,
                deltaZ = 0
            };
            double valueCheck = 0;
            for(int i = 0; i < count; i++)
            {

                Variables temp = functions.CalculateGradient((float)initial.alpha, (float)initial.beta, (float)initial.gamma,(float) initial.deltaX, (float)initial.deltaY, (float)initial.deltaZ);
                Variables final = initial + (-1 * step) * temp;
                valueCheck = functions.mainFunction((float)initial.alpha, (float)initial.beta, (float)initial.gamma, (float)initial.deltaX, (float)initial.deltaY, (float)initial.deltaZ);
                if (valueCheck < distanceToCheck)
                {
                    result.Add(new Tuple<double, Variables>(valueCheck, final));
                    //Console.WriteLine(valueCheck);
                }
                initial = final;
            }
        }
    }
}
