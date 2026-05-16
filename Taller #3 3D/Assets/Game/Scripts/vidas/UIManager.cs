using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textoVidas;
    [SerializeField] private GameObject panelGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
        panelGameOver.SetActive(false);
    }

    private void Start()
    {
        ActualizarVidas(LifeManager.Instance.Vidas);
    }

    public void ActualizarVidas(int vidas)
    {
        textoVidas.text = vidas.ToString();
    }

    public void MostrarGameOver()
    {
        Debug.Log("GAME OVER");

        panelGameOver.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Reintentar()
    {
        Debug.Log("REINICIANDO ESCENA");

        Time.timeScale = 1f;

        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }
}