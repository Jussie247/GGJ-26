using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Input")]
    [SerializeField] private InputActionReference submit; // assign in Inspector (e.g., Interact)

    private Story currentStory;
    private bool dialogIsPlaying;

    // 🔹 NEW: blocks submit input for one frame
    private bool blockSubmitThisFrame;

    private static DialogManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogManager in the scene");
        }
        instance = this;
    }

    public static DialogManager GetInstance() => instance;

    private void Start()
    {
        dialogIsPlaying = false;
        if (dialogPanel != null) dialogPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (submit != null) submit.action.Enable();
    }

    private void OnDisable()
    {
        if (submit != null) submit.action.Disable();
    }

    private void Update()
    {
        if (!dialogIsPlaying) return;
        if (submit == null) return;

        // 🔹 NEW: ignore submit once after opening dialog
        if (blockSubmitThisFrame)
        {
            blockSubmitThisFrame = false;
            return;
        }

        if (submit.action.WasPressedThisFrame())
        {
            ContinueStory();
        }
    }

    public void EnterDialogMode(TextAsset inkJSON)
    {
        // 🔹 IMPORTANT: prevent restarting dialog while already talking
        if (dialogIsPlaying) return;

        if (inkJSON == null)
        {
            Debug.LogWarning("EnterDialogMode called with null inkJSON");
            return;
        }

        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;

        if (dialogPanel != null) dialogPanel.SetActive(true);

        // 🔹 NEW: block the opening Interact press
        blockSubmitThisFrame = true;

        ContinueStory(); // show first line safely
    }

    private void ExitDialogMode()
    {
        dialogIsPlaying = false;

        if (dialogPanel != null) dialogPanel.SetActive(false);
        if (dialogText != null) dialogText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory != null && currentStory.canContinue)
        {
            dialogText.text = currentStory.Continue().Trim();
        }
        else
        {
            ExitDialogMode();
        }
    }
}
