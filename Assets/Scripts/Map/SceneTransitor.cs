using UnityEngine;
using System.Linq;
public class SceneTransitor : ObjectEffectTrigger<Player>
{

    [SerializeField] protected Transform SpawnPosition;
    public enum SceneTransitionType
    {
        NextScene, PreviousScene, SpecificScene, None
    }

    public SceneTransitionType transitionType;
    public string SceneName;

    protected override void TriggerEffect(Player target)
    {
        switch (transitionType)
        {
            case SceneTransitionType.NextScene:
                GameManager.Instance.LoadNextScene(() => SpawnPlayerAfterLoadedScene(SceneTransitionType.PreviousScene));
                break;
            case SceneTransitionType.PreviousScene:
                GameManager.Instance.LoadPreviousScene(() => SpawnPlayerAfterLoadedScene(SceneTransitionType.NextScene));
                break;
            case SceneTransitionType.SpecificScene:
                GameManager.Instance.LoadScene(name, () => SpawnPlayerAfterLoadedScene(SceneTransitionType.PreviousScene));
                break;
        }
    }

    protected void SpawnPlayerAfterLoadedScene(SceneTransitionType targetTransitionType)
    {
        var transitor = FindObjectsOfType<SceneTransitor>().FirstOrDefault((x) => x.transitionType == targetTransitionType);
        var player = FindObjectOfType<Player>();
        
        if (transitor == null || player == null) return;

        player.transform.position = transitor.SpawnPosition == null ? Vector3.zero : transitor.SpawnPosition.position;

    }
}