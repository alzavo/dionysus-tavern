using System;

namespace PublicApi.DTO.v1
{
    public class Additive
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        
        public int CocktailsCount { get; set; }
    }
    
    public class AdditiveUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public class AdditiveCreate
    {
        public string Name { get; set; } = default!;
    }
}