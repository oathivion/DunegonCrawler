using UnityEngine;
using UnityEngine.UI;  // Required for Button
using TMPro;           // Required for TextMeshProUGUI
using System.IO;

public class SaveSlotMenu : MonoBehaviour
{
    // References to the UI elements that will display the save file data
    public TextMeshProUGUI saveSlot1Text;
    public TextMeshProUGUI saveSlot2Text;
    public TextMeshProUGUI saveSlot3Text;

    public StatsSaveSystem statsSaveSystem;  // Reference to the StatsSaveSystem (drag this into the inspector)

    private string[] saveFiles;

    void Start()
    {
        LoadSaveSlots();
    }

    // This function loads the save files and displays the information in the UI
    public void LoadSaveSlots()
    {
        saveFiles = statsSaveSystem.GetAvailableSaveFiles();
        
        // Display data for the first 3 save slots (or fewer if there are less than 3 save files)
        DisplaySaveSlotInfo(saveSlot1Text, 0);
        DisplaySaveSlotInfo(saveSlot2Text, 1);
        DisplaySaveSlotInfo(saveSlot3Text, 2);
    }

    // This function updates the text of a save slot with the player's data
    private void DisplaySaveSlotInfo(TextMeshProUGUI saveSlotText, int saveSlotIndex)
{
    if (saveFiles.Length > saveSlotIndex)
    {
        string playerName = Path.GetFileNameWithoutExtension(saveFiles[saveSlotIndex]).Replace("_playerdata", "");
        PlayerStats playerStats = statsSaveSystem.LoadPlayerStats(playerName);

        if (playerStats != null)
        {
            // Concatenate all player info in a single line
            saveSlotText.text = $"{playerStats.playerName} | Level: {playerStats.level} | Exp: {playerStats.experience}";
            saveSlotText.gameObject.SetActive(true); // Ensure it's visible
            
            // Optionally, attach a button click event to load the save file when clicked
            Button button = saveSlotText.GetComponentInParent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => LoadPlayerSave(playerName));
            }
        }
        else
        {
            saveSlotText.text = "Empty Slot";
            saveSlotText.gameObject.SetActive(true);
        }
    }
    else
    {
        saveSlotText.text = "Empty Slot";
        saveSlotText.gameObject.SetActive(true); // Ensure it's visible
    }
}


    // Load the selected player's save data
    private void LoadPlayerSave(string playerName)
    {
        PlayerStats playerStats = statsSaveSystem.LoadPlayerStats(playerName);

        if (playerStats != null)
        {
            statsSaveSystem.SetActivePlayer(playerName);
            PlayerPrefs.SetString("ActivePlayer", playerName);
        }
        else
        {
            Debug.LogWarning("Failed to load player save.");
        }
    }
}
