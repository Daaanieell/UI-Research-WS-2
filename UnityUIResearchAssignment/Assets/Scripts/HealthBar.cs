using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
public class HealthBarUI : MonoBehaviour
{
    [Header("Target NPC")]
    [SerializeField] private NPC npcTarget;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);

    private UIDocument uiDoc;
    private ProgressBar healthBar;
    private Label nameLabel;
    private VisualElement progressFill;
    private Camera mainCam;

    private float lastHealth;
    private float lastMaxHealth;
    private string lastName;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        uiDoc = GetComponent<UIDocument>();

        var root = uiDoc.rootVisualElement;
        healthBar = root.Q<ProgressBar>("healthBar");
        nameLabel = root.Q<Label>("nameLabel");
        progressFill = healthBar?.Q(className: "unity-progress-bar__progress");
        mainCam = Camera.main;

        UpdateUI(force:true);
    }

    private void LateUpdate()
    {
        if (npcTarget == null || uiDoc == null)
            return;

        if (mainCam == null)
            mainCam = Camera.main;

        if (mainCam != null)
        {
            transform.position = npcTarget.transform.position + offset;
            transform.rotation = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0);
        }

        bool changed = npcTarget.Health != lastHealth ||
                       npcTarget.MaxHealth != lastMaxHealth ||
                       npcTarget.Name != lastName;

        if (changed)
            UpdateUI(force: false);
    }

    private void UpdateUI(bool force)
    {
        if (healthBar == null || nameLabel == null || npcTarget == null)
            return;

        float maxHealth = Mathf.Max(1, npcTarget.MaxHealth);
        float currentHealth = Mathf.Clamp(npcTarget.Health, 0, maxHealth);
        float ratio = currentHealth / maxHealth;

        nameLabel.text = npcTarget.Name;
        healthBar.highValue = maxHealth;
        healthBar.value = currentHealth;

        if (progressFill != null)
        {
            Color barColor = Color.Lerp(Color.red, Color.green, ratio);
            progressFill.style.backgroundColor = new StyleColor(barColor);
        }

        lastHealth = currentHealth;
        lastMaxHealth = maxHealth;
        lastName = npcTarget.Name;

        if (!Application.isPlaying)
        {
            EditorApplication.QueuePlayerLoopUpdate();
            SceneView.RepaintAll();
        }
        
    }

    public void RefreshFromEditor()
    {
        UpdateUI(force: true);
    }
}