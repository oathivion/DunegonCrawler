using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats
{
    public string playerName; // Player Name
    public int maxHealth; // Maximum Health
    public int experience; // Current Experience
    public int level; // Current level
    public int strength; // Current Strength Stat
    public int dexterity; // Current Dexterity Stat (Mainly Used in Speed Calculations)
    public int constitution; // Current Constitution Stat
    public string weaponOne; // Reserved For melee Weapon
    public string weaponTwo; // Reserved For Ranged Weapon
    public string armor; // Current Armor Equipped
    public string statItemOne; // First Item that can modify stats
    public string statItemTwo;
}

public class StatsSaveSystem : MonoBehaviour
{
    private string activePlayer = "dev";

    private static readonly string encryptionKey = "WkmOknkz0BLkyC7p82NsivQ8NvwVor17"; //Encyrption Key. Please Don't Change. It will break my save data.
    private PlayerStats loadedStats; 

    void Awake ()
    {
        LoadActivePlayer();
    }

    public void SetActivePlayer(string newActivePlayer) {
        activePlayer = newActivePlayer;
    }

    public object GetStat(string whatStatToGet)
    {
        if (loadedStats == null)
        {
            Debug.LogError("Player stats have not been loaded. Ensure the stats are loaded before accessing them.");
            return null; // Return early to prevent further errors
        }

        var fieldInfo = typeof(PlayerStats).GetField(whatStatToGet);
        if (fieldInfo == null)
        {
            Debug.LogError($"Field {whatStatToGet} does not exist in PlayerStats.");
            return null;
        }

        return fieldInfo.GetValue(loadedStats);
    }
        

    public void ModifyStat(string whatToModify, int byHowMuch) { //This Function Modifies an IntergerStat by adding it to a number. To Subtract use a negative number as input
        var fieldInfo = typeof(PlayerStats).GetField(whatToModify);
        if (fieldInfo != null) {
            // Check if the field type is correct
            if (fieldInfo.FieldType == typeof(int)) {
                // Get the current value of the field
                int currentValue = (int)fieldInfo.GetValue(loadedStats);
                
                // Add the value to the current field value
                int newValue = currentValue + byHowMuch;

                // Set the new value to the field
                fieldInfo.SetValue(loadedStats, newValue);
                
                Debug.Log($"{whatToModify} updated: {currentValue} -> {newValue}");
            }
            else {
                Debug.LogError($"{whatToModify} is not an integer and cannot be updated with a numeric value.");
            }
        }
        else {
            Debug.LogError($"Field {whatToModify} does not exist in PlayerStats.");
        }

    }

    public void PermanitlyModifyStat (string whatToModify, int byHowMuch) { //Same Thing at Modify Stat, But saves it when it is done.
        ModifyStat(whatToModify, byHowMuch);
        SavePlayerStats(activePlayer, loadedStats);
    }

    public void EquipItem (string whatToReplace, string whatIsReplacingIt) {
        var fieldInfo = typeof(PlayerStats).GetField(whatToReplace);
        if(fieldInfo !=null) {
            //Check If The field is the correct type
            if(fieldInfo.FieldType == typeof(string)) {
                fieldInfo.SetValue(loadedStats, whatIsReplacingIt);
                Debug.Log($"{whatIsReplacingIt} was sucsesfully equiped in {whatToReplace} slot.");
                SavePlayerStats(activePlayer, loadedStats);
            }
            else {
                Debug.LogError($"{whatToReplace} is not a string and cannot be updated with a numeric value.");
            }
        }
        else {
            Debug.LogError($"Field {whatToReplace} does not exist in PlayerStats.");
        }
    }

    private void LoadActivePlayer()
        {
            if (DoesSaveFileExist(activePlayer))
            {
                loadedStats = LoadPlayerStats(activePlayer);
                if (loadedStats != null)
                {
                    Debug.Log("Player stats loaded successfully.");
                }
                else
                {
                    Debug.LogWarning("Failed to load player stats.");
                }
            }
            else
            {
                Debug.LogWarning("Save file does not exist.");
                Debug.LogWarning("Either Make A New Player Or Create a New Player");
                CreatePlayer(); //Temp Function for Testing
            }
        }

    private string GetSaveFilePath(string playerName)
    {
        return Path.Combine(Application.persistentDataPath, $"{playerName}_playerdata.dat");
    }

    public bool DoesSaveFileExist(string playerName)
    {
        return File.Exists(GetSaveFilePath(playerName));
    }

    //Method to save player stats
    public void SavePlayerStats(string playerName, PlayerStats stats)
    {
        //Convert stats to JSON
        string json = JsonUtility.ToJson(stats);

        //Encrypt the JSON string
        string encryptedData = Encrypt(json, encryptionKey);

        //Write the encrypted data to a file
        try
        {
            using (StreamWriter writer = new StreamWriter(GetSaveFilePath(playerName), false))
            {
                writer.Write(encryptedData);
            }
            Debug.Log($"Player stats for {playerName} saved and encrypted.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save player stats for {playerName}: {e.Message}");
        }
    }

    //Method to load player stats
    public PlayerStats LoadPlayerStats(string playerName)
    {
        string filePath = GetSaveFilePath(playerName);
        if (File.Exists(filePath))
        {
            try
            {
                // Read the encrypted data from the file
                string encryptedData;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    encryptedData = reader.ReadToEnd();
                }

                // Decrypt the data
                string decryptedJson = Decrypt(encryptedData, encryptionKey);

                // Convert JSON back to PlayerStats object
                PlayerStats stats = JsonUtility.FromJson<PlayerStats>(decryptedJson);
                Debug.Log($"Player stats for {playerName} loaded and decrypted.");
                return stats;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load player stats for {playerName}: {e.Message}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"Save file for {playerName} not found.");
            return null;
        }
    }

    //AES encryption method
    private static string Encrypt(string plainText, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = new byte[16]; // AES block size is 16 bytes (128 bits)
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cs.Close();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    // AES decryption method
    private static string Decrypt(string encryptedText, string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = new byte[16];
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    cs.Close();
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }

    //Get a list of available save files
    public string[] GetAvailableSaveFiles()
    {
        return Directory.GetFiles(Application.persistentDataPath, "*_playerdata.dat");
    }

    //Method to delete a save file
    public void DeleteSaveFile(string playerName)
    {
        string filePath = GetSaveFilePath(playerName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Deleted save file for {playerName}.");
        }
        else
        {
            Debug.LogWarning($"Save file for {playerName} not found.");
        }
    }

    private void CreatePlayer() { //This is a temporary script for testing purposes. Player Creation will be moved to a different script in the future.
            PlayerStats playerStats = new PlayerStats {
                playerName = "dev",
                maxHealth = 20,
                experience = 0,
                level = 1,
                strength = 10,
                dexterity = 10,
                constitution = 10,
                weaponOne = "devSword",
                weaponTwo = "devBow",
                armor = "devArmor",
                statItemOne = "speedRing",
                statItemTwo = "devAmulet",
            };
    
            SavePlayerStats(activePlayer, playerStats);
        }
}
