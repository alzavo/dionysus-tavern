using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using Moq;
using Xunit;
using Xunit.Abstractions;
using BLLAdditive = BLL.App.DTO.Additive;
using DALAdditive = DAL.App.DTO.Additive;

namespace TestProject.UnitTests
{
    public class BaseEntityServiceAdditiveUnitTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Mock<IAppUnitOfWork> _mockedAppUnitOfWork;
        private readonly Mock<IBaseRepository<DALAdditive>> _mockedBaseRepository;
        private readonly Mock<IBaseMapper<BLLAdditive, DALAdditive>> _mockedMapper;

        private readonly BaseEntityService<IAppUnitOfWork, IBaseRepository<DALAdditive>, BLLAdditive, DALAdditive> _baseEntityService;
        
        private enum MethodName
        {
            GetAllAsync,
            FirstOrDefaultAsync,
            Add,
            Update,
            Remove,
            RemoveAsync,
            ExistAsync
        }

        // ARRANGE - common
        public BaseEntityServiceAdditiveUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            // mocked objects
            _mockedAppUnitOfWork = new Mock<IAppUnitOfWork>();
            
            _mockedBaseRepository = new Mock<IBaseRepository<DALAdditive>>();
            SetupForMockedRepo(_mockedBaseRepository, MethodName.GetAllAsync);
            SetupForMockedRepo(_mockedBaseRepository, MethodName.FirstOrDefaultAsync);
            SetupForMockedRepo(_mockedBaseRepository, MethodName.Add);
            SetupForMockedRepo(_mockedBaseRepository, MethodName.Update);
            SetupForMockedRepo(_mockedBaseRepository, MethodName.Remove);
            SetupForMockedRepo(_mockedBaseRepository, MethodName.RemoveAsync);
            SetupForMockedRepo(_mockedBaseRepository, MethodName.ExistAsync);
            
            _mockedMapper = new Mock<IBaseMapper<BLLAdditive, DALAdditive>>();
            SetupForMockedMapper(_mockedMapper, MethodName.GetAllAsync);
            SetupForMockedMapper(_mockedMapper, MethodName.FirstOrDefaultAsync);
            SetupForMockedMapper(_mockedMapper, MethodName.Add);
            SetupForMockedMapper(_mockedMapper, MethodName.Update);
            SetupForMockedMapper(_mockedMapper, MethodName.Remove);
            SetupForMockedMapper(_mockedMapper, MethodName.RemoveAsync);
            SetupForMockedMapper(_mockedMapper, MethodName.ExistAsync);
            
            _baseEntityService =
                new BaseEntityService<IAppUnitOfWork, IBaseRepository<DALAdditive>, BLLAdditive, DALAdditive>
                    (_mockedAppUnitOfWork.Object, _mockedBaseRepository.Object, _mockedMapper.Object);
        }

        [Fact]
        public async void Test_Get_All_Async_Method()
        {
            // ACT
            var result = (await _baseEntityService.GetAllAsync()).ToList();
            
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Lime", result[0].Name);
            Assert.Equal("Ice", result[1].Name);
            
            _mockedBaseRepository.Verify(x => x
                .GetAllAsync(
                    It.Is<Guid>(a => a == default),
                    It.Is<bool>(a=> a)), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Ice")), Times.Once);
        }
        
        [Fact]
        public async void Test_First_Or_Default_Async_Method()
        {
            // ACT
            var result = await _baseEntityService.FirstOrDefaultAsync(Guid.Empty);
            
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Lime", result!.Name);

            _mockedBaseRepository.Verify(x => x
                .FirstOrDefaultAsync(
                    It.Is<Guid>(a => a == Guid.Empty),
                    It.Is<Guid>(a => a == default),
                    It.Is<bool>(a=> a)), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
        }
        
        [Fact]
        public void Test_Add_Method()
        {
            // ACT
            var result = _baseEntityService.Add(new BLL.App.DTO.Additive {Name = "Lime"});
            
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Lime", result!.Name);

            _mockedBaseRepository.Verify(x => x
                .Add(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<BLL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
        }
        
        [Fact]
        public void Test_Update_Method()
        {
            // ACT
            var result = _baseEntityService.Update(new BLL.App.DTO.Additive {Name = "Lime"});
            
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Lime", result!.Name);

            _mockedBaseRepository.Verify(x => x
                .Update(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<BLL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
        }
        
        [Fact]
        public void Test_Remove_Method()
        {
            // ACT
            var result = _baseEntityService.Remove(new BLL.App.DTO.Additive {Name = "Lime"});
            
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Lime", result!.Name);

            _mockedBaseRepository.Verify(x => x
                .Remove(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime"), 
                    It.Is<Guid>(a => a == default)), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<BLL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
        }
        
        [Fact]
        public async void Test_Remove_Async_Method()
        {
            // ACT
            var result = await _baseEntityService.RemoveAsync(Guid.Empty);
            
            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Lime", result!.Name);

            _mockedBaseRepository.Verify(x => x.RemoveAsync(
                    It.Is<Guid>(a => a == Guid.Empty), 
                    It.Is<Guid>(a => a == default)), 
                Times.Once);
            
            _mockedMapper.Verify(x => x
                .Map(It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")), Times.Once);
        }
        
        [Fact]
        public async void Test_Exist_Async_Method()
        {
            // ACT
            var resultExpectTrue = await _baseEntityService.ExistsAsync(Guid.Empty);
            var resultExpectFalse = await _baseEntityService.ExistsAsync(Guid.Parse("b847c33b-2590-4cf8-bf77-f24e9728650b"));

            // ASSERT
            Assert.IsType<bool>(resultExpectTrue);
            Assert.IsType<bool>(resultExpectFalse);
            Assert.True(resultExpectTrue);
            Assert.False(resultExpectFalse);

            _mockedBaseRepository.Verify(x => x.ExistsAsync(
                    It.Is<Guid>(a => a == Guid.Empty), 
                    It.Is<Guid>(a => a == default)), 
                Times.Once);
            
            _mockedBaseRepository.Verify(x => x.ExistsAsync(
                    It.Is<Guid>(a => a == Guid.Parse("b847c33b-2590-4cf8-bf77-f24e9728650b")), 
                    It.Is<Guid>(a => a == default)), 
                Times.Once);
        }
        
        private static IEnumerable<DAL.App.DTO.Additive> ListOfDalAdditives()
        {
            List<DAL.App.DTO.Additive> additives = new()
            {
                new DAL.App.DTO.Additive {Name = "Lime"},
                new DAL.App.DTO.Additive {Name = "Ice"},
            };
            
            return additives;
        }

        private static void SetupForMockedRepo(Mock<IBaseRepository<DALAdditive>> mockedBaseRepository, MethodName methodName)
        {
            switch (methodName)
            {
                case MethodName.GetAllAsync:
                    mockedBaseRepository.Setup(x => x.GetAllAsync(
                                It.Is<Guid>(a => a == default),
                                It.Is<bool>(a => a)))
                        .Returns(Task.FromResult(ListOfDalAdditives()));
                    break;
                
                case MethodName.FirstOrDefaultAsync:
                    mockedBaseRepository.Setup(x => x.FirstOrDefaultAsync(
                                It.Is<Guid>(a => a == Guid.Empty),
                                It.Is<Guid>(a => a == default),
                                It.Is<bool>(a => a)))
#pragma warning disable 8620
                        .Returns(Task.FromResult(new DAL.App.DTO.Additive {Name = "Lime"}));
#pragma warning restore 8620
                    break;
                
                case MethodName.Add:
                    mockedBaseRepository.Setup(x => x.Add(
                            It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")))
                        .Returns(new DAL.App.DTO.Additive {Name = "Lime"});
                    break;
                
                case MethodName.Update:
                    mockedBaseRepository.Setup(x => x.Update(
                            It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")))
                        .Returns(new DAL.App.DTO.Additive {Name = "Lime"});
                    break;
                
                case MethodName.Remove:
                    mockedBaseRepository.Setup(x => x.Remove(
                            It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime"),
                            It.Is<Guid>(a => a == default)))
                        .Returns(new DAL.App.DTO.Additive {Name = "Lime"});
                    break;
                
                case MethodName.RemoveAsync:
                    mockedBaseRepository.Setup(x => x.RemoveAsync(
                            It.Is<Guid>(a => a == Guid.Empty),
                            It.Is<Guid>(a => a == default)))
                        .Returns(Task.FromResult(new DAL.App.DTO.Additive {Name = "Lime"}));
                    break;
                
                case MethodName.ExistAsync:
                    mockedBaseRepository.Setup(x => x.ExistsAsync(
                            It.Is<Guid>(a => a == Guid.Empty),
                            It.Is<Guid>(a => a == default)))
                        .Returns(Task.FromResult(true));
                    
                    mockedBaseRepository.Setup(x => x.ExistsAsync(
                            It.Is<Guid>(a => a == Guid.Parse("b847c33b-2590-4cf8-bf77-f24e9728650b")),
                            It.Is<Guid>(a => a == default)))
                        .Returns(Task.FromResult(false));
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(methodName), methodName, null);
            }
        }

        private static void SetupForMockedMapper(Mock<IBaseMapper<BLLAdditive, DALAdditive>> mockedMapper, MethodName methodName)
        {
            switch (methodName)
            {
                case MethodName.GetAllAsync: case MethodName.FirstOrDefaultAsync:
                    mockedMapper.Setup(x => x.Map(
                            It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")))
                        .Returns(new BLL.App.DTO.Additive {Name = "Lime"});
            
                    mockedMapper.Setup(x => x.Map(
                            It.Is<DAL.App.DTO.Additive>(a => a.Name == "Ice")))
                        .Returns(new BLL.App.DTO.Additive {Name = "Ice"});
                    break;
                
                case MethodName.Add: case MethodName.Update: case MethodName.Remove:
                    mockedMapper.Setup(x => x.Map(
                            It.Is<BLL.App.DTO.Additive>(a => a.Name == "Lime")))
                        .Returns(new DAL.App.DTO.Additive {Name = "Lime"});
                    
                    mockedMapper.Setup(x => x.Map(
                            It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")))
                        .Returns(new BLL.App.DTO.Additive {Name = "Lime"});
                    break;
                
                case MethodName.RemoveAsync:
                    mockedMapper.Setup(x => x.Map(
                            It.Is<DAL.App.DTO.Additive>(a => a.Name == "Lime")))
                        .Returns(new BLL.App.DTO.Additive {Name = "Lime"});
                    break;
                
                case MethodName.ExistAsync:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(methodName), methodName, null);
            }
        }
    }
}
