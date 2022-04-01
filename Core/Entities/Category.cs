namespace Core.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; private set; }

        public Category()
        {
            
        }

        public Category(string name)
        {
            Name = name;
        }

        
    }
}