using UnityEngine;
using UnityEngine.InputSystem;

public class DialogTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Input")]
    public InputActionReference interact;   // im Inspector die Interact-Action zuweisen

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        if (visualCue != null) visualCue.SetActive(false);
    }

    private void OnEnable()
    {
        if (interact != null) interact.action.Enable();
    }

    private void OnDisable()
    {
        if (interact != null) interact.action.Disable();
    }

    private void Update()
    {
        if (visualCue != null)
            visualCue.SetActive(playerInRange);

        if (!playerInRange) return;
        if (interact == null) return;

        if (interact.action.WasPressedThisFrame())
        {
            DialogManager.GetInstance().EnterDialogMode(inkJSON);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            playerInRange = false;
    }
}
