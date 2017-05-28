using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int mutationRatio = 1;
            int populationSize = 10000;
            double reproductionRatio = 0.4;
            string searchedPhrase = "litwo ojczyzno moja ty jestes jak zdrowie ile cie trzeba cenic ten tylko sie dowie kto cie stracil";
            GlobalPopulationRules.Initialize(mutationRatio, populationSize, searchedPhrase, reproductionRatio);
            Population population = new Population();
            population.FillWithRandom();
            population.Loop();
        }
    }
}
