using System.Windows.Forms;
using BlindMan.Domain;
using BlindMan.View;

namespace BlindMan
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Application.Run(new GameForm(new GameModel()));
        }
    }
}