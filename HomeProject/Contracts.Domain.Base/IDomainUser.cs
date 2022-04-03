using System;
using Microsoft.AspNetCore.Identity;

namespace Contracts.Domain.Base
{
    public interface IDomainUser<TUser> : IDomainUser<Guid, TUser> 
        where TUser : IdentityUser<Guid>
    {
    }

    public interface IDomainUser<TKey, TUser> 
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<Guid>
    {
        TUser User { get; set; }
    }
}