using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Ball : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController playerController;

    public ParticleSystem coinFallParticle;

    private AudioSource audioSource;
    public AudioClip coinAudio;
    public AudioClip liveAudio;
    public AudioClip rockAudio;

    public int groundDamage = 1;
    public int badBallDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (gameObject.CompareTag("Coin"))
            {
                Instantiate(coinFallParticle, transform.position, transform.rotation);
                playerController.playCoinFallSound();
                gameManager.ReduceLives(groundDamage);
                
            }
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Coin"))
            {
                playerController.playCoinSound();
                gameManager.AddScore();
            } else if (gameObject.CompareTag("Bad"))
            {
                playerController.playRockSound();
                gameManager.ReduceLives(badBallDamage);
            } else
            {
                playerController.playLiveSound();
                gameManager.AddLives();
            }
            Destroy(gameObject);
        }
    }
}
