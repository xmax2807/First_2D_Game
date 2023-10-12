using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollingImage : MonoBehaviour
{
    private RawImage background;
    [SerializeField] private Vector2 direction = new Vector2(0.1f, 0);
    void Awake(){
        background = GetComponent<RawImage>();
    }
    // Update is called once per frame
    void Update()
    {
        background.uvRect = new Rect(background.uvRect.position + direction * Time.deltaTime, background.uvRect.size);
    }
}
