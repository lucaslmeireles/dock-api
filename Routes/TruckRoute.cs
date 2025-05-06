public static class TruckRoute
{
    public static void TruckRoutes(this WebApplication app)
    {
        app.MapGet("/dock", () => "DOCK");
    }
}