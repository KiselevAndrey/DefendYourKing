namespace KAP.Music
{
    public enum MusicType { Master, Music, Effects }

    [UnityEngine.CreateAssetMenu(fileName = "MusicStats")]
    public class MusicStatSO : UnityEngine.ScriptableObject
    {
        public MusicOptionSO master;
        public MusicOptionSO music;
        public MusicOptionSO effects;
    }
}