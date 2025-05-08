using Microsoft.EntityFrameworkCore;

public static class CargoRoute
{
    public static void CargoRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/cargo");
        route.MapGet("",
        async (DockContext context) =>
        {
            var cargo = await context.Cargo.ToListAsync();
            if (cargo == null)
            {
                return Results.NoContent();
            }
            ;
            return Results.Ok(cargo);
        });

        route.MapPost("",
        async (CargoRequest req, DockContext context) =>
        {
            var cargo = new Cargo(req.productName, req.receiptNumber, req.supplier);
            await context.AddAsync(cargo);
            await context.SaveChangesAsync();
            return Results.Created($"/cargo/{cargo.Id}", cargo);
        });
        route.MapGet("/{id:guid}",
        async (Guid id, DockContext context) =>
        {
            var cargo = await context.Cargo.FirstOrDefaultAsync(x => x.Id == id);
            if (cargo == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(cargo);
        });
    }
}