using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public interface IDayExercise
    {
        int Order { get; }
        string Name { get; }
        Task<long> Solve1();
        Task<long> Solve2();
    }
}