using System.Collections.Generic;
using System.Linq;

public class InstancesProvider<T> where T: ProviderableObject
{
    private readonly List<T> _objects;

    public InstancesProvider(List<T> objects)
    {
        _objects = objects;
    }

    public TO GetObjectOfType<TO>() where TO: ProviderableObject
    {
        TO resultObject = null;
        
        foreach (T providerableObject in _objects)
        {
            if (providerableObject is TO seekingObject)
            {
                resultObject = seekingObject;
                break;
            }
        }

        if (resultObject == null)
            throw new($"Requested type {typeof(TO)} doesn't exist");
        
        return resultObject;
    }
}