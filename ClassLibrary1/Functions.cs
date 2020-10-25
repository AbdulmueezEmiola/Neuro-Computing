using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ClassLibrary1
{
    public class Functions
    {
        public Vector3[] PointsA { get; set; }
        public Vector3[] PointsB { get; set; }

        public Functions(Vector3[] pointsA, Vector3[] pointsB)
        {
            PointsA = pointsA;
            PointsB = pointsB;
        }
        public Variables CalculateGradient(float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            Variables variables = new Variables();
            var newPoints = TransformPoint.transformPoints(PointsA, alpha, beta, gamma, deltaX, deltaY, deltaZ);
            variables.alpha = (float)gradientAlongAlpha(newPoints,alpha, beta, gamma, deltaX, deltaY, deltaZ);
            variables.beta = (float)gradientAlongBeta(newPoints, alpha, beta, gamma, deltaX, deltaY, deltaZ);
            variables.gamma = (float)gradientAlongGamma(newPoints, alpha, beta, gamma, deltaX, deltaY, deltaZ);
            variables.deltaX = (float)gradientAlongdeltaX(newPoints, alpha, beta, gamma, deltaX, deltaY, deltaZ);
            variables.deltaY = (float)gradientAlongdeltaY(newPoints, alpha, beta, gamma, deltaX, deltaY, deltaZ);
            variables.deltaZ = (float)gradientAlongdeltaZ(newPoints, alpha, beta, gamma, deltaX, deltaY, deltaZ);
            return variables;
        }
        public double mainFunction(float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double sum = 0;
            var newPoints = TransformPoint.transformPoints(PointsA, alpha, beta, gamma, deltaX, deltaY, deltaZ);
            for (int i =0; i < PointsA.Length; i++)
            {
                sum += Vector3.Distance(newPoints.ElementAt(i), PointsB[i]);
            }
            return sum;
        }

        private double gradientAlongAlpha(IEnumerable<Vector3> newPoints, float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double sum = 0;
            for (int i = 0; i < PointsA.Length; i++)
            {
                double xPart = 2 *(newPoints.ElementAt(i).X- PointsB[i].X);
                xPart *= (-PointsA[i].X * Math.Sin(alpha) * Math.Cos(beta) - PointsA[i].Y * (Math.Sin(alpha) * Math.Sin(beta) * Math.Sin(gamma) + Math.Cos(alpha) * Math.Cos(gamma))
                    - PointsA[i].Z * (Math.Sin(alpha) * Math.Sin(beta) * Math.Cos(gamma) - Math.Cos(alpha) * Math.Sin(gamma)));
                double yPart = 2 * (newPoints.ElementAt(i).Y - PointsB[i].Y);
                yPart *= (PointsA[i].X * Math.Cos(alpha) * Math.Cos(beta) + PointsA[i].Y * (Math.Cos(alpha) * Math.Sin(beta) * Math.Sin(gamma) - Math.Sin(alpha) * Math.Cos(gamma))
                    + PointsA[i].Z * (Math.Cos(alpha) * Math.Sin(beta) * Math.Cos(gamma) + Math.Sin(alpha) * Math.Sin(gamma)));
                sum += xPart + yPart;
            }
            return sum;
        }

        private double gradientAlongBeta(IEnumerable<Vector3> newPoints, float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double sum = 0;
            for(int i = 0; i < PointsA.Length; i++)
            {
                double xPart = 2 * (newPoints.ElementAt(i).X - PointsB[i].X);
                xPart *= -PointsA[i].X * Math.Cos(alpha) * Math.Sin(beta) + PointsA[i].Y * Math.Cos(alpha) * Math.Cos(beta) * Math.Sin(gamma)
                    + PointsA[i].Z * Math.Cos(alpha) * Math.Cos(beta) * Math.Cos(gamma);
                double yPart = 2 * (newPoints.ElementAt(i).Y - PointsB[i].Y);
                yPart *= -PointsA[i].X * Math.Sin(alpha) * Math.Sin(beta) + PointsA[i].Y * Math.Sin(alpha) * Math.Cos(beta) * Math.Sin(gamma) 
                    + PointsA[i].Z * Math.Sin(alpha) * Math.Cos(gamma) * Math.Cos(gamma);
                double zPart = 2 * (newPoints.ElementAt(i).Z - PointsB[i].Z);
                zPart *= -PointsA[i].X * Math.Cos(beta) - PointsA[i].Y * Math.Sin(beta) * Math.Sin(gamma) - PointsA[i].Z * Math.Sin(beta) * Math.Cos(gamma);
                sum += xPart + yPart + zPart;
            }
            return sum;
        }
        private double gradientAlongGamma(IEnumerable<Vector3> newPoints, float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double sum = 0;
            for (int i = 0; i < PointsA.Length; i++)
            {
                double xPart = 2 * (newPoints.ElementAt(i).X - PointsB[i].X);
                xPart *= PointsA[i].Y * (Math.Cos(alpha) * Math.Sin(beta) * Math.Cos(gamma) + Math.Sin(alpha) * Math.Sin(gamma)) +
                    PointsA[i].Z * (-Math.Cos(alpha) * Math.Sin(beta) * Math.Sin(gamma) + Math.Sin(alpha) * Math.Cos(gamma));
                double yPart = 2 * (newPoints.ElementAt(i).Y - PointsB[i].Y);
                yPart *= PointsA[i].Y * (Math.Sin(alpha) * Math.Sin(beta) * Math.Cos(gamma) - Math.Cos(alpha) * Math.Sin(gamma)) +
                    PointsA[i].Z * (-Math.Sin(alpha) * Math.Sin(beta) * Math.Sin(gamma) - Math.Cos(alpha) * Math.Cos(gamma));
                double zPart = 2 * (newPoints.ElementAt(i).Z - PointsB[i].Z);
                zPart *= PointsA[i].Y*Math.Cos(beta)*Math.Cos(gamma)-PointsA[i].Z*Math.Cos(beta)*Math.Sin(gamma);
                sum += xPart + yPart + zPart;
            }
            return sum;
        }
        private double gradientAlongdeltaX(IEnumerable<Vector3> newPoints, float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double sum = 0;
            for (int i = 0; i < PointsA.Length; i++)
            {
                double xPart = 2 * (newPoints.ElementAt(i).X - PointsB[i].X);
                sum += xPart;
            }
            return sum;
        }
        private double gradientAlongdeltaY(IEnumerable<Vector3> newPoints, float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double sum = 0;
            for (int i = 0; i < PointsA.Length; i++)
            {
                double yPart = 2 * (newPoints.ElementAt(i).Y - PointsB[i].Y);
                sum += yPart;
            }
            return sum;
        }
        private double gradientAlongdeltaZ(IEnumerable<Vector3> newPoints, float alpha, float beta, float gamma, float deltaX, float deltaY, float deltaZ)
        {
            double sum = 0;
            for (int i = 0; i < PointsA.Length; i++)
            {
                double zPart = 2 * (newPoints.ElementAt(i).Z - PointsB[i].Z);
                sum += zPart;
            }
            return sum;
        }
    }
}
