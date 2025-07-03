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
            var truck = new Truck(req.driverName, req.carrieName);
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
        route.MapDelete("/{id:guid}", async (Guid id, DockContext context) =>
            {
                var truck = await context.Truck.FirstOrDefaultAsync(t => t.Id == id);
                if (truck is null)
                {
                    return Results.NotFound("Truck not found");
                }
                context.Entry(truck).State = EntityState.Deleted;
                await context.SaveChangesAsync();
                return Results.Ok("Truck deleted");
        });
    }
}