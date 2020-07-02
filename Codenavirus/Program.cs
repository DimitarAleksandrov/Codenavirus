namespace Codenavirus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            var testInputWorld = new char[][] { new char[] { '#', '#', '#' }, new char[] { '#', '#', '#' }, new char[] { '#', '#', '#' } };
            var testInputFirstInfected = new int[] { 1, 1 };
            var result = Codenavirus(testInputWorld, testInputFirstInfected);
            //Console.WriteLine($"[{string.Join(',', result)}]");

            int[] Codenavirus(char[][] world, int[] firstInfected)
            {
                CheckInputMatrix(world);
                CheckInputArray(firstInfected);
                
                var days = 0;
                var newInfected = 1;

                var recoverySchedule = new Dictionary<int, List<int[]>>();
                var day0infected = new List<int[]>();

                addToDictionaryRecoveredPeople(days + 1, firstInfected);
                day0infected.Add(firstInfected);

                ////code to display the array on the console
                //Console.WriteLine("before while");
                //printMatrix(world, days);

                while (newInfected > 0)
                {
                    days += 1;
                    newInfected = 0;

                    if (recoverySchedule.ContainsKey(days))
                    {
                        foreach (var record in recoverySchedule[days])
                        {
                            world[record[0]][record[1]] = 'r';
                        }
                    }

                    for (int row = 0; row < world.Length; row++)
                    {
                        for (int coll = 0; coll < world[row].Length; coll++)
                        {
                            if (world[row][coll].Equals('i'))
                            {
                                int[] coordinates = new int[2];
                                // try to infect someone: right, top, left, below.
                                if (coll + 1 < world[row].Length && world[row][coll + 1].Equals('#'))
                                {
                                    coordinates[0] = row;
                                    coordinates[1] = coll + 1;
                                    addToDictionaryRecoveredPeople(days, coordinates);
                                    day0infected.Add(coordinates);
                                }
                                else if (row - 1 >= 0 && world[row - 1][coll].Equals('#'))
                                {
                                    coordinates[0] = row - 1;
                                    coordinates[1] = coll;
                                    addToDictionaryRecoveredPeople(days, coordinates);
                                    day0infected.Add(coordinates);
                                }
                                else if (coll - 1 >= 0 && world[row][coll - 1].Equals('#'))
                                {
                                    coordinates[0] = row;
                                    coordinates[1] = coll - 1;
                                    addToDictionaryRecoveredPeople(days, coordinates);
                                    day0infected.Add(coordinates);
                                }
                                else if (row + 1 < world.Length && world[row + 1][coll].Equals('#'))
                                {
                                    coordinates[0] = row + 1;
                                    coordinates[1] = coll;
                                    addToDictionaryRecoveredPeople(days, coordinates);
                                    day0infected.Add(coordinates);
                                }
                            }
                        }
                    }

                    newInfected = day0infected.Count();

                    foreach (var coordinate in day0infected)
                    {
                        world[coordinate[0]][coordinate[1]] = 'i';
                    }
                    day0infected.Clear();

                    ////code to display the array on the console
                    //Console.WriteLine("end while");
                    //printMatrix(world, days);
                }
                var allInfected = 0;
                var recovered = 0;
                var health = 0;

                for (int row = 0; row < world.Length; row++)
                {
                    for (int coll = 0; coll < world[row].Length; coll++)
                    {
                        if (world[row][coll].Equals('#'))
                        {
                            health += 1;
                        }
                        else if (world[row][coll].Equals('i'))
                        {
                            allInfected += 1;
                        }
                        else if (world[row][coll].Equals('r'))
                        {
                            recovered += 1;
                        }
                    }
                }

                return new int[] { days, allInfected, recovered, health };

                void addToDictionaryRecoveredPeople(int curentDay, int[] coordinate)
                {
                    var key = curentDay + 3;
                    if (!recoverySchedule.ContainsKey(key))
                    {
                        recoverySchedule.Add(key, new List<int[]>());
                    }
                    recoverySchedule[key].Add(coordinate);
                }
            }
            void CheckInputMatrix(char[][] world)
            {
                for (int row = 0; row < world.Length; row++)
                {
                    for (int coll = 0; coll < world[row].Length; coll++)
                    {
                        if (!(world[row][coll].Equals('#') || world[row][coll].Equals('.')))
                        {
                            throw new ArgumentException("Allowed symbol are only '#' and '.'!");
                        }
                    }
                }
            }
            void CheckInputArray(int[] firstInfected)
            {
                if (firstInfected.Length != 2)
                {
                    throw new ArgumentException("The array must contain exactly 2 elements!");
                }
            }
            ////code to display the array on the console
            //void printMatrix(char[][] world, int day)
            //{
            //    Console.WriteLine(day);
            //    for (int i = 0; i < world.Length; i++)
            //    {
            //        for (int j = 0; j < world[i].Length; j++)
            //        {
            //            Console.Write("{0}{1}", world[i][j], j == (world[i].Length - 1) ? $"{Environment.NewLine}" : ", ");
            //        }
            //    }
            //}
        }
    }
}
