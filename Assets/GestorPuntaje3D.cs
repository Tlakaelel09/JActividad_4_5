using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestorPuntaje3D : MonoBehaviour
{
    public static GestorPuntaje3D instancia;

    [Header("Referencias")]
    public TMP_Text textoScore;
    public Movimiento3D jugador;

    [Header("Progreso y Dificultad")]
    public int score = 0;
    public int highScore = 0;
    public float incrementoVelocidad = 0.5f;

    [Header("Transición de Niveles (4.5)")]
    public string nombreSiguienteNivel = "Nivel2";
    public int puntajeParaSiguienteNivel = 50;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        ActualizarUI();
    }

    public void SumarPuntos(int cantidad)
    {
        score += cantidad;

        if (jugador != null)
        {
            jugador.velocidad += incrementoVelocidad;
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (score % 20 == 0 && GestorVisual3D.instancia != null)
        {
            GestorVisual3D.instancia.CambiarFondo();
        }

        ActualizarUI();

        if (!string.IsNullOrEmpty(nombreSiguienteNivel) && score >= puntajeParaSiguienteNivel)
        {
            SceneManager.LoadScene(nombreSiguienteNivel);
        }
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void ActualizarUI()
    {
        if (textoScore != null)
        {
            float vel = jugador != null ? jugador.velocidad : 0f;
            string nivel = SceneManager.GetActiveScene().name;
            textoScore.text = $"{nivel} | Score: {score} | HighScore: {highScore}\nVelocidad: {vel:F1}";
        }
    }
}