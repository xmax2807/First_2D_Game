using UnityEngine;
public class AutoCollectableItem : SelfDestructionItem{
    private bool isDetected = false;
    private Player _player;
    protected bool startUpdating => isDetected && _player != null;
    protected virtual void Update(){
        if(!startUpdating) return;

        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, 30 * Time.deltaTime);
        
    }

    private void FixedUpdate(){
        if(!startUpdating) return;

        if(Vector2.Distance(transform.position, _player.transform.position) <= 0.8f){
            ApplyItem(_player);
        }
    }

    public override void Trigger(Player player){
        isDetected = true;
        _player = player;
    }
    protected override void OnDestroyState()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.SoundFX.InGameSoundFX.ItemCollected.Clip);
        //Item Destroyed after this
        base.OnDestroyState();

    }
}