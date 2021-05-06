using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Web.Converters;
using Blog.Web.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Blog.Test.Converters
{
    [TestClass]
    public class CategoryConvertersTest
    {
        [TestMethod]
        public void EntityToDto()
        {
            Category category = new()
            {
                Id = 1,
                Name = "Sport",
                SerializedAgeGroups = (new AgeGroup[] { AgeGroup.YOUNG, AgeGroup.ADULT }).ToJson(),
            };

            CategoryDTO categoryDTO = category.EntityToDto();
            Assert.IsNotNull(categoryDTO);
            Assert.AreEqual(category.Id, categoryDTO.Id);
            Assert.AreEqual(category.Name, categoryDTO.Name);
            Assert.AreEqual(category.SerializedAgeGroups.FromJson<AgeGroup[]>().First(), categoryDTO.AgeGroups.First());
            Assert.AreEqual(category.SerializedAgeGroups.FromJson<AgeGroup[]>().Last(), categoryDTO.AgeGroups.Last());
        }

        [TestMethod]
        public void DtoToEntity()
        {
            CategoryDTO categoryDTO = new()
            {
                Id = 1,
                AgeGroups = new AgeGroup[] { AgeGroup.ELDER, AgeGroup.ELDER },
                Name = "Sport"
            };

            Category category = categoryDTO.DtoToEntity();
            Assert.IsNotNull(category);
            Assert.AreEqual(categoryDTO.Id, category.Id);
            Assert.AreEqual(categoryDTO.Name, category.Name);
            Assert.AreEqual(categoryDTO.AgeGroups.First(), category.SerializedAgeGroups.FromJson<AgeGroup[]>().First());
            Assert.AreEqual(categoryDTO.AgeGroups.Last(), category.SerializedAgeGroups.FromJson<AgeGroup[]>().Last());
        }
    }
}
