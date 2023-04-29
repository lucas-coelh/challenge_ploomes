using Moq;
using Newtonsoft.Json;
using WebAPIs.Models.External;
using WebAPIs.Utils.ExternalAPI;

namespace UnitTest.Apis.Utils.ExternalAPI
{
    public class CnpjHelperTest
    {
        private static Mock<HttpClient> client = new Mock<HttpClient>();

        [Fact(DisplayName = "Should return false since the numbers are equal")]
        public async Task ShouldReturnFalseSinceNumbersEqual()
        {
            //Arrange
            var cnpj = "11111111111111";
            // Act
            var result = CnpjHelper.ValidCnpj(cnpj);
            // Assert
            Assert.Equal(cnpj, result.cnpj);
            Assert.False(result.valido);
        }

        [Fact(DisplayName = "Should return false since the CNPJ does not have 14 digits")]
        public async Task ShouldReturnFalseCNPJHassLessDigits()
        {
            //Arrange
            var cnpj = "1111111111111";
            // Act
            var result = CnpjHelper.ValidCnpj(cnpj);
            // Assert
            Assert.Equal(cnpj, result.cnpj);
            Assert.False(result.valido);
        }

        [Fact(DisplayName = "should return false because the CNPJ has letters")]
        public async Task ShouldReturnFalseCNPJHassLetter()
        {
            //Arrange
            var cnpj = "1111111111111a";
            // Act
            var result = CnpjHelper.ValidCnpj(cnpj);
            // Assert
            Assert.Equal(cnpj, result.cnpj);
            Assert.False(result.valido);
        }

        [Fact(DisplayName = "Should return true because the CNPJ is valid")]
        public async Task ShouldReturnTrueBecauseCNPJValid()
        {
            //Arrange
            var cnpj = "11.222.333/0001-81";
            // Act
            var result = CnpjHelper.ValidCnpj(cnpj);
            // Assert
            Assert.True(result.valido);
        }

        [Fact(DisplayName = "Should return true because the CNPJ is valid")]
        public async Task ShouldReturnTrueBecauseCNPJValid2()
        {
            //Arrange
            var cnpj = "11222333000181";
            // Act
            var result = CnpjHelper.ValidCnpj(cnpj);
            // Assert
            Assert.True(result.valido);
        }

      //  [Fact(DisplayName = "Deve retornar True Já que o CNPJ é válido.")]
      //  public async Task ValidCnpj_ShouRj_And_True_When_Is_Called()
      //  {
      //      //Arrange
      //      string cnpj = "01234567890123";
      //      string expectedUrl = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";
      //      PessoaJuridica expectedResponse = new PessoaJuridica 
      //      { Cnpj = cnpj, 
      //          Nome = "Empresa Teste", 
      //          Bairro = "Testenopolis", 
      //          Email = "teste@email.com", 
      //          Logradouro = "Rua Aprovado", 
      //          Municipio = "Aprovanopolis", 
      //          Numero = "2023", 
      //          UF = "AP" 
      //      };
      //      client.Setup(c => c.GetStringAsync(expectedUrl))
      //.ReturnsAsync(JsonConvert.SerializeObject(expectedResponse));

      //      // Act
      //      PessoaJuridica actualResponse = await CnpjHelper.GetCnpj(cnpj);
      //      // Assert
      //      Assert.Equal(expectedResponse.Cnpj, actualResponse.Cnpj);
      //      Assert.Equal(expectedResponse.Nome, actualResponse.Nome);
      //      Assert.Equal(expectedResponse.Email, actualResponse.Email);
      //      Assert.Equal(expectedResponse.Logradouro, actualResponse.Logradouro);
      //      Assert.Equal(expectedResponse.Numero, actualResponse.Numero);
      //      Assert.Equal(expectedResponse.Municipio, actualResponse.Municipio);
      //      Assert.Equal(expectedResponse.Bairro, actualResponse.Bairro);
      //      Assert.Equal(expectedResponse.UF, actualResponse.UF);
      //  }
    }
}