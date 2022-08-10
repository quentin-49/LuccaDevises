using LuccaDevises.Exception;
using LuccaDevises.Object;
using LuccaDevises.Ressource;
using System.Text.RegularExpressions;

namespace LuccaDevises.Service
{
    public class GraphService : IGraphService
    {
        public Graph Graph { get; set; }
        private string PathFile { get; init; }

        public GraphService(string pathFile)
        {
            PathFile = pathFile;
            Graph = Graph.GetInstance();

            GenerateGraph();
        }

        /// <summary>
        /// Génère les éléments du problème
        /// </summary>
        /// <exception cref="IntegrationDataException"></exception>
        public void GenerateGraph()
        {
            string[] linesFile = new FileService().ReadFiles(PathFile);

            var requestRegex = new Regex(Constant.RequestRegex);
            var numberDeviceChangeRegex = new Regex(Constant.DeviceChangeNumberRegex);
            var deviceChangeRegex = new Regex(Constant.DeviceChangeRegex);

            foreach (string lineFile in linesFile)
            {
                if (requestRegex.IsMatch(lineFile))
                {
                    Graph.Request = new Request(lineFile);
                }
                else if (numberDeviceChangeRegex.IsMatch(lineFile))
                {
                    Graph.DeviceNumber = Convert.ToInt32(lineFile);
                }
                else if (deviceChangeRegex.IsMatch(lineFile))
                {
                    Graph.AddDeviceChange(lineFile);
                }
                else
                    throw new IntegrationDataException(string.Format(Constant.NoMatchFormat, lineFile));
            }
            Graph.GenerateListOfDevice();
        }

        /// <summary>
        /// Implémente l'algorithme de Dijkstra
        /// </summary>
        /// <returns></returns>
        public decimal CalculResult()
        {
            Currency? point = Graph.GetStartedDevice();
            do
            {
                point = Graph.DeviceProcessing(point);

            } while (point != null);

            return Graph.GetResult();
        }
    }
}
