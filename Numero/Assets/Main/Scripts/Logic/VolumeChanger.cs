using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
  
    public void ChangeVolume(float volume)
    {
        DIContainer.Resolve<GameBootstrapper>().ChangeVolume(volume);
       
    }
}
