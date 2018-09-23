using UnityEngine;
using System.Collections;

public class DestroyAfterAudio : MonoBehaviour
{
    new AudioSource audio;

	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
        Invoke("Suicide", audio.clip.length);
	
	}
	
    void Suicide()
    {
        audio.Stop();
        Destroy(this.gameObject);
    }
}
