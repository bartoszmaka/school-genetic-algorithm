using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Population
    {
        public static int Generation { get; set; }
        public static Random Rnd = new Random();

        public List<Gene> Pop { get; private set; }
        public List<Gene> ReproductionPool { get; private set; }
        public List<Gene> LatestBestGenes { get; set; }
        public Gene CurrentBestGene { get; set; }
        public double AvgFitness { get; private set; }
        public double TotalFitness { get; private set; }

        public Population()
        {
            Pop = new List<Gene>();
            ReproductionPool = new List<Gene>();
            LatestBestGenes = new List<Gene>();
            AvgFitness = 0;
            TotalFitness = 0;
        }

        public void Loop()
        {
            do
            {
                FillReproductionPool();
                GenerateNextPopulation();
                CalculateAvgFitness();
                FindCurrentBestGene();
                FillLatestBestGenes();
                PrintInfo();
                Generation++;
            } while (CurrentBestGene.Fitness < 1);
        }

        public void GenerateNextPopulation()
        {
            TotalFitness = 0;
            Pop = new List<Gene>();
            for (int i = 0; i < GlobalPopulationRules.PopulationSize; i++)
            {
                Gene parentA = RandomGene(ReproductionPool);
                Gene parentB = RandomGene(ReproductionPool);
                Gene child = new Gene(parentA, parentB);
                Pop.Add(child);
                TotalFitness += child.Fitness;
            }
        }

        public void FillReproductionPool()
        {
            int count = (int) (GlobalPopulationRules.PopulationSize * GlobalPopulationRules.AllowedToReproductionRatio);
            ReproductionPool = Pop.OrderByDescending(x => x.Fitness).Take(count).ToList();
        }

        public void FindCurrentBestGene()
        {
            CurrentBestGene = ReproductionPool.OrderByDescending(x => x.Fitness).First();
        }

        public void FillLatestBestGenes()
        {
            LatestBestGenes.Insert(0, CurrentBestGene);
            if (LatestBestGenes.Count > 20)
            {
                LatestBestGenes.RemoveAt(19);
            }
        }

        private void CalculateAvgFitness()
        {
            AvgFitness = TotalFitness / Pop.Count;
        }

        public void FillWithRandom()
        {
            for (int i = 0; i < GlobalPopulationRules.PopulationSize; i++)
            {
                Gene gene = new Gene();
                Pop.Add(gene);
                TotalFitness += gene.Fitness;
            }
        }

        private void PrintInfo()
        {
            Console.Clear();
            Console.WriteLine($"Generation:\t{Generation}");
            Console.WriteLine($"AvgFitness:\t{AvgFitness}");
            Console.WriteLine($"Target:\t\t{GlobalPopulationRules.TargetCode}");
            for (var index = 0; index < LatestBestGenes.Count; index++)
            {
                var item = LatestBestGenes[index];
                Console.WriteLine($"\t#{Generation - index}\t{item.Code}\t{item.Fitness}");
            }
        }

        public static Gene RandomGene(List<Gene> genesPool)
        {
            int randomIndex = Rnd.Next(0, genesPool.Count);
            return genesPool[randomIndex];
        }
    }
}
