using LuccaDevises.Exception;
using LuccaDevises.Object;
using LuccaDevises.Ressource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevisesTest
{
    public class CurrencyChangeMethodsTest
    {
        [Fact]
        public void CurrencyChange_NegativeWeight_AddCurrencyChange_IntegrationException()
        {
            var graph = new Graph();

            void result() => graph.CurrencyChanges.AddCurrencyChange("AAA", "BBB", 0);

            IntegrationDataException integrationDataException = Assert.Throws<IntegrationDataException>(result);

            Assert.Equal(Constant.CurrencyChangeNullError, integrationDataException.Message);
        }

        [Fact]
        public void CurrencyChange_Existing_AddCurrencyChange_IntegrationException()
        {
            var graph = new Graph();
            graph.CurrencyChanges.AddCurrencyChange("AAA", "BBB", 1);

            void result() => graph.CurrencyChanges.AddCurrencyChange("AAA", "BBB", 1);

            IntegrationDataException integrationDataException = Assert.Throws<IntegrationDataException>(result);

            Assert.Equal(Constant.CurrencyChangeAlreadyExist, integrationDataException.Message);
        }
    }
}
