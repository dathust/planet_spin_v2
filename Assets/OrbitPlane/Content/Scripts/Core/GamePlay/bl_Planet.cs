using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bl_Planet : Singleton<bl_Planet>
{

    public int Lives = 1;
    [Header("References")]
    [SerializeField]private Animator PlanetAnim;  
    [SerializeField]private GameObject DestroyParticle;

    private int DefaultLives;
    private GameObject cacheExplosion;

    [SerializeField] private GameObject Planet;

        public Sprite[] spritePlayer;


    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        DefaultLives = Lives;
    }

    /// <summary>
    /// 
    /// </summary>
    public void DoDamage()
    {
        Lives--;

        PlanetAnim.Play("hit", 0, 0);
        if (Lives > 0)
        {       
        }
        else if(Lives == 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void GameOver()
    {
        cacheExplosion = Instantiate(DestroyParticle, transform.position, Quaternion.identity) as GameObject;
        bl_TimeManager.Instance.SetSlowMotion(true, 1);
        bl_ScoreManager.Instance.SaveScore();
        bl_GameManager.Instance.OnGameOver();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        Lives = DefaultLives;
        if(cacheExplosion != null)
        {
            Destroy(cacheExplosion);
            cacheExplosion = null;
        }
    }

      int i = 0;
    public void ChangePlayer()
    {
        i = Random.Range(0, 20); // 0, 1, 2, 19.
        Planet.GetComponent<Image>().sprite = spritePlayer[i];
    }

    public void UpdateChangePlayer()
    {
        if (GameConfig.LEVELUP)
        {
            if (i > 18)
            {
                if (i == (spritePlayer.Length - 1))
                {
                    i = spritePlayer.Length - 1;
                }
                else
                {
                    i++;
                }
                Planet.GetComponent<Image>().sprite = spritePlayer[i];
                GameConfig.LEVELUP = false;
            }
        }
    }

    public static bl_Planet Instance
    {
        get
        {
            return ((bl_Planet)mInstance);
        }
        set
        {
            mInstance = value;
        }
    }
}