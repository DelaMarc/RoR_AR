using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RORAR.Entity;

[CreateAssetMenu(fileName = "Ship list item")]
public class ListItem : ScriptableObject
{
    public AEntity item;
    public string name = "";
}
