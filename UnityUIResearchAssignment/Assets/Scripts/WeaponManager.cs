using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public Weapon weapon;

    private Text _weaponName;
    private int _damage;
    private float _attackSpeed;
    private Image _weaponSprite;
    
    // getters and setters
    public string WeaponName { get; set; }
    public int Damage  { get; set; }
    public float AttackSpeed { get; set; }
    public Sprite WeaponSprite { get; set; }
    void Start()
    {
        Debug.Log(weapon.attackSpeed);
    }
}
