using UnityEngine;

namespace NorskaLib.Utilities
{
    [CreateAssetMenu(fileName = "ReadMe", menuName = "Editor/ReadMe", order = 1)]
    public sealed class ReadMeAsset : ScriptableObject
    {
        [TextArea(4, 32)]
        [SerializeField] string text;
    }
}
