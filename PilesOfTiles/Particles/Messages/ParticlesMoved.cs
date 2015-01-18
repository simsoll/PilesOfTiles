using System.Collections.Generic;

namespace PilesOfTiles.Particles.Messages
{
    public class ParticlesMoved
    {
        public IEnumerable<Particle> Particles { get; set; } 
    }
}
