using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        private readonly MediatR.IMediator _mediator;

        public DashboardController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var stats = await _mediator.Send(new Application.Modules.AdminModule.Queries.DashboardStats.GetAdminDashboardStatsQuery());
            return View(stats);
        }
    }
}
