using Restfull.Domain.Entities;

namespace Restfull.Infrastructure.Services
{
    public class ResourceManager
    {
        private readonly List<Resource> _resources = new List<Resource>();

        public IReadOnlyList<Resource> Resources => _resources.AsReadOnly();

        public void AddResource(Resource resource)
        {
            if (resource != null)
            {
                _resources.Add(resource);
            }
        }

        public void ShowResources()
        {
            foreach (var resource in _resources)
            {
                resource.ShowInfo();
                resource.ShowDetails();
                Console.WriteLine("---");
            }
        }

        public List<Resource> GetResourcesByStatus(Domain.Enums.ResourceStatus status)
        {
            return _resources.Where(r => r.Status == status).ToList();
        }

        public List<T> GetResourcesByType<T>() where T : Resource
        {
            return _resources.OfType<T>().ToList();
        }
    }
}