using UnityEngine;

namespace WorkTools
{
    [SerializeField]
    public class ResData : ScriptableObject
    {
        [SerializeField] public GameObject obj;
        [SerializeField] public string path;
        [SerializeField] public string tips = "remarks";
    }
}