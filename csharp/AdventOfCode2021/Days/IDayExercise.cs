using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public interface IDayExercise
    {
        int Order { get; }
        string Name { get; }
        Task<int> Solve1();
        Task<int> Solve2();
    }
}