using System;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO
{
    public class Role : IdentityRole<Guid>, IDomainEntityId
    {
    }
}