using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class Grade : DomainEntityId
    {
        public string GradeValue { get; set; } = default!;
        
        public ICollection<UserWithCocktail>? UsersWithCocktails { get; set; }
    }
}
