using System.Collections;

public class Wood : ResourceTemplate
{
    public override IEnumerator Start()
    {
        ResType = ResourceType.Wood;
        return base.Start();
    }

}
