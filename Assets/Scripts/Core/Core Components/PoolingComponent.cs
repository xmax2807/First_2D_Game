using UnityEngine;
using System.Collections.Generic;
public class PoolingComponent: CoreComponent{
    [SerializeField] private GameObjectPooling[] ListPooling;
    private Dictionary<string, GameObjectPooling> hashedList;
    
    protected void Start(){
        hashedList = new();
        foreach(var pooling in ListPooling){
            hashedList.Add(pooling.GetPoolName(), pooling);
        }
    }

    public bool TryGetGameObjectPooling(string name, out GameObjectPooling result){
        result = GetGameObjectPooling(name);
        return result != null;
    }

    public GameObjectPooling GetGameObjectPooling(string name) => hashedList[name];   
}