using AVS.TesteBasico.Utils;

namespace AVS.TesteBasico.Tests
{
    public class AssertStringsTests
    {
        [Fact]
        public void StringTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Equal("Eduardo Pires", nomeCompleto);
        }
    }
}
