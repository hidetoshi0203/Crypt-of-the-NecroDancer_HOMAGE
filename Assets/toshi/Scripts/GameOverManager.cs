using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    AudioSource audioSource;
    //[SerializeField] private AudioClip GameOverBGM;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.PlayOneShot(GameOverBGM);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("testScene");
        }
    }
}
