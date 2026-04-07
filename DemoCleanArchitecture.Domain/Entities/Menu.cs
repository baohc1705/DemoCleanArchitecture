namespace DemoCleanArchitecture.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }

        public Menu? Parent {  get; set; }

        public List<Menu> Children { get; set; } = new List<Menu>();

        public List<News> News { get; set; } = new List<News>();
    }
}
