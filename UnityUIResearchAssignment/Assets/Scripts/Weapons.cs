using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Weapons")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float attackSpeed;
    public Sprite weaponSprite;
    public GameObject weapon;
}

[SerializeField] enum WeaponType
{
    Melee,
    Magic,
    Ranged
}
