using Microsoft.Xna.Framework;

namespace PilesOfTiles.Core
{
    public interface IUpdatable
    {
        //int UpdateOrder { get; set; }
        void Update(GameTime gameTime);
    }
}