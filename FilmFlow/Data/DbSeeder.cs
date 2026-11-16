using FilmFlow.Core.Constants;
using FilmFlow.Core.Entities;
using FilmFlow.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FilmFlow.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services, string demoPassword)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await db.Database.MigrateAsync();

        // Роли
        foreach (var role in AppRoles.All)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Демо акаунти (по един на роля)
        await EnsureUserAsync(userManager, "admin@filmflow.local", "Админ", "Админов", AppRoles.Administrator, demoPassword);
        var producer = await EnsureUserAsync(userManager, "producer@filmflow.local", "Иван", "Продуцентов", AppRoles.Producer, demoPassword);
        var crew = await EnsureUserAsync(userManager, "crew@filmflow.local", "Мария", "Екипова", AppRoles.CrewMember, demoPassword);

        // Примерни данни
        if (!await db.Locations.AnyAsync())
        {
            db.Locations.Add(new Location
            {
                Name = "Старо читалище",
                Address = "ул. Примерна 1",
                City = "София"
            });
        }

        if (!await db.Actors.AnyAsync())
        {
            db.Actors.AddRange(
                new Actor { FullName = "Георги Георгиев", Email = "georgi@example.com" },
                new Actor { FullName = "Елена Петрова", Email = "elena@example.com" });
        }

        if (!await db.Productions.AnyAsync())
        {
            db.Productions.Add(new Production
            {
                Title = "Последният влак",
                Description = "Демонстрационна късометражна продукция.",
                Status = ProductionStatus.PreProduction,
                StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(14)),
                EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                Budget = 5000m,
                ProducerId = producer.Id,
                Members = { new ProductionMember { UserId = crew.Id, CrewRole = CrewRole.Cinematographer } }
            });
        }

        await db.SaveChangesAsync();
    }

    private static async Task<ApplicationUser> EnsureUserAsync(
        UserManager<ApplicationUser> userManager, string email,
        string firstName, string lastName, string role, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null) return user;

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new InvalidOperationException(
                "Неуспешно създаване на потребител: " + string.Join("; ", result.Errors.Select(e => e.Description)));

        await userManager.AddToRoleAsync(user, role);
        return user;
    }
}