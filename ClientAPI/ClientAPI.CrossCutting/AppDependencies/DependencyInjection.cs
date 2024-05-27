using ClientAPI.Application.Members.Commands.Validations;
using ClientAPI.Domain.Abstractions;
using ClientAPI.Infrastructure.Context;
using ClientAPI.Infrastructure.RabbitMQ;
using ClientAPI.Infrastructure.RabbitMQ.Actions;
using ClientAPI.Infrastructure.Repositories;
using ClientAPI.Infrastructure.Repositories.MongoDB;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RabbitMQ.Client;
using System.Reflection;

namespace ClientAPI.CrossCutting.AppDependencies
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("test"));

            services.AddScoped<IClientRepository,ClientRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            // MongoDB
            var mongoClient = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var mongoDatabase = mongoClient.GetDatabase(configuration["MongoDB:DatabaseName"]);

            services.AddSingleton(mongoClient);
            services.AddSingleton(mongoDatabase);
            services.AddScoped<IMongoClientRepository, MongoClientRepository>();

            var myhandlers = AppDomain.CurrentDomain.Load("ClientAPI.Application");
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(myhandlers);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.Load("ClientAPI.Application"));

            // Rabbit connection
            var factory = new ConnectionFactory()
            {
                HostName = configuration["rabbitMQSettings:HostName"],
                UserName = configuration["rabbitMQSettings:UserName"],
                Password = configuration["rabbitMQSettings:Password"]
            };
            var connection = factory.CreateConnection();

            services.AddSingleton<IConnection>(connection);
            services.AddSingleton<IMessageProducer, MessageProducer>();
            services.AddHostedService<ClientConsumer>();

            // Actions
            services.AddScoped<IClientOpeationAction, CreateClientAction>();
            services.AddScoped<IClientOpeationAction, UpdateClientAction>();
            services.AddScoped<IClientOpeationAction, DeleteClientAction>();

            // Clear MongoDB collection on startup
            var serviceProvider = services.BuildServiceProvider();
            var mongoClientRepository = serviceProvider.GetRequiredService<IMongoClientRepository>();
            mongoClientRepository.ClearCollectionAsync().Wait();

            return services;
        }
    }
}
