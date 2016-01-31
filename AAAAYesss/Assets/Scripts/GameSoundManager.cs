using UnityEngine;
using System.Collections;

public class GameSoundManager : MonoBehaviour {
    
    public const string TAG = "soundManager";
    
    private const float minPitch = 0.90f;
    private const float maxPitch = 1.10f;
    
    public AudioSource efxSource;
    public AudioClip[] playerAttackSoundArray;
    public AudioClip playerDead;
    
    
    public void PlayAttackSoundForPlayer(int playerId){
        PlaySingle(playerAttackSoundArray[playerId - 1]);
    }
    
    public void PlayDeathSound(){
        PlaySingle(playerDead);
    }
	
	public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;
        
        //Play the clip.
        efxSource.Play ();
    }
}
