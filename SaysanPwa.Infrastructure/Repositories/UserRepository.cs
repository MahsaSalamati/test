using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SaysanPwa.Domain.AggregateModels.UserAggregate;
using SaysanPwa.Domain.SeedWorker;
using SqlServerWrapper.Wrapper.DatabaseManager;
using System.Data;

namespace SaysanPwa.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbManager _dbManager;

    public UserRepository(IDbManager dbManager)
    {
        _dbManager = dbManager;
    }

    public async Task<SysResult<IEnumerable<User>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<User> users = await _dbManager.CallProcedureAsync<User>("Apk_Proc_GetAllUsers");

            return new(users);
        }
        catch (Exception ex)
        {
            return new(Enumerable.Empty<User>(), false, new() { "مشگلی در واکشی اطلاعات رخ داد." });
        }
    }

    public async Task<SysResult<User>> GetUserByUserNameAsync(string username, CancellationToken cancellationToken = default)
    {
        try
        {
            User user = await _dbManager.CallSingleRowProcedureWithParametersAsync<User>("Apk_Proc_GetUserByUserName", 
                new
                {
                    username = username
                }, cancellationToken);
            if (user != null)
            {
                return new(user);
            }
            return new(null!, false, new List<string>() { "کاربر پیدا نشد." });
        }
        catch (Exception ex)
        {
            return new(null!, false, new() { ex.Message });
        }
    }
}


// TODO: add polly retry strategy. => for Mahdi