using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }
        
        public IAdditiveRepository Additives => GetRepository(() => new AdditiveRepository(UowDbContext, Mapper));
        public IAdditiveInCocktailRepository AdditivesInCocktails => GetRepository(() => new AdditiveInCocktailRepository(UowDbContext, Mapper));
        public IAmountUnitRepository AmountUnits => GetRepository(() => new AmountUnitRepository(UowDbContext, Mapper));
        public ICocktailRepository Cocktails => GetRepository(() => new CocktailRepository(UowDbContext, Mapper));
        public IDrinkInCocktailRepository DrinkInCocktails => GetRepository(() => new DrinkInCocktailRepository(UowDbContext, Mapper));
        public IDrinkRepository Drinks => GetRepository(() => new DrinkRepository(UowDbContext, Mapper));
        public IDrinkTypeRepository DrinksTypes => GetRepository(() => new DrinkTypeRepository(UowDbContext, Mapper)); 
        public IGradeRepository Grades => GetRepository(() => new GradeRepository(UowDbContext, Mapper));
        public IStepRepository Steps => GetRepository(() => new StepRepository(UowDbContext, Mapper));
        public IUserWithCocktailRepository UsersWithCocktails => GetRepository(() => new UserWithCocktailRepository(UowDbContext, Mapper));
        public IUserRepository Users => GetRepository(() => new UserRepository(UowDbContext, Mapper));
    }
}
