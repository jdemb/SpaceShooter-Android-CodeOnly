using UnityEngine;
using System.Collections;
using TMPro;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerexplosion;
    public int scorevalue;
    private GameController gameController;
    private PlayerController playerController;
    public int healthpoints;
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer meshRenderer2;
    public Color hitColor;
    public GameObject PointText;
    public SpawnAfterDestroy spawnAfterDestroy;
    private TextMeshPro textMeshPro;
    static readonly object locker = new object();
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController==null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        GameObject playerControllerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
        
    }

	void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "TimePack")
        {
            if (other.CompareTag("Player"))
            {
                gameController.SlowMotion();
                Destroy(gameObject);
                return;
            }
            else
            {
                return;
            }
        }
        if (gameObject.tag == "SpeedPack")
        {
            if(other.CompareTag("Player"))
            {
                playerController.IncreaseFireRate();
                Destroy(gameObject);
                return;
            }
            else
            {
                return;
            }
        }
        if(other.CompareTag("Boundary") || other.CompareTag("Enemy")|| other.CompareTag("Boss") || other.CompareTag("PowerUp") || other.CompareTag("SpeedPack") || other.CompareTag("TimePack"))
        {
            return;
        }
        if(gameObject.tag == "PowerUp" && other.CompareTag("PlayerBolt"))
        {
            return;
        }
        if (explosion != null && healthpoints <= 1)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        if (other.CompareTag ("Player"))
        {
            if (gameObject.tag == "PowerUp")
            {
                gameController.HigherHP();
                Destroy(gameObject);
                return;
            }
            else
            {
                if (gameController.hp == false)
                {
                    if (playerexplosion != null)
                    {
                        Instantiate(playerexplosion, transform.position, transform.rotation);
                    }
                    gameController.GameOver();
                    Destroy(other.gameObject);
                }
                else
                {
                    if (playerexplosion != null)
                    {
                        Instantiate(playerexplosion, transform.position, transform.rotation);
                    }
                    gameController.LowerHP();
                    if (gameObject.tag != "Boss")
                    {
                        Destroy(gameObject);
                    }
                }
            }
            
        }
        
        if(other.tag != "Player")
            Destroy(other.gameObject);

        if (healthpoints <= 1)
        {
            if(PointText != null)
            {
                textMeshPro = PointText.GetComponentInChildren<TextMeshPro>();
                textMeshPro.text = "+" + scorevalue.ToString();
                Instantiate(PointText, transform.position, PointText.transform.rotation);
            }
            if (spawnAfterDestroy != null)
                spawnAfterDestroy.destroy();
            Destroy(gameObject);
            gameController.Addscore(scorevalue);
            if(gameObject.tag == "Boss")
            {
                gameController.BossOver();
            }
        }
        else
        {
            healthpoints--;
            if (gameObject.tag == "Boss")
            {
                StartCoroutine(swapBossShaderColor());
            }
            else if(meshRenderer == null)
            {
                if(meshRenderer2 != null)
                    StartCoroutine(swapSkinnedShaderColor());
            }
            else
            {
                StartCoroutine(swapShaderColor());
            }
        }

    }

    IEnumerator swapShaderColor()
    {
        lock(locker)
        {
            meshRenderer.materials[0].SetColor("_EmissionColor", hitColor);
            yield return new WaitForSeconds(0.2f);
            meshRenderer.materials[0].SetColor("_EmissionColor", Color.black);
        }
        
    }

    IEnumerator swapSkinnedShaderColor()
    {
        lock (locker)
        {
            meshRenderer2.material.SetFloat("_Shininess", 0.03f);
            yield return new WaitForSeconds(0.2f);
            meshRenderer2.material.SetFloat("_Shininess", 1);
        }

    }

    IEnumerator swapBossShaderColor()
    {
        lock(locker)
        {
            meshRenderer.materials[0].color = hitColor;
            yield return new WaitForSeconds(0.2f);
            meshRenderer.materials[0].SetColor("_Color", Color.white);
        }      
    }

}
