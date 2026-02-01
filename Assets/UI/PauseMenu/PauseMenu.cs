using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    [Header("Input System")]
    [SerializeField] private InputActionReference pauseAction; // bind to Escape / Start

    [Header("Scenes")]
    [SerializeField] private string titleSceneName = "Title"; // must match your scene name

    private bool isPaused;

    private void OnEnable()
    {
        if (pauseAction != null) pauseAction.action.Enable();
        if (pauseAction != null) pauseAction.action.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        if (pauseAction != null) pauseAction.action.performed -= OnPausePerformed;
        if (pauseAction != null) pauseAction.action.Disable();
    }

    private void Start()
    {
        SetPaused(false);
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx)
    {
        // Optional: don't allow pausing during dialogue
        if (DialogManager.GetInstance() != null && DialogManager.GetInstance().dialogIsPlaying)
            return;

        TogglePause();
    }

    public void TogglePause()
    {
        SetPaused(!isPaused);
    }

    public void Resume()
    {
        SetPaused(false);
    }

    public void GoToTitle()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    private void SetPaused(bool pause)
    {
        isPaused = pause;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

       
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
