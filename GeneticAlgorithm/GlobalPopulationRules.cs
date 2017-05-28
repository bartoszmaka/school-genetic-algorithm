using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public static class GlobalPopulationRules
    {
        public static int GeneLength { get; set; }
        public static int MutationRatio { get; set; }
        public static int PopulationSize { get; set; }
        public static string TargetCode { get; set; }
        public static double AllowedToReproductionRatio { get; set; }
       

        public static void Initialize(int mutationRatio, int populationSize, string targetCode, double ratio, int geneLength = 0)
        {
            if (mutationRatio > 100 || mutationRatio < 0)
            {
                Console.WriteLine("Invalid mutation ratio... using default '5' value");
                mutationRatio = 5;
            }
            if (geneLength == 0)
            {
                geneLength = targetCode.Length;
            }
            GeneLength = geneLength;
            MutationRatio = mutationRatio;
            PopulationSize = populationSize;
            TargetCode = targetCode.ToLower();
            AllowedToReproductionRatio = ratio;
        }
    }

}
