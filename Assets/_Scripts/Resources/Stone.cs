using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : ResourceTemplate
{
    public override IEnumerator Start()
    {
        ResType = ResourceType.Stone;
        return base.Start();
    }

}
