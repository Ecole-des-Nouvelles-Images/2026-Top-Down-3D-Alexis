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

    void OnPrepared(VideoPlayer vp)
    {
        vp.Pause();
        vp.frame = 1;
        vp.Play();
    }
    
    
}
