using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //private string name;
    //private int health;
    //private int maxHealth;
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
