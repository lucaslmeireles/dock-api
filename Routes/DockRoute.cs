using Microsoft.EntityFrameworkCore;

public static class DockRoute
{
    public static void DockRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/dock");
        route.MapPost("/", async (DockRequest req, DockContext context) =>
        {
            var dock = new Dock(req.name, req.slots);
            await context.AddAsync(dock);
            await context.SaveChangesAsync();
        });
        route.MapGet("/", async (DockContext context) =>
        {
            var docks = await context.Dock.ToListAsync();

            return Results.Ok(docks);
        });

        route.MapPatch("/{id:guid}",
        async (Guid id, DockRequest req, DockContext context) =>
        {
            var dock = await context.Dock.FirstOrDefaultAsync(x => x.Id == id);

            if (dock == null)
            {
                return Results.NotFound();
            }
            dock.ChangeName(req.name);
            await context.SaveChangesAsync();
            return Results.Ok(dock);
        });

        route.MapDelete("/{id:guid}",
         async (Guid id, DockContext context) =>
         {
             var dock = await context.Dock.FirstOrDefaultAsync(x => x.Id == id);

             if (dock == null)
             {
                 return Results.NotFound();
             }
             ;
             dock.SetInactive();
             await context.SaveChangesAsync();
             return Results.Ok(dock);
         });
        route.MapPost("/{id:guid}/checkin",
        async (TruckOnDockRequest req, Guid id, DockContext context) =>
        {
            var truckOnDock = new TruckOnDock(req.truckId, id, req.slot);
            await context.AddAsync(truckOnDock);
            await context.SaveChangesAsync();
            return Results.Created($"/dock/{id}/checkin", truckOnDock);
        });

    }

}