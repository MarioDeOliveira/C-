
namespace Projeto_I_OneTetris
{
    class Cordenates
    {
        private int x;
        private int y;

        public Cordenates(int xCoord, int yCoord)
        {
            x = xCoord;
            y = yCoord;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void updateCoord(int xCoord, int yCoord)
        {
            x = xCoord;
            y = yCoord;
        }
    }
}
