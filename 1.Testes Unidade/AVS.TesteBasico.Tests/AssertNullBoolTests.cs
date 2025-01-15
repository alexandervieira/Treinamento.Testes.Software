using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVS.TesteBasico.Tests
{
    public class AssertNullBoolTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Funcionario_Nome_NaoDeveSerNuloOuVazio(string nome)
        {
            // Arrange & Act            
            var funcionario = new Funcionario(nome, 2000);
            
            // Assert
            Assert.False(!string.IsNullOrEmpty(funcionario.Nome));
        }

        [Fact]
        public void Funcionario_Apelido_NaoDeveTerApelido()
        {
            // Arrange & Act
            var funcionario = new Funcionario("Eduardo", 2000);

            // Assert
            Assert.Null(funcionario.Apelido);

            // Assert Bool
            Assert.True(string.IsNullOrEmpty(funcionario.Apelido));
            Assert.False(funcionario.Apelido?.Length > 0);
        }
    }
}