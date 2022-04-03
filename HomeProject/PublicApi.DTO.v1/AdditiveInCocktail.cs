using System;

namespace PublicApi.DTO.v1
{
    public class AdditiveInCocktail
    {
        public Guid Id { get; set; }

        public Guid CocktailId { get; set; }
        public string CocktailName { get; set; } = default!;

        public Guid AdditiveId { get; set; }
        public string AdditiveName { get; set; } = default!;

        public Guid AmountUnitId { get; set; }
        public string AmountUnitName { get; set; } = default!;

        public int Amount { get; set; }
    }
    
    public class AdditiveInCocktailUpdate
    {
        public Guid Id { get; set; }

        public Guid CocktailId { get; set; }

        public Guid AdditiveId { get; set; }

        public Guid AmountUnitId { get; set; }

        public int Amount { get; set; }
    }
    
    public class AdditiveInCocktailCreate
    {
        public Guid CocktailId { get; set; }

        public Guid AdditiveId { get; set; }

        public Guid AmountUnitId { get; set; }

        public int Amount { get; set; }
    }
}