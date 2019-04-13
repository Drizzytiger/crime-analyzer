using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CrimeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            var crimeList = new List<categories>();
            if (args.Length != 2)
            {
                Console.WriteLine("Error! Please enter the data source and report file.");
                Console.ReadLine();
            }

            else
            {


                using (var reader = new StreamReader(args[0]))
                {

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (count == 0)
                        {
                            count++;
                        }
                        else
                        {
                            var item = new categories
                            {
                                year = Int32.Parse(values[0]),
                                population = Int32.Parse(values[1]),
                                violent = Int32.Parse(values[2]),
                                murder = Int32.Parse(values[3]),
                                rape = Int32.Parse(values[4]),
                                robbery = Int32.Parse(values[5]),
                                assault = Int32.Parse(values[6]),
                                property = Int32.Parse(values[7]),
                                burglary = Int32.Parse(values[8]),
                                theft = Int32.Parse(values[9]),
                                vehicle = Int32.Parse(values[10])
                            };
                            crimeList.Add(item);
                        }
                    }
                }
            }
            try
            {
                Console.WriteLine("Crime Analyzer Report\n\n");
                Console.WriteLine("Period: 1994-2013");
                string yearlist = "Range of Years" + crimeList[0].year + " to " + crimeList[count].year;
                int span = crimeList[count].year - crimeList[0].year;
                string spanyears = "Report spans" + span + "years";
                Console.WriteLine(yearlist);
                Console.WriteLine(spanyears);
                var lessmurder = from categories in crimeList where categories.murder < 1500 select categories.year;
                string murderyears = "Years murders per year < 1500 : ";
                Console.WriteLine(murderyears);
                foreach (var year in lessmurder)
                {
                    Console.WriteLine(year);
                }
                var numberRobberies = from categories in crimeList where categories.robbery > 500000 select new { categories.year, categories.robbery };
                string robberies = "Robberies per year > 500000: ";
                Console.WriteLine(robberies);
                foreach (var year in numberRobberies)
                {
                    Console.WriteLine(year);
                }
                var violent = (from categories in crimeList where categories.year == 2010 select categories.violent);
                var population = (from categories in crimeList where categories.year == 2010 select categories.population);
                double capita = violent / population;
                string capita = "Violent crime per capita rate in 2010:" + capita;;
                var avgtotal = crimeList.Average(x => x.murder);
                string murderaverageall = "Average murder per year(all years): " + avgtotal;
                var avgmurderfirst = (from categories in crimeList where categories.year <= 1997 && categories.year >= 1994 select categories.murder.Average());
                string murderaverage = "Average murder per year (1994-1997): " + avgmurderfirst;
                var avgmurderSecond = (from categories in crimeList where categories.year <= 2013 && categories.year >= 2010 select categories.murder.Average());
                string recentmurderaverage = "Average murder per year (2010-2013): " + avgmurderSecond;
                var minThefts = (from categories in crimeList where categories.year >= 1999 && categories.year <= 2004 select categories.theft.Min());
                string minthefts = "Minumum thefts per year" + minThefts;
                var maxThefts = (from categories in crimeList where categories.year >= 1990 && categories.year <= 2004 select categories.theft.Min());
                string maxthefts = "Maximum thefts per year (1999-2004): " + maxThefts;
                Console.WriteLine(capita);
                Console.WriteLine(murderaverageall);
                Console.WriteLine(murderaverage);
                Console.WriteLine(recentmurderaverage);
                Console.WriteLine(minthefts);
                Console.WriteLine(maxthefts);
                Console.WriteLine(maxThefts);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable analysis" + e.Message);
            }
        }
    }
}
