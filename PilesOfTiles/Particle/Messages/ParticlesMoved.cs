using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PilesOfTiles.Particle.Messages
{
    public class ParticlesMoved
    {
        public IEnumerable<Particle> Particles { get; set; } 
    }
}
