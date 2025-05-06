public static class DockRoute
{
    public static void DockRoutes(this WebApplication app)
    {
        app.MapGet("/dock", () => new Dock("Dock 1", 5))
            .WithName("GetDock")
            .Produces<Dock>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}