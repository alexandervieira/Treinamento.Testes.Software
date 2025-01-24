using AVS.NerdStore.Core.Messages.Commons.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AVS.NerdStore.Site.MVC.Extensions
{
    public class SummaryViewComponent: ViewComponent
    {
        private readonly DomainNotificationHandler _notifications;

        public SummaryViewComponent(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notifications.ObterNotificacoes());
            notifications.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));

            return View();
        }
    }
}