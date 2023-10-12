using UnityEngine;
using UnityEditor;
using System;

public class SkillItem : FloatingItem, ISavableObject
{
    private Animator childAnimator;
    [SerializeField] private SpriteRenderer childRenderer;
    [SerializeField] private UniqueItemId UniqueItemId;
    [SerializeField] private ModalData modalData;

    private Vector4 alphaOnly = new Vector4(0, 0, 0, 0);

    public event Action OnDestroyed;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _player != null)
        {
            return;
        }
        _player = other.GetComponent<Player>();
        childAnimator = GetComponentInChildren<Animator>();
        childAnimator.SetBool("isDead", true);
        
        InputManager.Controls.GamePlay.Disable();
        _player.GetComponentInChildren<Core>().Movement.SetVectorZero();
        TimeManager.Instance.WaitUntil(() => isApplied == true, InputManager.Controls.GamePlay.Enable);
    }

    protected override void Update()
    {
        base.Update();
        if (_player == null) return;

        if (childRenderer.color.a >= 1f)
        {// visible
            transform.position = Vector2.MoveTowards(transform.position, _player.GetPosition(), 5f * Time.deltaTime);
            if (Vector2.Distance(transform.position, _player.GetPosition()) <= 0.1f)
            {
                ApplyItem(_player);
            }
        }
        else
        {
            alphaOnly.Set(0, 0, 0, Mathf.Min(0.6f * Time.deltaTime, 1));
            childRenderer.color += (Color)alphaOnly;
        }
    }

    public void TriggerAnimationDone()
    {
        //isChildAnimatorDone = true;
        childAnimator.gameObject.SetActive(false);
        //ApplyItem(_player);
    }
    protected override void ApplyItem(Player player)
    {
        OnDestroyState();
        //add 1 skill point
        Core core = player.Core;
        core.UIBarComponent.ExpandBar(UIBarComponent.UIType.HP);
        core.PlayerGameData.Data.savedObjectIDs.Add(GetID());
    }

    protected override void OnDestroyState()
    {
        ToggleVisual(false);

        int i = 0;
        System.Action SpawnEffect = ()=>{
            SpawnSystem.Instance.SpawnObject(DestroyEffect, _player.GroundPosition());
        };
        while(i < 1){
            TimeManager.Instance.WaitForSeconds(0.01f*i, SpawnEffect);
            i++;
        }
        OnDestroyed?.Invoke();
        Destroy(gameObject);
        
        var modal = ModalManager.Instance.BuiltModal(ModalManager.ModalType.GameTutorialModal) as NavigationModal;
        modal.OnCloseModal += ()=>isApplied = true;
        modal.ShowModal(modalData);
    }

    public bool IsActive() =>  this.enabled || this.gameObject.activeSelf && !isApplied;

    public string GetID() => UniqueItemId.Id;

    public void ToggleActive(bool value) => gameObject.SetActive(value);

    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}