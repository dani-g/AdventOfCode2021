using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SonarSweep
{
    public class Input
    {
        public static async Task<int[]> GetReadings()
        {
            try
            {
                using var sr = new StreamReader("input.txt");
                var input = await sr.ReadToEndAsync();
                
                var depthReadings = input.Split(Environment.NewLine)
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => int.Parse(s))
                    .ToArray();
                return depthReadings;
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Failed to read the file", ex);
            }
        }
    }
}
