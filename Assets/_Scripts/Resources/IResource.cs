public delegate void Created(string name);
public delegate void Used(string name);
public interface IResource
{

    public static Created OnCreated;
    public static Used OnUsed;

    public void CreateResource();
    public void UseResource();
}
