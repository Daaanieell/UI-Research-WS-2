using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarUI : MonoBehaviour
{
    //public Transform target;
    //public Vector3 offset = new Vector3(0, 2f, 0);
    //public float maxHealth = 100f;
    //public float currentHealth = 100f;
    //public string displayName = "NPC";

    public NPC npcTarget;
    public Vector3 offset = new Vector3(0, 2f, 0);
    
    private ProgressBar healthBar;
    private Label nameLabel;
    private Camera mainCam;

    void Awake()
    {
        var doc = GetComponent<UIDocument>();
        var root = doc.rootVisualElement;

        healthBar = root.Q<ProgressBar>("healthBar");
        nameLabel = root.Q<Label>("nameLabel");

        //nameLabel.text = displayName;
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (npcTarget == null || npcTarget.transform == null)
            return;
        
        transform.position = npcTarget.transform.position + offset; 
        transform.rotation = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0);
        
        healthBar.highValue = npcTarget.MaxHealth;
        healthBar.value = npcTarget.Health;
        nameLabel.text = npcTarget.Name;

        float t = Mathf.Clamp01(npcTarget.Health / npcTarget.MaxHealth);
        Color barColor = Color.Lerp(Color.red, Color.green, t);
        healthBar.Q(null, "unity-progress-bar__progress").style.backgroundColor = new StyleColor(barColor);
    }
}