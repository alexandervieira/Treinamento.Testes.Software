using AVS.NerdStore.Catalogo.Application.Services;
using AVS.NerdStore.Catalogo.Data;
using AVS.NerdStore.Catalogo.Data.Repository;
using AVS.NerdStore.Catalogo.Domain;
using AVS.NerdStore.Core.Messages.Commons.Notifications;
using MediatR;

namespace AVS.NerdStore.Site.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            // Notifications 
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            
            // Vendas
            // services.AddScoped<IPedidoRepository, PedidoRepository>();
            // services.AddScoped<IPedidoQueries, PedidoQueries>();
            // services.AddScoped<VendasContext>();

            // services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            // services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
            // services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
            // services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();
        }
    }
}