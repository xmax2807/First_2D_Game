using UnityEngine;
using System.Collections;
public class SpawnSystem : MonoBehaviour
{
    private static SpawnSystem _instance;
    public static SpawnSystem Instance => _instance;
    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    // private void Start(){
    //     if (_instance == null) _instance = this;
    // }
    private void OnDestroy() => StopAllCoroutines();

    
    private IEnumerator RandomSpawning(GameObject obj, Transform parentTransform,bool isAttachParent = false, int duplicate = 1)
    {
        float range = duplicate/50f;
        for (int i = 0; i < duplicate; i++)
        {
            //list[i].SetActive(true);
            
            int amount = Mathf.Min(duplicate, 10 + i);
            int j = i;
            for(; j < amount; j++){
                float x = Random.Range(-range,range);
                float y = Random.Range(0.0f, 0.5f);
                var child = Instantiate(obj, (Vector2)parentTransform.position + new Vector2(x, y) , Quaternion.Euler(0,0,Random.Range(-90f,90f)));
                
                if(isAttachParent) child.transform.parent = parentTransform; 
            }
            i = j;
            yield return new WaitForEndOfFrame();
            //yield return null;
        }
    }

    private IEnumerator Spawning(GameObject obj, Transform parentTransform,bool isAttachParent = false, int duplicate = 1)
    {
        for (int i = 0; i < duplicate; i++)
        {
            //list[i].SetActive(true);
            int amount = Mathf.Min(duplicate, 10 + i);
            int j = i;
            for(; j < amount; j++){
                var child = Instantiate(obj, parentTransform.position, parentTransform.rotation);
                
                if(isAttachParent) child.transform.parent = parentTransform; 
            }
            i = j;
            yield return new WaitForEndOfFrame();
            //yield return null;
        }
    }

    public T SpawnObject<T> (GameObject obj, Vector2 Position){
        if(obj.GetComponent<T>() == null) return default;
         
        return Instantiate(obj, Position, Quaternion.identity).GetComponent<T>();
    }
    public void SpawnObjectRandomly(GameObject obj,Vector3 Position, int duplicate = 1){
        if(obj == null) return;
        obj.transform.position = Position;
        StartCoroutine(RandomSpawning(obj, obj.transform, false, duplicate));
    }

    public void SpawnObjectRandomly(GameObject obj, Transform parentTransform,bool isAttachParent = false, int duplicate = 1)
    {
        if (obj == null || parentTransform == null) return;
        StartCoroutine(RandomSpawning(obj, parentTransform, isAttachParent, duplicate));
    }

    public void SpawnObject(GameObject obj, Transform parentTransform, bool isAttachParent = false, int duplicate = 1){
        if(obj == null || parentTransform == null) return;
        StartCoroutine(Spawning(obj, parentTransform, isAttachParent, duplicate));
    }

    public void SpawnObject(GameObject obj,Vector3 Position, int duplicate = 1){
        if(obj == null) return;
        obj.transform.position = Position;
        StartCoroutine(Spawning(obj, obj.transform, false, duplicate));
    }

    [Header("Blood")]
    [SerializeField] private ManualAnimatorController BloodAnimator;

    public void SpawnBlood(Transform parent, int duplicate = 1){
        BloodAnimator.Shuffle();
        SpawnObject(BloodAnimator.gameObject, parent, true, duplicate);
    }
    public void SpawnBlood(Vector3 Position, int duplicate = 1){
        BloodAnimator.Shuffle();
        SpawnObject(BloodAnimator.gameObject, Position, duplicate);
    }
}