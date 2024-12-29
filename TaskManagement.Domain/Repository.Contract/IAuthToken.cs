using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Domain.Repository.Contract
{
    public interface IAuthToken
    {
       public Task<string> CreateTokenAsync(IdentityUser user , UserManager<IdentityUser> userManager);
    }
}
