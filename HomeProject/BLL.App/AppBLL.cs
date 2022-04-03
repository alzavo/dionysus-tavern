using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }
        
        public IAdditiveService Additives => 
            GetService<IAdditiveService>(() => new AdditiveService(Uow, Uow.Additives, Mapper));

        public IAdditiveInCocktailService AdditivesInCocktails => 
            GetService<IAdditiveInCocktailService>(() => new AdditiveInCocktailService(Uow, Uow.AdditivesInCocktails, Mapper));

        public IAmountUnitService AmountUnits =>
            GetService<IAmountUnitService>(() => new AmountUnitService(Uow, Uow.AmountUnits, Mapper));

        public ICocktailService Cocktails =>
            GetService<ICocktailService>(() => new CocktailService(Uow, Uow.Cocktails, Mapper));

        public IDrinkInCocktailService DrinkInCocktails =>
            GetService<IDrinkInCocktailService>(() => new DrinkInCocktailService(Uow, Uow.DrinkInCocktails, Mapper));
        
        public IDrinkService Drinks =>
            GetService<IDrinkService>(() => new DrinkService(Uow, Uow.Drinks, Mapper));
        
        public IDrinkTypeService DrinksTypes =>
            GetService<IDrinkTypeService>(() => new DrinkTypeService(Uow, Uow.DrinksTypes, Mapper));
        
        public IGradeService Grades =>
            GetService<IGradeService>(() => new GradeService(Uow, Uow.Grades, Mapper));

        public IStepService Steps =>
            GetService<IStepService>(() => new StepService(Uow, Uow.Steps, Mapper));
        
        public IUserWithCocktailService UsersWithCocktails =>
            GetService<IUserWithCocktailService>(() => new UserWithCocktailService(Uow, Uow.UsersWithCocktails, Mapper));
        
        public IUserService Users =>
            GetService<IUserService>(() => new UserService(Uow, Uow.Users, Mapper));
    }
}
