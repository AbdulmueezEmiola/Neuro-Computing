using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace ClassLibrary1
{
    public static class FileToArray
    {
        public static IEnumerable<Tuple<string,Vector3>> Converter(string fileName)
        {
            List<Tuple<string, Vector3>> retValue = new List<Tuple<string, Vector3>>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] values = line.Split('.', ';', '(', ')');
                if(values.Length > 2)
                {
                    Vector3 vector = new Vector3(convertStringToValidNumber(values[2]), convertStringToValidNumber(values[3]), convertStringToValidNumber(values[4]));
                    retValue.Add(new Tuple<string, Vector3>(values[1], vector));
                }
            }
            return retValue;
        }
        private static float convertStringToValidNumber(string value)
        {
            value = value.Replace(',','.').Trim();
            return float.Parse(value);
        }
    }
}
