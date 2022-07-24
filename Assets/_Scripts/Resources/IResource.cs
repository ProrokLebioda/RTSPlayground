public delegate void Created(ResourceType resType);
public delegate void Used(ResourceType resType);
public interface IResource
{

    public static Created OnCreated;
    public static Used OnUsed;
    
    ResourceType resourceType { get;}

    public void CreateResource();
    public void UseResource();
}
