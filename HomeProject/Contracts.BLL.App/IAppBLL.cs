using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IAdditiveService Additives { get; }
        IAdditiveInCocktailService  AdditivesInCocktails { get; }
        IAmountUnitService  AmountUnits { get; }
        ICocktailService  Cocktails { get; }
        IDrinkInCocktailService  DrinkInCocktails { get; }
        IDrinkService Drinks { get; }
        IDrinkTypeService  DrinksTypes { get; }
        IGradeService  Grades { get; }
        IStepService  Steps { get; }
        IUserWithCocktailService  UsersWithCocktails { get; }
        IUserService  Users { get; }
    }
}