using AVS.Features.Core;

namespace AVS.Features.Clientes
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObterPorEmail(string email);
    }
}