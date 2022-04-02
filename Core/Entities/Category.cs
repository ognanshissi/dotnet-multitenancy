using Core.Contracts;

namespace Core.Entities
{
    public class Category: BaseEntity, IMustHaveTenant
    {
        public string Name { get; private set; }

        public Category()
        {
            
        }

        public Category(string name)
        {
            Name = name;
        }


        public string TenantId { get; set; }
    }
}