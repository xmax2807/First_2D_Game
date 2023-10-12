using UnityEngine;
public class EnemyCore : BaseCore{
    [SerializeField] private EnemyUIComponent _uiComponent;
    public EnemyEntity Enemy {get;private set;}
    [SerializeField] private EnemyEntityData Stats;
    public EnemyStat EnemyStat => Stats.stat;
    public EnemyUIComponent UIComponent => _uiComponent;
    protected override void Awake()
    {
        base.Awake();
        Enemy = GetComponentInParent<EnemyEntity>();
    }
}