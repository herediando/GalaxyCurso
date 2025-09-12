using System;

namespace PortalGalaxy.Services.Utils;

public static class Helper
{
    public static int GetTotalPages(int totalRows, int pageSize)
    {
        if (totalRows == 0) return 0;

        var total = totalRows / pageSize;
        if (totalRows % pageSize > 0)
            total++;
        return total;
    }
}
