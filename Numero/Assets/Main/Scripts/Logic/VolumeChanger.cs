using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume(float volume)
    {
        DIContainer.Resolve<GameBootstrapper>().ChangeVolume(volume);
       
    }
}
