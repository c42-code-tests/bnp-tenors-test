using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace bnp_coding_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputCurves = ReadPortfolioCurvesFromFile();

            List<PortfolioCurve> portfolioCurves = new List<PortfolioCurve>();

            foreach (var curve in inputCurves)
            {
                var portfolioCurve = new PortfolioCurve(curve.PortfolioId, curve.Tenor, curve.Value);

                portfolioCurves.Add(portfolioCurve);
            }

            // sort by tenor, portfolioid
            var outputByTenorPortfolio = portfolioCurves
                    .Where(c => c.TenorInDays > 0) // only write out valid tenors
                    .OrderBy(c => c.TenorInDays)
                    .ThenBy(c => c.PortfolioId)
                    .ToList();

            WriteCurvesToFile(outputByTenorPortfolio, @".\3.txt");

            // sort by portfolioid, tenor
            var outputByPortfolioTenor = portfolioCurves
                    .Where(c => c.TenorInDays > 0) // only write out valid tenors
                    .OrderBy(c => c.PortfolioId)
                    .ThenBy(c => c.TenorInDays)
                    .ToList();

            WriteCurvesToFile(outputByPortfolioTenor, @".\4.txt");
        }

        private static void WriteCurvesToFile(List<PortfolioCurve> curves, string fileName)
        {
            File.Delete(fileName);

            using (TextWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("tenor, portfolioid, value");

                foreach (var curve in curves)
                {
                    writer.WriteLine($"{curve.Tenor}, {curve.PortfolioId}, {curve.Value}");
                }
            }
        }

        private static List<CurveData> ReadPortfolioCurvesFromFile()
        {
            List<CurveData> portfolioCurves = new List<CurveData>();

            var lines = File.ReadLines(@".\data.txt").ToList();

            lines.RemoveAll(l => l.StartsWith("tenor") || string.IsNullOrWhiteSpace(l));

            foreach (var line in lines)
            {
                var items = line.Split(',');

                var curve = new CurveData()
                {
                    Tenor = items[0].Trim().ToLower(),
                    PortfolioId = Int32.Parse(items[1]),
                    Value = Int32.Parse(items[2])
                };

                portfolioCurves.Add(curve);
            }

            return portfolioCurves;
        }
    }
}
