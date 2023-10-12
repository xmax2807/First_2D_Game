using UnityEngine;
[System.Serializable]
public class NamedAttribute<T>{
    [SerializeField]public string Name;

    [SerializeField]public T Value;
}

public class NamedAttributeWithInfo<T> : NamedAttribute<T>{
    [SerializeField] public string Info;
}
