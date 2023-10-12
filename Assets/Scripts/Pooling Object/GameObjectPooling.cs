using UnityEngine;
using System.Collections.Generic;
public class GameObjectPooling : MonoBehaviour{
    [SerializeField] private string Name;
    [SerializeField] private PoolableObject poolableObject;
    private Queue<GameObject> _objectPoolingQueue;
    public string GetPoolName() => Name;
    protected void Awake(){
        _objectPoolingQueue = new();
        PopulateObjects();
    }
    private void PopulateObjects(){        
        for(int i = 0; i < poolableObject.CloneCount; i++){
            var newGameObj = Instantiate(poolableObject.gameObject);
            AddGameObject(newGameObj);
        }
    }

    public void AddGameObject(GameObject newGameObj){
        if(newGameObj == null) return;
        newGameObj.SetActive(false);
        _objectPoolingQueue.Enqueue(newGameObj);
    }

    public GameObject GetGameObject(bool forcePopulate = false) {
        
        bool canDequeue = _objectPoolingQueue.TryDequeue(out var PoolObject);
        if(!canDequeue){

            if(!forcePopulate) return null;

            PopulateObjects();
            PoolObject = _objectPoolingQueue.Dequeue();
        }
        PoolObject.SetActive(true);
        TimeManager.Instance.WaitForSeconds(poolableObject.LifeTime, ()=>AddGameObject(PoolObject));
        return PoolObject;
    }
}