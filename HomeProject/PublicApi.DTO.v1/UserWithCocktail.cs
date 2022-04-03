using System;
using BLLAppDTO = BLL.App.DTO;

namespace PublicApi.DTO.v1
{
    public class UserWithCocktail
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid CocktailId { get; set; }
        
        public string CocktailName { get; set; } = default!;
        
        public Guid GradeId { get; set; }
        
        public string GradeValue { get; set; } = default!;
        
        public string Comment { get; set; } = default!;
    }
    
    public class UserWithCocktailCreate
    {
        public Guid UserId { get; set; }
        
        public Guid CocktailId { get; set; }
        
        public Guid GradeId { get; set; }
        
        public string Comment { get; set; } = default!;
    }

    public class UserWithCocktailUpdate
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid CocktailId { get; set; }
        
        public Guid GradeId { get; set; }
        
        public string Comment { get; set; } = default!;
    }
}
