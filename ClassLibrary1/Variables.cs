using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public struct Variables
    {
        public double alpha { get; set; }
        public double beta { get; set; }
        public double gamma { get; set; }
        public double deltaX { get; set; }
        public double deltaY { get; set; }
        public double deltaZ { get; set; }

        public static Variables operator *(double multiply, Variables variables)
        {
            return new Variables
            {
                alpha = multiply * variables.alpha,
                beta = multiply * variables.beta,
                gamma = multiply * variables.gamma,
                deltaX = multiply * variables.deltaX,
                deltaY = multiply * variables.deltaY,
                deltaZ = multiply * variables.deltaZ
            };
        }
        public static double operator *(Variables first, Variables second)
        {
            return (first.alpha * second.alpha)
                + (first.beta * second.beta)
                + (first.gamma * second.gamma)
                + (first.deltaX * second.deltaX)
                + (first.deltaY * second.deltaY)
                + (first.deltaZ * second.deltaZ);            
        }
        public static Variables operator +(Variables first, Variables second)
        {
            return new Variables
            {
                alpha = first.alpha + second.alpha,
                beta = first.beta + second.beta,
                gamma = first.gamma + second.gamma,
                deltaX =  first.deltaX + second.deltaX,
                deltaY = first.deltaY + second.deltaY,
                deltaZ = first.deltaZ + second.deltaZ
            };
        }
    }
}
