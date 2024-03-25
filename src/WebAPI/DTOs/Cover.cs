using Shared.Enums;

namespace WebAPI.DTOs;

public class Cover : Dto
{
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public CoverType Type { get; set; }

    public decimal Premium { get; set; }
}
