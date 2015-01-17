namespace PilesOfTiles.Core
{
    public interface IController
    {
        //int LoadOrder { get; set; }
        //bool Enabled { get; set; }
        void Load();
        void Unload();
    }
}