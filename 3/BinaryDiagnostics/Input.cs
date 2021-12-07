using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDiagnostics
{
    public class Input
    {
        public static async Task<string[]> GetInput(string filename)
        {
            using var sr = new StreamReader(filename);
            var content = await sr.ReadToEndAsync();
            return content.Split(Environment.NewLine);
        }
    }
}
