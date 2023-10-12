using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventItemDataNode : ScriptableObject
{
    public abstract string Name {get;}
    public virtual string[] CustomPortNames {get;}
}
