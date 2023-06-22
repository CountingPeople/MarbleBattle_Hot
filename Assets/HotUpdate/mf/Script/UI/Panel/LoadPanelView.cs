using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.UI;


internal partial class LoadPanelView
{
    private ILoadEvent _loadEvent;

    IEnumerator UpDateProgress()
    {
        for (int i = 0; i <=50; i++)
        {
            int idx = i;
            idx = idx * 2;
            while (!_loadEvent.IsCanContinue())
            {
                yield return null;
            }
            Sli_slider.value = idx * 0.01f;
            txt_progress.text = string.Format("{0}%", idx);

            _loadEvent.OnProgress(idx);
            //yield return new WaitForSeconds(_loadEvent.WaitTime());
            yield return null;
        }
        
        _loadEvent.OnComplete();
    }

    public void SetEvent(ILoadEvent loadEvent)
    {
        _loadEvent = loadEvent;
        _loadEvent.Init();
        GameApp.Instance.StartCoroutine(UpDateProgress());
    }

    protected override void OnCreate()
    {
      
    }

    protected override void OnDestroy()
    {
 
    }

}
