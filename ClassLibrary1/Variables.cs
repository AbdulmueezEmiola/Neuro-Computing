using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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

        public Variables(string variablesAsString)
        {
            var variables = variablesAsString.Remove(0,1).Remove(variablesAsString.Length-2).Split(';');
            alpha = double.Parse(variables[0]);
            beta = double.Parse(variables[1]);
            gamma = double.Parse(variables[2]);
            deltaX = double.Parse(variables[3]);
            deltaY = double.Parse(variables[4]);
            deltaZ = double.Parse(variables[5]);
        }
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
