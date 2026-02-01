using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Input")]
    [SerializeField] private InputActionReference submit;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogIsPlaying { get; private set; }

    private bool blockSubmitThisFrame;

    
    [Header("Re-enter Block")]
    [SerializeField] private float reenterBlockSeconds = 0.2f;
    private float blockEnterUntilTime;

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

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();

            
            var button = choice.GetComponent<Button>();
            if (button != null)
            {
                var nav = button.navigation;
                nav.mode = Navigation.Mode.None;
                button.navigation = nav;
            }

            choice.SetActive(false);
            index++;
        }

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
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

        if (blockSubmitThisFrame)
        {
            blockSubmitThisFrame = false;
            return;
        }

       
        if (currentStory != null && currentStory.currentChoices.Count > 0)
            return;

        if (submit.action.WasPressedThisFrame())
        {
            ContinueStory();
        }
    }

    public void EnterDialogMode(TextAsset inkJSON)
    {
       
        if (Time.unscaledTime < blockEnterUntilTime) return;

        if (dialogIsPlaying) return;

        if (inkJSON == null)
        {
            Debug.LogWarning("EnterDialogMode called with null inkJSON");
            return;
        }

        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;

        if (dialogPanel != null) dialogPanel.SetActive(true);

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

        blockSubmitThisFrame = true;

        ContinueStory();
    }

    private void ExitDialogMode()
    {
        dialogIsPlaying = false;

        
        blockEnterUntilTime = Time.unscaledTime + reenterBlockSeconds;

        if (dialogPanel != null) dialogPanel.SetActive(false);
        if (dialogText != null) dialogText.text = "";

        for (int i = 0; i < choices.Length; i++)
            choices[i].SetActive(false);

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }

    private void ContinueStory()
    {
        if (currentStory != null && currentStory.canContinue)
        {
            dialogText.text = currentStory.Continue().Trim();
            DisplayChoices();
        }
        else
        {
            ExitDialogMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].SetActive(true);
            choicesText[index].text = choice.text.Trim();
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (currentStory == null || !dialogIsPlaying) return;

        currentStory.ChooseChoiceIndex(choiceIndex);

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

        ContinueStory();
    }

}
