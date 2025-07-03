using Microsoft.EntityFrameworkCore;

public static class TruckRoute
{
    public static void TruckRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/truck");
        route.MapGet("", async (DockContext context) =>
        {
            var trucks = await context.Truck.ToListAsync();
            if (trucks == null)
            {
                return Results.NoContent();
            }
            return Results.Ok(trucks);
        });
        route.MapPost("", async (TruckRequest req, DockContext context) =>
        {
            var truck = new Truck(req.driverName, req.carrierName);
            await context.AddAsync(truck);
            await context.SaveChangesAsync();
            return Results.Created($"/truck/{truck.Id}", truck);
        });
        route.MapGet("/{id:guid}", async (Guid id, DockContext context) =>
        {
            var truck = await context.Truck
            .Include(t => t.Cargo)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (truck == null)
            {
                return Results.NotFound();
            }
            ;
            return Results.Ok(truck);
        });
        route.MapGet("/{id:guid}/history", async (Guid id, DockContext context) =>
        {
            var truckOnDock = await context.truckOnDocks.FirstOrDefaultAsync(x => x.TruckId == id);
            if (truckOnDock == null)
            {
                return Results.NotFound("Truck not on dock");
            }
            ;

            return Results.Ok(truckOnDock);

        });
        route.MapDelete("/{id:guid}", async (Guid Id, DockContext context) =>
        {
            var truck = await context.Truck.FirstOrDefaultAsync(x => x.Id == Id);
            if (truck == null)
            {
                return Results.NotFound();
            }
            ;
            context.Remove(truck);
            await context.SaveChangesAsync();
            return Results.Ok("Truck deleted");
        });
        route.MapPatch("/{id:guid}", async (Guid id, TruckRequest req, DockContext context) =>
        {
            var truck = await context.Truck.FirstOrDefaultAsync(x => x.Id == id);
            if (truck == null)
            {
                return Results.NotFound();
            }
            ;
            truck.ChangeDriverName(req.driverName);
            truck.ChangeCarrierName(req.carrierName);
            await context.SaveChangesAsync();
            return Results.Ok(truck);
        });

    }
}