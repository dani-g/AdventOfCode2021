using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dive
{
    public class Input
    {
        public static async Task<string[]> GetReadings(string filename)
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
