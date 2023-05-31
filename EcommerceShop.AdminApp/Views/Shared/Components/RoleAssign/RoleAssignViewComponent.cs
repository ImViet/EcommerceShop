using EcommerceShop.Contracts.Dtos.RoleDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Views.Shared.Components.RoleAssign
{
    public class RoleAssignViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(RoleAssignDto roleAssigned)
        {
            return View("Default", roleAssigned);
        }
    }
}