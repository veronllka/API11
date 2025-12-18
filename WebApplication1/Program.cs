using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Mapping;
using WebApplication1.Middleware;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Responses;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connString = builder.Configuration.GetConnectionString("SqlConnection")
                ?? throw new InvalidOperationException("Не найдена строка подключения 'SqlConnection' в appsettings.json");

            builder.Services.AddDbContext<UsersDBContext>(options =>
                options.UseSqlServer(connString));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(x => x.Value?.Errors.Count > 0)
                            .ToDictionary(
                                k => k.Key,
                                v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                            );

                        var payload = ApiResponse<object>.Fail(
                            StatusCodes.Status400BadRequest,
                            "Validation failed",
                            context.HttpContext.TraceIdentifier
                        );

                        payload.Data = errors;

                        return new BadRequestObjectResult(payload);
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
