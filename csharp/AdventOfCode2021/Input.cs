using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Input
    {
        //public static async Task<int[]> GetReadings(string filename)
        //{
        //    try
        //    {
        //        using var sr = new StreamReader(filename);
        //        var input = await sr.ReadToEndAsync();
                
        //        var depthReadings = input.Split(Environment.NewLine)
        //            .Where(s => !string.IsNullOrEmpty(s))
        //            .Select(s => int.Parse(s))
        //            .ToArray();
        //        return depthReadings;
        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        throw new Exception("Failed to read the file", ex);
        //    }
        //}

        public static async Task<string[]> ReadInput(string filename)
        {
            try
            {
                using var sr = new StreamReader(filename);
                var input = await sr.ReadToEndAsync();

                return input.Split(Environment.NewLine)
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToArray();
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Failed to read the file", ex);
            }
        }
    }
}
