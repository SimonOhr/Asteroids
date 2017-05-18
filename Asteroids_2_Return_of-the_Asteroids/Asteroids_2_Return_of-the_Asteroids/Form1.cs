using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids_2_Return_of_the_Asteroids
{
    public partial class Form1 : Form
    {
        Game1 game;
        public bool formDone;
        public string PlayerName { get; private set; }
        public Form1(Game1 game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlayerName = textBox1.Text;
            Console.Write("Name Saved");
            formDone = true;
        }
    }
}
