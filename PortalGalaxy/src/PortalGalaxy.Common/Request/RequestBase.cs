namespace PortalGalaxy.Common.Request;

public class RequestBase
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    protected RequestBase()
    {
        PageNumber = 1;
        PageSize = 15;
    }
}