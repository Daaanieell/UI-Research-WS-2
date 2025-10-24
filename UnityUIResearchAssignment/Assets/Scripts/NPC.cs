using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    
    private string weaponType;
    private Sprite weaponSprite;

    [SerializeField] private Transform handTransform;
    [SerializeField] private GameObject weaponPrefab;

    private GameObject currentWeapon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public string WeaponType
    {
        get => weaponType;
        set => weaponType = value;
    }

    public Sprite WeaponSprite
    {
        get => weaponSprite;
        set => weaponSprite = value;
    }

    void EquipWeapon()
    {
        if (handTransform == null || weaponPrefab == null)
            return;

        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeapon = Instantiate(weaponPrefab, handTransform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one * 5f;
    }
}
