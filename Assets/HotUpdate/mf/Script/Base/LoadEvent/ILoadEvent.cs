using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoadEvent
{
     void Init();

     bool IsCanContinue();

     void OnComplete();

     void OnProgress(float value);

     float WaitTime();
}
