using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using OneOf;
using OneOf.Types;
using TourAPI.Models.Domains;
using TourAPI.Models.Dto;

namespace BenchMark
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = BenchmarkRunner.Run<BenchMark>();
        }
    }

    public class BenchMark
    {
        IMapper mapper;
        Category category;

        [GlobalSetup]
        public void Init()
        {
            var confg = new MapperConfiguration(cfg
                => cfg.CreateMap<Category, CategoryResponse>().ReverseMap());
            mapper = confg.CreateMapper();
            category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                CreateTimes = DateTime.Now,
                Description = "Description",
                LastUpdateTimes = DateTime.Now,
                Name = "Family"
            };
        }

        [Benchmark]
        public void HandMake()
        {
            CategoryResponse response = (CategoryResponse)category;
        }

        [Benchmark]
        public void Mapper()
        {
            CategoryResponse response = mapper.Map<CategoryResponse>(category);
        }

        [Benchmark]
        public OneOf<Category, NotFound> ReturnOneOf()
        {
            if (category == null)
            {
                return new NotFound();
            }
            return category;
        }

        [Benchmark]
        public Category? ReturnWalk()
        {
            if (category == null)
            {
                return null;
            }
            return category;
        }
    }
}