namespace KAP.Music
{
    [UnityEngine.CreateAssetMenu(fileName = "MusicOption")]
    public class MusicOptionSO : UnityEngine.ScriptableObject
    {
        public MusicType type;
        public bool play;
        [UnityEngine.Range(0, 1)] public float volume;
    }
}