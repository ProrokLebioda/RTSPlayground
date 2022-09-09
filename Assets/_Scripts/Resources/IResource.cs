public delegate void Created(ResourceType resType);
public delegate void Used(ResourceType resType);
public interface IResource
{

    public static Created OnCreated;
    public static Used OnUsed;
    
    public bool IsInUse { get; set; }

    ResourceType ResType { get; set; }

    public void CreateResource();
    public void UseResource();
}
