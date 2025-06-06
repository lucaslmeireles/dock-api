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
        route.MapPost("/{dockId:guid}/check-in/{truckId:guid}",
        async (Guid dockId, Guid truckId, DockContext context, TruckOnDockRequest req) =>
        {
            var trucks = await context.truckOnDocks.Where(d => d.DockId == dockId && d.TruckId == truckId).ToListAsync();
            var dock = await context.Dock.FirstOrDefaultAsync(d => d.Id == dockId);
            var truckOnDock = new TruckOnDock(truckId, dockId, req.slot);
            if (trucks.Any(t => t.TruckId == truckId))
            {
                return Results.Conflict("Truck already checked in");
            }
            if (dock == null)
            {
                return Results.NotFound();
            }
            if (dock.Slots == 0)
            {
                return Results.Conflict("No slots available");
            }
            if (req.slot > dock.Slots)
            {
                return Results.Conflict("Slot not available");
            }
            if (trucks != null && trucks.Any(t => t.Slot == req.slot))
            {
                return Results.Conflict("Slot already occupied");
            }
            await context.AddAsync(truckOnDock);
            await context.SaveChangesAsync();
            return Results.Created($"/dock/{dockId}/checkin", truckOnDock);
        });

        route.MapPost("/{dockId:guid}/checkout/{truckId:guid}",
        async (Guid dockId, Guid truckId, DockContext context) =>
        {
            var truckOnDock = await context.truckOnDocks.FirstOrDefaultAsync(d => d.TruckId == truckId && d.DockId == dockId);
            if (truckOnDock == null)
            {
                return Results.NotFound();
            }
            if (truckOnDock.DepartureTime != default)
            {
                return Results.Conflict("Truck already checked out");
            }
            truckOnDock.SetDepartureTime(DateTime.UtcNow);
            await context.SaveChangesAsync();
            return Results.Ok(truckOnDock);
        });
    }

}