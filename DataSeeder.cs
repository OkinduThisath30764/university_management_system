using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BlindMatchPAS.Core.Constants;
using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure DB created
            context.Database.EnsureCreated();

            string[] roles = { Roles.Student, Roles.Supervisor, Roles.ModuleLeader, Roles.SystemAdmin };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed System Admin
            var adminEmail = "admin@pas.lk";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Administrator",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.SystemAdmin);
                }
            }

            // Seed Module Leader
            var mlEmail = "ml@pas.lk";
            if (await userManager.FindByEmailAsync(mlEmail) == null)
            {
                var mlUser = new ApplicationUser
                {
                    UserName = mlEmail,
                    Email = mlEmail,
                    FullName = "Module Leader",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(mlUser, "Ml@1234");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mlUser, Roles.ModuleLeader);
                }
            }

            // Seed Supervisor
            var supervisorEmail = "supervisor@pas.lk";
            if (await userManager.FindByEmailAsync(supervisorEmail) == null)
            {
                var supervisor = new ApplicationUser
                {
                    UserName = supervisorEmail,
                    Email = supervisorEmail,
                    FullName = "Dr. John Doe",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(supervisor, "Sup@1234");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(supervisor, Roles.Supervisor);
                }
            }

            // Seed Student
            var studentEmail = "student@pas.lk";
            if (await userManager.FindByEmailAsync(studentEmail) == null)
            {
                var student = new ApplicationUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    FullName = "Jane Smith",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(student, "Stu@1234");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(student, Roles.Student);
                }
            }

            // Seed Research Areas
            if (!context.ResearchAreas.Any())
            {
                context.ResearchAreas.AddRange(
                    new ResearchArea { Name = "Artificial Intelligence" },
                    new ResearchArea { Name = "Web Development" },
                    new ResearchArea { Name = "Cybersecurity" },
                    new ResearchArea { Name = "Data Science" },
                    new ResearchArea { Name = "Cloud Computing" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
