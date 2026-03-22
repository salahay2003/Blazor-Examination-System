using Blazor_Examination_System.BLL.DTOs;

namespace Blazor_Examination_System.BLL.Managers.Interfaces;

public interface IDashboardManager
{
    Task<DashboardDto> GetDashboardDataAsync();
}
