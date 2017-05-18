using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    class HighScoreItem
    {
        string name;
        int score;
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
