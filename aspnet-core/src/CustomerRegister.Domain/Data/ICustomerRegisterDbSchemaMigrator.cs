using System.Threading.Tasks;

namespace CustomerRegister.Data;

public interface ICustomerRegisterDbSchemaMigrator
{
    Task MigrateAsync();
}
