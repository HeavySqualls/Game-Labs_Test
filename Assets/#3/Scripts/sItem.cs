using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "Ship Equipment", order = 1)]
public class sItem : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;

    public float damage;
    public float reloadTime;
}
