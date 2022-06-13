namespace structs
{
    [System.Serializable]

    public struct GameData
    {
        public bool musicmute;
        public bool sfxmute;
    }

    public struct BundleData
    {
        public string path;
        public uint version;
    }
}
