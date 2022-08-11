using LuccaDevises.Exception;
using LuccaDevises.Object;
using LuccaDevises.Ressource;
using System.Text.RegularExpressions;

namespace LuccaDevises.Service
{
    public class GraphService : IGraphService
    {
        public Graph Graph { get; set; }
        private string FilePath { get; init; }

        public GraphService(string filePath)
        {
            FilePath = filePath;
            Graph = Graph.GetInstance();

            GenerateGraph();
        }

        /// <summary>
        /// Génère les éléments du problème
        /// </summary>
        /// <exception cref="IntegrationDataException"></exception>
        public void GenerateGraph()
        {
            string[] linesFile = new FileService().ReadFiles(FilePath);

            var requestRegex = new Regex(Constant.RequestRegex);
            var numberCurrencyChangeRegex = new Regex(Constant.CurrencyChangeNumberRegex);
            var CurrencyChangeRegex = new Regex(Constant.CurrencyChangeRegex);

            foreach (string lineFile in linesFile)
            {
                if (requestRegex.IsMatch(lineFile))
                {
                    Graph.Request = new Request(lineFile);
                }
                else if (numberCurrencyChangeRegex.IsMatch(lineFile))
                {
                    Graph.CurrencyNumber = Convert.ToInt32(lineFile);
                }
                else if (CurrencyChangeRegex.IsMatch(lineFile))
                {
                    Graph.AddCurrencyChange(lineFile);
                }
                else
                    throw new IntegrationDataException(string.Format(Constant.NoMatchFormat, lineFile));
            }
            Graph.GenerateCurrencies();
        }

        /// <summary>
        /// Implémente l'algorithme de Dijkstra
        /// </summary>
        /// <returns></returns>
        public decimal CalculResult()
        {
            Currency? point = Graph.GetStartedCurrency();
            do
            {
                point = Graph.CurrencyProcessing(point);

            } while (point != null);

            return Graph.GetResult();
        }
    }
}
