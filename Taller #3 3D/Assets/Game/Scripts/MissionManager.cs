using UnityEngine;
using UnityEngine.Events;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    [Header("Configuración")]
    [SerializeField] private int minimumToAdvance = 6;
    [SerializeField] private int totalArtifacts   = 10;

    // Eventos que otros scripts escuchan
    public UnityEvent<int, int> onArtifactCollected; // (recogidos, total)
    public UnityEvent           onMinimumReached;    // cuando llega a 6

    private int collectedCount = 0;

    // -------------------------------------------------------
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // -------------------------------------------------------
    /// <summary>
    /// Llamado por ArtifactPickup cada vez que se recoge un artefacto.
    /// </summary>
    public void RegisterArtifact()
    {
        collectedCount++;

        // Avisar al GameDataManager que guarde en el JSON
        GameDataManager.Instance.AddArtifactCollected();

        // Avisar a la UI (pasa cuántos van y el total)
        onArtifactCollected?.Invoke(collectedCount, totalArtifacts);

        Debug.Log($"[MissionManager] Artefactos: {collectedCount}/{totalArtifacts}");

        // ¿Se alcanzó el mínimo para avanzar?
        if (collectedCount == minimumToAdvance)
        {
            onMinimumReached?.Invoke();
            Debug.Log("[MissionManager] ¡Mínimo alcanzado! Portal desbloqueado.");
        }
    }

    public int GetCollected() => collectedCount;
    public int GetTotal()     => totalArtifacts;
    public bool CanAdvance()  => collectedCount >= minimumToAdvance;
}
