using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        Debug.Log("Début préparation : " + Time.realtimeSinceStartup);

        videoPlayer.prepareCompleted += OnPrepared;
        videoPlayer.Prepare();
    }
    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            Debug.Log("Playing frame : " + videoPlayer.frame);
        }
    }

    void OnPrepared(VideoPlayer vp)
    {
        Debug.Log("Préparée après : " + Time.realtimeSinceStartup + " secondes");
        vp.Pause();
        vp.frame = 1;
        vp.Play();
    }
    
    
}
