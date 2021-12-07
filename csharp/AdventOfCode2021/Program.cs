using AdventOfCode2021.Days;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Choose your day to solve");
            var exercises = GetAllExercises();
            exercises.ForEach(e => {
                Console.WriteLine($"{e.Order}: {e.Name}");
            });

            Console.Write("Type the number of your choosing: ");
            var choice = Console.ReadLine();
            var exerciseChosen = 0;
            while (!int.TryParse(choice, out exerciseChosen)) {
                Console.WriteLine("sigh!!");
                Console.Write("Try again: ");
                choice = Console.ReadLine();
            }
            var exercise = exercises
                .Where(e => e.Order == exerciseChosen)
                .FirstOrDefault() ?? throw new Exception("Can't find the exercise with number "+ exerciseChosen);

            Console.WriteLine(exercise.Name);
            Console.WriteLine($"{exercise.Order}.1 = {await exercise.Solve1()}");
            Console.WriteLine($"{exercise.Order}.2 = {await exercise.Solve2()}");
        }

        private static List<IDayExercise> GetAllExercises() { 
            var instances = new List<IDayExercise>();
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetEntryAssembly();

            foreach (System.Reflection.TypeInfo ti in ass.DefinedTypes)
            {
                if (ti.ImplementedInterfaces.Contains(typeof(IDayExercise)))
                {
                    instances.Add(ass.CreateInstance(ti.FullName) as IDayExercise);
                }
            }
            return instances
                .OrderBy(t => t.Order)
                .ToList();
        }
    }
}
