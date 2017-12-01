using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class HighScoreItem
    {
        public string name { get; private set; }
        public int score { get; private set; }
        public HighScoreItem(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
        public override string ToString()
        {
            return name + "," + score;
        }
    }

}
