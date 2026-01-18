using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu")]
    public GameObject pauseMenuPanel;
    public GameObject resumeButton; // First button to select
    
    [Header("Settings Panel")]
    public GameObject settingsPanel;
    public GameObject settingsButton; // Button to open settings
    public GameObject settingsBackButton;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    private bool isPaused = false;
    private bool settingsOpen = false;
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = EventSystem.current;
        
        // Make sure pause menu is hidden on start
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        
        // Load saved volume settings
        LoadVolumeSettings();
    }

    void Update()
    {


        // Check for ESC key (keyboard), Start button (Xbox - button 7), or Options button (PS5 - button 9)
        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.JoystickButton7) || // xbox start / ps options (other drivers)
            Input.GetKeyDown(KeyCode.JoystickButton9))   // ps options (alternate)
        {
            if (settingsOpen)
            {
                // If settings is open, close settings instead of unpausing
                CloseSettings();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
        // Make sure a button is selected when paused (for controller navigation)
        if (isPaused && eventSystem.currentSelectedGameObject == null)
        {
            if (settingsOpen && settingsBackButton != null)
            {
                eventSystem.SetSelectedGameObject(settingsBackButton);
            }
            else if (resumeButton != null)
            {
                eventSystem.SetSelectedGameObject(resumeButton);
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Freezes the game
        isPaused = true;

        // Position the canvas in front of the player camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null && pauseMenuPanel != null)
        {
            Canvas canvas = pauseMenuPanel.GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                // Position 3 units in front of camera
                canvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 3f;
                
                // Face the camera
                canvas.transform.LookAt(mainCamera.transform);
                canvas.transform.Rotate(0, 180, 0); // Flip it around so text faces player
            }
        }

        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Select resume button
        if (resumeButton != null)
        {
            eventSystem.SetSelectedGameObject(resumeButton);
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Unfreezes the game
        isPaused = false;
        
        // Close settings if it was open
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            settingsOpen = false;
        }

        // Lock and hide cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            settingsOpen = true;
            
            // Disable pause menu buttons while settings is open
            DisablePauseMenuButtons(true);
            
            // Select settings back button
            if (settingsBackButton != null)
            {
                eventSystem.SetSelectedGameObject(settingsBackButton);
            }
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            settingsOpen = false;
            
            // Re-enable pause menu buttons
            DisablePauseMenuButtons(false);
        }
        
        // Return to resume button
        if (resumeButton != null)
        {
            eventSystem.SetSelectedGameObject(resumeButton);
        }
    }

    void DisablePauseMenuButtons(bool disable)
    {
        // Disable/enable the pause menu buttons when settings is open
        if (resumeButton != null)
        {
            Button btn = resumeButton.GetComponent<Button>();
            if (btn != null) btn.interactable = !disable;
        }
        
        if (settingsButton != null)
        {
            Button btn = settingsButton.GetComponent<Button>();
            if (btn != null) btn.interactable = !disable;
        }
        
        // Find Quit button by name (adjust if your button is named differently)
        GameObject quitButton = GameObject.Find("QuitButton");
        if (quitButton != null)
        {
            Button btn = quitButton.GetComponent<Button>();
            if (btn != null) btn.interactable = !disable;
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Unfreeze before quitting
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        
        Debug.Log("Quitting game...");
    }
    
    // ===== VOLUME CONTROL =====
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }

    void LoadVolumeSettings()
    {
        // Load saved volumes or default to 0.7
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.7f);

        // Apply to audio sources
        if (musicSource != null)
        {
            musicSource.volume = musicVol;
        }
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVol;
        }
        
        // Set sliders to match saved/default values
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = musicVol;
        }
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = sfxVol;
        }
    }
}