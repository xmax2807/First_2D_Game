using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraManager : MonoBehaviour{
    public static CameraManager Instance;
    public Cinemachine.CinemachineVirtualCamera CinemachineCam {get;private set;}
    public static float CamAspectRatio => Camera.main.aspect;
    [SerializeField] private PolygonCollider2D WorldBoundaries;
    [SerializeField] private float defaultOrthographicSize = 7f;

    private float WorldWidth, WorldHeight;
    private Vector2 TopLeft, BottomRight;
    private float maxOrthographicSize;
    private Transform CenterPoint;
    private void Awake(){
        if(Instance == null) Instance = this;
        CinemachineCam = GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        CinemachineCam.m_Lens.OrthographicSize = defaultOrthographicSize;
        Vector2[] Corners = WorldBoundaries.GetPath(0);
        TopLeft = new Vector2(Corners[0].x, Corners[0].y);
        BottomRight = new Vector2(Corners[2].x, Corners[2].y);

        WorldHeight = Mathf.Abs(TopLeft.y - BottomRight.y);
        WorldWidth = Mathf.Abs(TopLeft.x - BottomRight.x);

        maxOrthographicSize = WorldHeight / 2 - 0.01f;
    }   
    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.sceneUnloaded += OnSceneUnLoaded; 
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //SceneManager.sceneUnloaded -= OnSceneUnLoaded; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        var player = FindObjectOfType<Player>();
        if(player == null) return;

        CinemachineCam.Follow = player.transform;
        CinemachineCam.LookAt = player.transform;
    }

    public bool CheckIsReachBoundary(){
        var mainCamPos = Camera.main.transform.position;
        bool isReachWidth;

        if(mainCamPos.x > WorldWidth/2){
            isReachWidth = Mathf.Approximately(mainCamPos.x, BottomRight.x - defaultOrthographicSize*CamAspectRatio);
        }
        else {
            isReachWidth = Mathf.Approximately(mainCamPos.x, TopLeft.x + defaultOrthographicSize*CamAspectRatio);
        }
        return isReachWidth;

        //bool isReachHeight;
        // Debug.Log(isReachWidth);
        // if(isReachWidth) return true;

        // if(mainCamPos.y > WorldHeight/2){
        //     isReachHeigth = Mathf.Approximately(mainCamPos.y, TopLeft.y - defaultOrthographicSize);
        // }
        // else{
        //     isReachHeigth = Mathf.Approximately(mainCamPos.y, BottomRight.y + defaultOrthographicSize);
        // }
        // return isReachHeigth;
    }

    public void SetOrthographicSize(float value) {
        if(CheckIsReachBoundary()){
            CinemachineCam.m_Lens.OrthographicSize = defaultOrthographicSize;
        }
        else CinemachineCam.m_Lens.OrthographicSize = Mathf.Clamp(value,defaultOrthographicSize, maxOrthographicSize);
    }

    public void SetOrthographicDefaultSize() => CinemachineCam.m_Lens.OrthographicSize = defaultOrthographicSize;

    public void FollowTarget(Vector2 position){
        if(CenterPoint == null){
            CenterPoint = Instantiate(new GameObject(), position, Quaternion.identity).transform;
        }
        CenterPoint.position = position;
        FollowTarget(CenterPoint);
    }
    public void FollowTarget(Transform target){
        CinemachineCam.Follow = null;
        CinemachineCam.Follow = target;
    }

    //private void OnSceneUnLoaded(Scene scene){}


}