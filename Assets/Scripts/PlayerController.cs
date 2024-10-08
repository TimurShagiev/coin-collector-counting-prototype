using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource audioSource;
    public AudioClip coinAudio;
    public AudioClip coinFallAudio;
    public AudioClip liveAudio;
    public AudioClip rockAudio;

    float horizontalPos;
    float offsetY = 135f;
    float offsetZ = 21.13814f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        BoxFollowMouse();
    }

    void BoxFollowMouse()
    {

        horizontalPos = Input.mousePosition.x;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(horizontalPos, offsetY, offsetZ));
    }

    public void playRockSound()
    {
        audioSource.PlayOneShot(rockAudio, 1f);
    }

    public void playCoinSound()
    {

        audioSource.PlayOneShot(coinAudio, 1f);
    }

    public void playCoinFallSound()
    {
        audioSource.PlayOneShot(coinFallAudio, 1f);
    }

    public void playLiveSound()
    {
        audioSource.PlayOneShot(liveAudio, 1f);
    }

    //private void OnMouseDown()
    //{
    //    if (gameManager.spawningTargets)
    //    {
    //    }
    //}
}
