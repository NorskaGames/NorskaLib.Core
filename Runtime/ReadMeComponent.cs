using UnityEngine;

namespace NorskaLib.Utilities
{
    public sealed class ReadMeComponent : MonoBehaviour
    {
        [TextArea(4, 32)]
        [SerializeField] string text;
    }
}
