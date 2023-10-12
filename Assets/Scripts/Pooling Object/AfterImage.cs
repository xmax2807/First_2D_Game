using System;
using UnityEngine;

public class AfterImage : PoolableObject
{
    [SerializeField] private int MaxImageCount;
    [SerializeField] private SpriteRenderer host;
    [SerializeField] private Color imageColor;
    [SerializeField] private float FadeMultiplier; // the smaller the quicker this sprite faded.
    [SerializeField]private float lifeTime;
    private SpriteRenderer thisGameObjetSprite;
    private Color alphaOnly;

    public override int CloneCount => MaxImageCount;
    public override float LifeTime => lifeTime;

    protected void Awake(){
        thisGameObjetSprite = GetComponent<SpriteRenderer>();
    }

    protected void Update(){
        thisGameObjetSprite.sprite = host.sprite;
        thisGameObjetSprite.color = alphaOnly;
        alphaOnly = new Color(imageColor.r,imageColor.g,imageColor.b, thisGameObjetSprite.color.a * FadeMultiplier);
    }
    protected void OnEnable(){
        if(host == null){
            enabled = false;
            return;
        }
        alphaOnly = imageColor;
        transform.position = host.transform.position;
        transform.rotation = host.transform.rotation;
    }

}