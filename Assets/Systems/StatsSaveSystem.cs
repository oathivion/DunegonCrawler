using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class PlayerStats
{
    public string playerName; // Player Name
    public int maxHealth; // Maximum Health
    public int experience; // Current Experience
    public int level; // Current level
    public int strength; // Current Strength Stat
    public int dexterity; // Current Dexterity Stat
    public int constitution; // Current Constitution Stat
    public string weaponOne; // Current weapon one
    public string weaponTwo; // Current Weapon Two
    public string armor; // Current Armor Equipped
    public string statItemOne; // First Item that can modify stats
    public string statItemTwo;
    public string statItemThree;
}

public class StatsSaveSystem : MonoBehaviour
{
    private static readonly string encryptionKey = "WkmOknkz0BLkyC7p82NsivQ8NvwVor17"; 

    private string GetSaveFilePath(string playerName)
    {
        return Path.Combine(Application.persistentDataPath, $"{playerName}_playerdata.dat");
    }

    public bool DoesSaveFileExist(string playerName)
    {
        return File.Exists(GetSaveFilePath(playerName));
    }

    // Method to save player stats
    public void SavePlayerStats(string playerName, PlayerStats stats)
    {
        // Convert stats to JSON
        string json = JsonUtility.ToJson(stats);

        // Encrypt the JSON string
        string encryptedData = Encrypt(json, encryptionKey);

        // Write the encrypted data to a file
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

    // Method to load player stats
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

    // AES encryption method
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

    // Optional: Get a list of available save files
    public string[] GetAvailableSaveFiles()
    {
        return Directory.GetFiles(Application.persistentDataPath, "*_playerdata.dat");
    }

    // Optional: Method to delete a save file
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
}
