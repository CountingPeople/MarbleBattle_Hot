using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioSourceDto
{
    public int cfgId;
    public string name;
    public string clipPath;
    public bool isPlay;
    public AudioEnum audioEnum;
    public AudioClip audioClip;

    public bool isBg;
    public AudioSource audioSource;
}