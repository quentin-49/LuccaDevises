using LuccaDevises.Exception;
using LuccaDevises.Object;
using LuccaDevises.Ressource;
using LuccaDevises.Service;

namespace LuccaDevisesTest
{
    public class GraphTest
    {
        [Fact]
        public void RecordList_GetResult_ResultException()
        {
            var graph = new Graph();
            //arrange
            void result() => graph.GetResult();
            //act 
            ResultException resultException = Assert.Throws<ResultException>(result);
            //assert 
            Assert.Equal(Constant.NoResultCalculeted, resultException.Message);
        }

        [Fact]
        public void RecordList_GetResult_Success()
        {
            var graph = new Graph();
            graph.RecordList
                .Add(new LuccaDevises.Object.Record("BBB", "AAA", 12));

            graph.Request = new Request() { Amount = 50, StartCurrency = "AAA", EndCurrency = "BBB" };
            decimal decimalResult = graph.GetResult();

            Assert.Equal(50 * 12, decimalResult);
        }

        [Fact]
        public void Currency_GenerateCurrencies_IntegrationException()
        {
            var graph = new Graph
            {
                CurrencyNumber = 1
            };

            void result() => graph.GenerateCurrencies();

            IntegrationDataException integrationDataException = Assert.Throws<IntegrationDataException>(result);

            Assert.Equal(Constant.IncoherentCurrencyChanges,integrationDataException.Message);
        }
    }
}