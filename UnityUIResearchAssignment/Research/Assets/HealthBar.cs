using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarUI : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public string displayName = "NPC";

    private ProgressBar healthBar;
    private Label nameLabel;
    private Camera mainCam;

    void Awake()
    {
        var doc = GetComponent<UIDocument>();
        var root = doc.rootVisualElement;

        healthBar = root.Q<ProgressBar>("healthBar");
        nameLabel = root.Q<Label>("nameLabel");

        nameLabel.text = displayName;
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
        }

        healthBar.value = currentHealth;
        healthBar.highValue = maxHealth;

        float t = Mathf.Clamp01(currentHealth / maxHealth);
        Color barColor = Color.Lerp(Color.red, Color.green, t);
        healthBar.Q(null, "unity-progress-bar__progress").style.backgroundColor = new StyleColor(barColor);
    }
}