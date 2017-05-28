using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Gene
    {
        public static Random Rnd = new Random();
        public string Code { get; set; }
        public double Fitness { get; set; }

        public Gene()
        {
            Code = RandomCode();
            Initialize();
        }

        public Gene(string c)
        {
            Code = c;
            Initialize();
        }

        public Gene(Gene geneA, Gene geneB)
        {
            Code = CrossoverCode(geneA, geneB);
            Mutate();
            Initialize();
        }

        private void Initialize()
        {
            Fitness = 0;
            CalculateFitness();
        }

        private void CalculateFitness()
        {
            for (int i = 0; i < GlobalPopulationRules.GeneLength; i++)
            {
                if (Code[i] == GlobalPopulationRules.TargetCode[i])
                {
                    Fitness++;
                }
            }
            Fitness /= GlobalPopulationRules.GeneLength;
        }

        private void Mutate()
        {
            StringBuilder mutatedCode = new StringBuilder(Code);
            for (int i = 0; i < GlobalPopulationRules.GeneLength; i++)
            {
                if (Rnd.Next(0, 100) < GlobalPopulationRules.MutationRatio)
                {
                    mutatedCode[i] = RandomChar();
                }
            }
            Code = mutatedCode.ToString();
        }

        private static string CrossoverCode(Gene geneA, Gene geneB)
        {
            int stringBreakIndex = Rnd.Next(1, GlobalPopulationRules.GeneLength);
            int end = GlobalPopulationRules.GeneLength - stringBreakIndex;
            StringBuilder result = new StringBuilder();
            result.Append(geneA.Code.Substring(0, stringBreakIndex));
            result.Append(geneB.Code.Substring(stringBreakIndex, end));
            return result.ToString();
        }

        private static string RandomCode()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < GlobalPopulationRules.GeneLength; i++)
            {
                result.Append(RandomChar());
            }
            return result.ToString();
        }

        private static char RandomChar()
        {
            char randomChar = (char) Rnd.Next(97, 124);
            if (randomChar == 123) randomChar = ' '; 
            return randomChar;
        }
    }
}
