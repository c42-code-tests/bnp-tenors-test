using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bnp_coding_test
{
    // for simplicity all tenor validations are contained by this class
    // any tenor transformation exceptions ignored and TenorInDays left at -1 to indicate invalid tenor 
    public class PortfolioCurve
    {
        Regex _tenorSplitRegex = new Regex(@"(d|w|m|y)");
        Regex _tenorRegex = new Regex(@"^(?<TenorValue>[0-9]+)(?<TenorUnit>[dwmy])$");

        public int PortfolioId { get; private set; }
        public string Tenor { get; private set; }
        public int TenorInDays { get; private set; }
        public int Value { get; private set; }

        public PortfolioCurve(int portfolioId, string tenor, int value)
        {
            PortfolioId = portfolioId;
            Tenor = tenor.ToLower();
            Value = value;

            TenorInDays = -1; // assume invalid tenor to start with

            CalculateTenorInDays();
        }

        private void CalculateTenorInDays()
        {
            try
            {
                // split by tenor units
                List<string> tenorPairs = GetTenorPairs().ToList();

                if (!tenorPairs.Any()) return;

                int tenorInDays = 0;

                // for each tenor unit calculate equiv days
                foreach (var pair in tenorPairs)
                {
                    Match match = _tenorRegex.Match(pair);

                    if (match.Success)
                    {
                        int tenorValue = int.Parse(match.Groups["TenorValue"].Value);
                        string tenorUnit = match.Groups["TenorUnit"].Value;

                        tenorInDays += TenorToDays(tenorUnit, tenorValue);
                    }
                }

                TenorInDays = tenorInDays;
            }
            catch (ArgumentException argEx)
            {
                // ignore curves with invalid tenors
                Console.WriteLine(argEx.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<string> GetTenorPairs()
        {
            var tenorSplit = _tenorSplitRegex
                .Split(Tenor)
                .Where(s => !String.IsNullOrEmpty(s)).ToArray();

            // check for for duplicate tenor units
            if (tenorSplit.Count(t => t == "d") > 1
                 || tenorSplit.Count(t => t == "w") > 1
                 || tenorSplit.Count(t => t == "m") > 1
                 || tenorSplit.Count(t => t == "y") > 1)
                throw new ArgumentException("Duplicate unit detected");

            // check for incomplete pairs
            if (tenorSplit.Count() % 2 == 1)
                throw new ArgumentException("Incomplete pair detected");

            // join in pairs
            List<string> tenorPairs = new List<string>();
            StringBuilder tenorPair = new StringBuilder();

            for (int i = 1; i <= tenorSplit.Length; i++)
            {
                var splitItem = tenorSplit[i - 1];

                if (i % 2 == 1) // pairs start at odd intervals
                {
                    if (!Char.IsDigit(splitItem, 0)) throw new ArgumentException($"{splitItem}: Start of tenor pair is not a valid number");
                    tenorPair = new StringBuilder(splitItem);
                }
                else // pairs end at even intervals
                {
                    if (false == (splitItem == "d" || splitItem == "w" || splitItem == "m" || splitItem == "y"))
                        throw new ArgumentException($"{splitItem}: End of tenor pair is not one of d/w/m/y");

                    tenorPair.Append(splitItem);

                    tenorPairs.Add(tenorPair.ToString()); // keep adding to the list
                }
            }

            return tenorPairs;
        }

        // assumes 30 days a month, 365 days a year
        private int TenorToDays(string unit, int value)
        {
            switch (unit)
            {
                case "d":
                    return value;
                case "w":
                    return value * 7;
                case "m":
                    return value * 30;
                case "y":
                    return value * 365;
                default:
                    throw new ArgumentException($"{unit} is not a valid tenor unit"); ;
            }
        }
    }
}
