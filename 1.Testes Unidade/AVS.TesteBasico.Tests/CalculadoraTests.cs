namespace AVS.TesteBasico.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Somar(2, 2);

            //Assert
            Assert.Equal(4, resultado);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(3, 3, 6)]
        [InlineData(4, 2, 6)]
        [InlineData(5, 3, 8)]
        [InlineData(6, 4, 10)]
        [InlineData(7, 5, 12)]
        public void Calculadora_Somar_RetornarValoresCorretos(double x, double y, double total)
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Somar(x, y);

            //Assert
            Assert.Equal(total, resultado);
        }

        [Fact]
        public void Calculadora_Dividir_RetornarValorDivisao()
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Dividir(2, 2);

            //Assert
            Assert.Equal(1, resultado);
        }

        [Theory]
        [InlineData(2, 2, 1)]
        [InlineData(4, 2, 2)]
        [InlineData(5, 5, 1)]
        [InlineData(6, 2, 3)]
        [InlineData(8, 2, 4)]
        [InlineData(9, 3, 3)]
        public void Calculadora_Dividir_RetornarValoresCorretos(int x, int y, double total)
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Dividir(x, y);

            //Assert
            Assert.Equal(total, resultado);
        }
    }
}
