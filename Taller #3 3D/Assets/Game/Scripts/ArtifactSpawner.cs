using UnityEngine;

public class ArtifactSpawner : MonoBehaviour
{
    [Header("Nombre del prefab (debe coincidir con prefabName en el JSON)")]
    [SerializeField] private string prefabName;

    // -------------------------------------------------------
    private void Start()
    {
        SpawnArtifact();
    }

    // -------------------------------------------------------
    private void SpawnArtifact()
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            Debug.LogWarning($"[ArtifactSpawner] {gameObject.name} no tiene prefabName asignado.");
            return;
        }

        // Carga el prefab desde Assets/Resources/Artifacts/
        GameObject prefab = Resources.Load<GameObject>($"Artifacts/{prefabName}");

        if (prefab == null)
        {
            Debug.LogError($"[ArtifactSpawner] No se encontró: Resources/Artifacts/{prefabName}");
            return;
        }

        // Instancia en la posición y rotación del Empty
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
