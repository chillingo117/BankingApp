using Source.Resources;
using UnityEngine;

namespace Source.Unity
{
    public class Startup : MonoBehaviour
    {
        void Awake()
        {
            Data.Init();
        }
    }
}