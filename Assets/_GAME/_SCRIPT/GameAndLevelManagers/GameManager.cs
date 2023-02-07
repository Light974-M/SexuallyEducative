using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

/// <summary>
/// singleton manager of all projects global properties
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Singleton Initialize

    /// <summary>
    /// unique instance of GameManager class
    /// </summary>
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    #endregion


    [SerializeField, Tooltip("tell if the game is in pause or not")]
    private bool _isPaused = false;

    [SerializeField, Tooltip("tell if the game is in pause or not")]
    private bool _isGameOver = false;

    #region Public API

    public bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

    public bool IsGameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }

    #endregion


    #region Public API



    #endregion

    /// <summary>
    /// awake is called when script instance is being loaded
    /// </summary>
    private void Awake()
    {
        Initialize();

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// OnDrawGizmos is called each time scene refresh
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            Initialize();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Initialize()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            if (Application.isPlaying)
                Destroy(this);
            else
                DestroyImmediate(this);
        }
    }
}
