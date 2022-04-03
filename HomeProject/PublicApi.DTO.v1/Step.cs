using System;

namespace PublicApi.DTO.v1
{
    public class Step
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = default!;
        public int IndexNumber { get; set; }
        public Guid CocktailId { get; set; }
        public string CocktailName { get; set; } = default!;
    }
    
    public class StepUpdate
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = default!;
        public int IndexNumber { get; set; }
        public Guid CocktailId { get; set; }
    }
    
    public class StepCreate
    {
        public string Description { get; set; } = default!;
        public int IndexNumber { get; set; }
        public Guid CocktailId { get; set; }
    }
}