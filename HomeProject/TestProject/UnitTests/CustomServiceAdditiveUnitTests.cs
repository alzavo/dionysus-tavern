using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Services;
using Moq;
using Xunit.Abstractions;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Xunit;

namespace TestProject.UnitTests
{
    public class CustomServiceAdditiveUnitTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly Mock<IAppUnitOfWork> _mockedAppUnitOfWork;
        private readonly Mock<IAdditiveRepository> _mockedAdditiveRepository;
        private readonly Mock<IMapper> _mockedMapper;

        private readonly AdditiveService _additiveService;

        // ARRANGE - common
        public CustomServiceAdditiveUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            // mocked objects
            _mockedAppUnitOfWork = new Mock<IAppUnitOfWork>();
            
            /////////////////////////
            _mockedAdditiveRepository = new Mock<IAdditiveRepository>();

            _mockedAdditiveRepository.Setup(x => x
                    .GetAllWithCocktailsCountAsync(It.Is<bool>(a => a)))
                    .Returns(Task.FromResult(ListOfDalAdditives()));
            
            _mockedAdditiveRepository.Setup(x => x
                    .GetOneWithCocktailsCountAsync(
                        It.Is<Guid>(a => a == Guid.Empty),
                        It.Is<bool>(a => a)))
#pragma warning disable 8620
                .Returns(Task.FromResult(new DAL.App.DTO.Additive {Name = "Lime", CocktailsCount = 3}));
#pragma warning restore 8620
            
            /////////////
            _mockedMapper = new Mock<IMapper>();

            _mockedMapper.Setup(x => x
                    .Map<BLL.App.DTO.Additive>(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")))
                    .Returns(new BLL.App.DTO.Additive {Name = "Lime", CocktailsCount = 3});
            
            _mockedMapper.Setup(x => x
                    .Map<BLL.App.DTO.Additive>(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Ice")))
                    .Returns(new BLL.App.DTO.Additive {Name = "Ice", CocktailsCount = 4});

            // SUT
            _additiveService = new AdditiveService(
                _mockedAppUnitOfWork.Object, 
                _mockedAdditiveRepository.Object,
                _mockedMapper.Object);
        }

        [Fact]
        public async void Test_Get_All_With_Cocktails_Count_Async_Method()
        {
            // ACT
            var result = (await _additiveService.GetAllWithCocktailsCountAsync()).ToList();

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Lime", result[0].Name);
            Assert.Equal(3, result[0].CocktailsCount);
            Assert.Equal("Ice", result[1].Name);
            Assert.Equal(4, result[1].CocktailsCount);

            _mockedAdditiveRepository.Verify(x => x
                .GetAllWithCocktailsCountAsync(It.Is<bool>(a=> a)), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map<BLL.App.DTO.Additive>(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map<BLL.App.DTO.Additive>(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Ice")), Times.Once);
        }
        
        [Fact]
        public async void Test_Get_One_With_Cocktails_Count_Async_Method()
        {
            // ACT
            var result = await _additiveService.GetOneWithCocktailsCountAsync(Guid.Empty);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Lime", result!.Name);
            Assert.Equal(3, result.CocktailsCount);

            _mockedAdditiveRepository.Verify(x => x
                .GetOneWithCocktailsCountAsync(
                    It.Is<Guid>(a=> a == Guid.Empty),
                    It.Is<bool>(a=> a)), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map<BLL.App.DTO.Additive>(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
        }

        private static IEnumerable<DAL.App.DTO.Additive> ListOfDalAdditives()
        {
            List<DAL.App.DTO.Additive> additives = new()
            {
                new DAL.App.DTO.Additive {Name = "Lime", CocktailsCount = 3},
                new DAL.App.DTO.Additive {Name = "Ice", CocktailsCount = 4},
            };
            
            return additives;
        }
    }
}
