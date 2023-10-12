using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
public class ExpandableBar : BaseBarUI{
    [SerializeField] private ExpandableLayout layout;

    protected override void Start(){
        base.Start();
        if(layout == null) return;
        layout.enabled = false;
    }

    public void ExpandBar() {
        if(layout == null) return;
        layout.enabled =true;
        layout.ExpandObject();
        layout.enabled = false;
    }
}