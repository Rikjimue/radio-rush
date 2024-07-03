using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem BackRightTireSmoke;
    [SerializeField] private ParticleSystem BackLeftTireSmoke;
    
    
    public void StartDriftEffect() {
        if (!BackRightTireSmoke.isPlaying) {
            BackRightTireSmoke.Play();
        }

        if (!BackLeftTireSmoke.isPlaying) {
            BackLeftTireSmoke.Play();
        }
    }

    public void StopDriftEffect() {
        if (BackRightTireSmoke.isPlaying) {
            BackRightTireSmoke.Stop();
        }
        
        if (BackLeftTireSmoke.isPlaying) {
            BackLeftTireSmoke.Stop();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
