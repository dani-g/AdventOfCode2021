using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Input
    {
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
