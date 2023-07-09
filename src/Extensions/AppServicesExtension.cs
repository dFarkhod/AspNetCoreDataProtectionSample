using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Virtualdars.DataProtectionSample.Repository;

namespace Virtualdars.DataProtectionSample.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddCustomDataProtection(this IServiceCollection serviceCollection)
        {
            var builder = serviceCollection
                .AddDbContext<AppDbContext>(builder =>
                {
                    builder.UseSqlServer(@"Server=.;Database=DataProtectionApp;Trusted_Connection=True;TrustServerCertificate=True;");
                    builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    builder.EnableSensitiveDataLogging();
                })
                .AddDataProtection()
                .SetApplicationName("VirtualDars.DataProtectionSample")
                .UseCryptographicAlgorithms(
                new AuthenticatedEncryptorConfiguration()
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            builder
                .AddKeyManagementOptions(options =>
                {
                    options.NewKeyLifetime = new TimeSpan(30, 0, 0, 0);
                    options.AutoGenerateKeys = true;
                })
                .PersistKeysToDbContext<AppDbContext>();
            // .PersistKeysToFileSystem(new DirectoryInfo(@"c:\temp\dataprotection-keys"))

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return serviceCollection;
        }
    }
}
