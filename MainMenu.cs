using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("First Selected Button")]
    public GameObject firstButton; // Drag your Play button here in Inspector

    [Header("Settings Panel")]
    public GameObject settingsPanel; // Drag your Settings panel here
    public GameObject backButton; // Drag your Back button here
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Audio Sources")]
    public AudioSource musicSource; // Your background music
    public AudioSource sfxSource; // Your nature sounds/SFX

    private EventSystem eventSystem;
    private bool settingsOpen = false;

    void Start()
    {
        // Get EventSystem
        eventSystem = EventSystem.current;

        // Make sure first button is selected for controller
        if (firstButton != null)
        {
            eventSystem.SetSelectedGameObject(firstButton);
        }

        // Load saved volume settings
        LoadVolumeSettings();

        // Hide settings panel on start
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            settingsOpen = false;
        }
    }

    void Update()
    {
        // FIX #2: Prevent selecting main menu buttons when settings is open
        if (settingsOpen)
        {
            // If settings is open and player tries to select a non-settings button, block it
            GameObject selected = eventSystem.currentSelectedGameObject;
            
            // If nothing is selected or they're selecting something outside settings panel
            if (selected == null || !selected.transform.IsChildOf(settingsPanel.transform))
            {
                // Force selection back to the back button
                if (backButton != null)
                {
                    eventSystem.SetSelectedGameObject(backButton);
                }
            }
        }
        else
        {
            // Ensure a button is always selected for controller navigation when NOT in settings
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(firstButton);
            }
        }
    }

    // ===== SCENE LOADING =====
    public void PlayGame()
    {
        // Don't allow playing if settings is open
        if (settingsOpen) return;
        
        SceneManager.LoadScene("1_island_scene"); // Change to your exact scene name
    }

    public void QuitGame()
    {
        // FIX #1: Improved quit functionality
        #if UNITY_EDITOR
            // If running in Unity Editor, stop play mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If running as a build, quit the application
            Application.Quit();
        #endif
        
        Debug.Log("Quitting game...");
    }

    // ===== SETTINGS =====
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            settingsOpen = true;
        
            // Disable main menu buttons so they can't be clicked
            DisableMainMenuButtons(true);
        
            // Select the back button when opening settings
            if (backButton != null)
            {
                eventSystem.SetSelectedGameObject(backButton);
            }
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            settingsOpen = false;
        
            // Re-enable main menu buttons
            DisableMainMenuButtons(false);
        }

        // Reselect first button when closing settings
        eventSystem.SetSelectedGameObject(firstButton);
    }

    // NEW FUNCTION - Add this anywhere in the script (before the last closing brace)
    void DisableMainMenuButtons(bool disable)
    {
        // Find Play, Quit, and Settings buttons specifically
        GameObject playButton = firstButton; // This is your Play button
        GameObject quitButton = GameObject.Find("Quit"); // Adjust name if different
        GameObject settingsButton = GameObject.Find("Settings"); // Adjust name if different
    
        // Disable/enable their Button components
        if (playButton != null)
        {
            Button btn = playButton.GetComponent<Button>();
            if (btn != null) btn.interactable = !disable;
        }
    
        if (quitButton != null)
        {
            Button btn = quitButton.GetComponent<Button>();
            if (btn != null) btn.interactable = !disable;
        }
    
        if (settingsButton != null)
        {
            Button btn = settingsButton.GetComponent<Button>();
            if (btn != null) btn.interactable = !disable;
        }
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
        // FIX #3: Load saved volumes or default to 0.7 (which is about 70% or "7" on a 0-10 scale)
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