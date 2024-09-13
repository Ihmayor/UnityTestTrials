using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VoiceInput : MonoBehaviour
{
    int sampleWindow = 64;
    AudioClip microphoneClip;
    public Image progressBar;

    float gainedAmount;

    float slope;

    bool isLit;


    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClip();   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float amountFromLoudness = GetLoudnessFromMicrophone() * 40;
        GetComponent<TextMeshPro>().text = "Warmth: " + amountFromLoudness;

        if (amountFromLoudness < 4.4f && amountFromLoudness > 0.0005f)
        {
            gainedAmount += amountFromLoudness ;
            progressBar.fillAmount = gainedAmount / 100;
        }
        gainedAmount -= 0.3f;

       
    }

    public void MicrophoneToAudioClip()
    {
        //Get the first microphone
        string microphoneName = Microphone.devices[1];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[1]), microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for(int i = 0; i< sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }
}
