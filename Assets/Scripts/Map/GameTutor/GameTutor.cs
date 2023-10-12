using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutor : ObjectEffectTrigger<Player>
{
    [SerializeField] private GameTutorData Data;

    private NavigationModal modal;
    private void Start(){
        modal = ModalManager.Instance.BuiltModal(ModalManager.ModalType.GameTutorialModal) as NavigationModal;
    }
    protected override void TriggerEffect(Player target)
    {
        modal.OnCloseModal += DestroyState;
        modal.ShowModal(Data.ModalDatas);
    }

    private void DestroyState(){
        modal.OnCloseModal = null;
        Destroy(this.gameObject);
    }
}
