using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchTrigger : MonoBehaviour
{
    bool isLit;

    public Image progressBar;
    public AudioClip clip;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        isLit = false;
    }


    public void LateUpdate()
    {
        if (progressBar != null &&
            !isLit &&
            progressBar.fillAmount >= 1)
        {
            isLit = true;
        }
        if (isLit && progressBar.fillAmount == 0)
        {
            audio.clip = clip;
            audio.loop = false;
            audio.volume = 1;
            StartCoroutine("TriggerSceneLoad");
            isLit = false;
        }
    }

    public IEnumerator TriggerSceneLoad()
    {
        audio.Play();
        yield return new WaitForSeconds(2f);
        while (audio.isPlaying)
        {
            yield return null;
        }

        SceneManager.LoadScene("Main");
    }

}
