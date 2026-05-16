using System;
using System.IO;
using UnityEngine;

// -------------------------------------------------------
// Clases serializables que mapean el JSON
// -------------------------------------------------------

[Serializable]
public class Scene1Data
{
    public int artifactsCollected;
    public int totalArtifacts = 10;
    public int livesRemaining = 3;
    public int livesLost;
    public int deathCount;
    public bool completed;
    public float completionTime;
}

[Serializable]
public class Scene2Data
{
    public int objectsPlacedCorrectly;
    public int totalObjectsToPlace = 5;
    public int failedAttempts;
    public string[] eventsTriggered = new string[0];
    public bool portalUnlocked;
    public bool completed;
    public float completionTime;
}

[Serializable]
public class SessionStats
{
    public float totalPlayTime;
    public int totalAttempts;
    public int gamesCompleted;
}

[Serializable]
public class GameData
{
    public string playerName = "Arqueólogo";
    public string lastScenePlayed = "";
    public string timestamp = "";
    public Scene1Data scene1 = new Scene1Data();
    public Scene2Data scene2 = new Scene2Data();
    public SessionStats sessionStats = new SessionStats();
}

// -------------------------------------------------------
// GameDataManager
// -------------------------------------------------------

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public GameData Data { get; private set; }

    // Application.streamingAssetsPath apunta a Assets/StreamingAssets/
    private string FilePath => Path.Combine(Application.streamingAssetsPath, "GameData.json");

    // -------------------------------------------------------
    private void Awake()
    {
        // Singleton que persiste entre escenas
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    // -------------------------------------------------------
    // Carga el JSON desde StreamingAssets.
    // Si no existe, crea uno con valores por defecto.
    // -------------------------------------------------------
    public void LoadData()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            Data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("[GameDataManager] Datos cargados desde StreamingAssets.");
        }
        else
        {
            Data = new GameData();
            SaveData(); // crea el archivo por primera vez
            Debug.Log("[GameDataManager] GameData.json creado en StreamingAssets.");
        }
    }

    // -------------------------------------------------------
    // Escribe el estado actual en StreamingAssets/GameData.json
    // -------------------------------------------------------
    public void SaveData()
    {
        Data.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        string json = JsonUtility.ToJson(Data, prettyPrint: true);
        File.WriteAllText(FilePath, json);
        Debug.Log("[GameDataManager] Datos guardados en StreamingAssets.");
    }

    // -------------------------------------------------------
    // Métodos de Escena 1
    // -------------------------------------------------------

    /// <summary>Llama esto cada vez que el jugador recoge un artefacto.</summary>
    public void AddArtifactCollected()
    {
        Data.scene1.artifactsCollected++;
        Data.lastScenePlayed = "Cámara de Artefactos";

        if (Data.scene1.artifactsCollected >= 6)
            Data.scene1.completed = true;

        SaveData();
    }

    public int  GetArtifactsCollected() => Data.scene1.artifactsCollected;
    public int  GetTotalArtifacts()     => Data.scene1.totalArtifacts;
    public int  GetLivesRemaining()     => Data.scene1.livesRemaining;
    public bool IsScene1Complete()      => Data.scene1.completed;
}
