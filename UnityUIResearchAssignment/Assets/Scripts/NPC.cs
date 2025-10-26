using UnityEngine;

[ExecuteAlways]
public class NPC : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string npcName;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    [Header("Weapon")]
    [SerializeField] private Transform handTransform;
    [SerializeField] private GameObject weaponPrefab;

    private GameObject currentWeapon;
    private GameObject lastWeaponPrefab;
    private bool needsRefresh;

    public string Name { get => npcName; set => npcName = value; }
    public int Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public GameObject GetWeaponPrefab() => weaponPrefab;
    public void SetWeaponPrefab(GameObject prefab)
    {
        if (weaponPrefab != prefab)
        {
            weaponPrefab = prefab;
            needsRefresh = true;
        }
    }

    private void OnEnable()
    {
        lastWeaponPrefab = weaponPrefab;
        RefreshWeapon();
    }

    private void OnValidate()
    {
        if (weaponPrefab != lastWeaponPrefab)
        {
            lastWeaponPrefab = weaponPrefab;
            needsRefresh = true;
        }
    }

    private void Update()
    {
        if (needsRefresh)
        {
            RefreshWeapon();
            needsRefresh = false;
        }

        if (!Application.isPlaying && currentWeapon != null && handTransform != null)
        {
            currentWeapon.transform.position = handTransform.position;
            currentWeapon.transform.rotation = handTransform.rotation;
        }
    }

    public void RefreshWeapon()
    {
        if (handTransform == null)
            return;

        for (int i = handTransform.childCount - 1; i >= 0; i--)
        {
            var child = handTransform.GetChild(i).gameObject;
            DestroyImmediate(child);
        }

        if (weaponPrefab == null)
        {
            currentWeapon = null;
            return;
        }

        currentWeapon = Instantiate(weaponPrefab, handTransform);
        currentWeapon.name = weaponPrefab.name + " (Equipped)";
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one * 5f;
        currentWeapon.hideFlags = HideFlags.DontSave;
        lastWeaponPrefab = weaponPrefab;
    }
}
