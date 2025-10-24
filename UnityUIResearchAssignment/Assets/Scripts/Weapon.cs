using UnityEngine;

public enum WeaponType { Melee, Ranged, Magic }

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public string name;
    public WeaponType weaponType;
    public float damage;
    public GameObject prefab;
    
}
