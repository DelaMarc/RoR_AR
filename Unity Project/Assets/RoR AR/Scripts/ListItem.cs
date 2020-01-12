using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship list item")]
public class ListItem : ScriptableObject
{
    public GameObject item;
    public string name = "";
}
