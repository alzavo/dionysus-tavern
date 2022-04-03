using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAdditiveRepository Additives { get; }
        IAdditiveInCocktailRepository AdditivesInCocktails { get; }
        IAmountUnitRepository AmountUnits { get; }
        ICocktailRepository Cocktails { get; }
        IDrinkInCocktailRepository DrinkInCocktails { get; }
        IDrinkRepository Drinks { get; }
        IDrinkTypeRepository DrinksTypes { get; }
        IGradeRepository Grades { get; }
        IStepRepository Steps { get; }
        IUserWithCocktailRepository UsersWithCocktails { get; }
        IUserRepository Users { get; }
    }
}
