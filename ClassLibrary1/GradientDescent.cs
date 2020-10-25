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
        private Functions functions { get; set; }
        public GradientDescent(Vector3[] pointsA, Vector3[] pointsB)
        {
            PointsA = pointsA;
            PointsB = pointsB;
            functions = new Functions(pointsA, pointsB);
        }
        public Variables calculateGradientDescent(double step, double epsilon, double count)
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
                if(i == 0 || i == 10|| i == 100|| i == 1000 || i== 10000|| i == 100000||i == 1000000)
                {
                    valueCheck = functions.mainFunction((float)initial.alpha, (float)initial.beta, (float)initial.gamma, (float)initial.deltaX, (float)initial.deltaY, (float)initial.deltaZ);
                    //Console.WriteLine(valueCheck);
                }
                initial = final;
            }
            return initial;
        }
        public double ComputeStepSize(Variables initial, Variables final)
        {
            var difference = final+ (-1)* initial;
            var gradientInitial = functions.CalculateGradient((float)initial.alpha, (float)initial.beta,(float) initial.gamma,(float) initial.deltaX, (float)initial.deltaY,(float) initial.deltaZ);
            var gradientFinal = functions.CalculateGradient((float)final.alpha,(float) final.beta, (float)final.gamma,(float) final.deltaX,(float) final.deltaY, (float)final.deltaZ);
            var difference2 = gradientFinal + (-1) * gradientInitial;
            var product = Math.Abs(difference * difference2) / Math.Pow(difference2 * difference2,2);
            return product;
        }
    }
}
