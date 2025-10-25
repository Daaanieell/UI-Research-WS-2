using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Weapons", menuName = "Weapons")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float attackSpeed;
    public Sprite weaponSprite;
    [FormerlySerializedAs("weapon")] public GameObject weaponPrefab;
    public WeaponType weaponType;
}

public enum WeaponType 
{
    Melee,
    Magic,
    Ranged
}
