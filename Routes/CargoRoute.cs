public static class CargoRoute
{
    public static void CargoRoutes(this WebApplication app)
    {
        app.MapGet("/dock", () => "DOCK");
    }
}