using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NavigationMenuData : MonoBehaviour
{
    [SerializeField]public NamedAttribute<Button.ButtonClickedEvent>[] ListCallbacks;
}
