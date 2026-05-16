using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    [Header("Sonido")]
    [SerializeField] private AudioClip collectSound;

    private bool isBeingCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isBeingCollected) return;
        if (!other.CompareTag("Player")) return;

        isBeingCollected = true;
        StartCoroutine(CollectSequence());
    }

    private IEnumerator CollectSequence()
    {
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        MissionManager.Instance.RegisterArtifact();

        yield return StartCoroutine(FadeOut(0.5f));

        Destroy(gameObject);
    }

    private IEnumerator FadeOut(float duration)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            foreach (Renderer rend in renderers)
            {
                foreach (Material mat in rend.materials)
                {
                    if (mat.HasProperty("_BaseColor"))
                    {
                        Color c = mat.GetColor("_BaseColor");
                        c.a = alpha;
                        mat.SetColor("_BaseColor", c);
                    }
                }
            }
            yield return null;
        }
    }
}