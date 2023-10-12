using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private Sprite layer1;
    [SerializeField] private Sprite layer2;
    [SerializeField] private Sprite layer3;
    [SerializeField] private int Duplicates;
    [SerializeField] private int sortingOrderStart;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distance;
    [SerializeField] private float scale;

    private void PopulateObjects()
    {
        for (int i = 0; i < Duplicates; i++)
        {
            var child = new GameObject($"Background{i + 1}");
            child.transform.SetParent(transform, false);
            child.transform.localPosition = new Vector2(distance * i, 0);
            //child.transform.localScale = new Vector2(scale);

            var gameObjectLayer1 = CreateGameObjectLayer(layer1, sortingOrderStart, distance * i, "Layer1");
            var gameObjectLayer2 = CreateGameObjectLayer(layer2, sortingOrderStart + 1, distance * i, "Layer2");
            var gameObjectLayer3 = CreateGameObjectLayer(layer3, sortingOrderStart + 2, distance * i, "Layer3");

            gameObjectLayer1.transform.SetParent(child.transform, false);
            gameObjectLayer2.transform.SetParent(child.transform, false);
            gameObjectLayer3.transform.SetParent(child.transform, false);
        }
    }
    private GameObject CreateGameObjectLayer(Sprite sprite, int sortingOrder, float distance, string name)
    {
        var gameoject = new GameObject(name);
        var component = gameoject.AddComponent<SpriteRenderer>();
        component.sprite = sprite;
        component.sortingOrder = sortingOrder;
        return gameoject;
    }

    void OnValidate()
    {
        if (Duplicates < 0) Duplicates = 0;

        // if(_duplicates != Duplicates){

        // }
        foreach (Transform child in gameObject.transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }
        PopulateObjects();
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }
}
