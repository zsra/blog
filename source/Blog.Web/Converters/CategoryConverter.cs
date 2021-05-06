using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Web.DTOs;

namespace Blog.Web.Converters
{
    public static class CategoryConverter
    {
        public static CategoryDTO EntityToDto(this Category category) => new()
        {
            Id = category.Id,
            AgeGroups =  category.SerializedAgeGroups == null ? new AgeGroup[] { AgeGroup.DEFAULT } : JsonExtensions.FromJson<AgeGroup[]>(category.SerializedAgeGroups),
            Name = category.Name,
        };

        public static Category DtoToEntity(this CategoryDTO categoryDTO) => new()
        {
            Id = categoryDTO.Id,
            Name = categoryDTO.Name,
            SerializedAgeGroups = categoryDTO.AgeGroups.ToJson()
        };
    }
}
