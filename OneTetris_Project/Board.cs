using System;
using System.Threading;

namespace Projeto_I_OneTetris
{
      class Board
    {
        private static int boardWidth;
        private static int boardHeight;
        private static string boardLimitCharacter;
        private static string boardEmptyCharacter;
        private static string boardFullCharacter;
        private static bool[,] boardContent;

        private static int lines;
        private static int score;
    
        public Board(int width, int height, string limitCharacter, string emptyCharacter, string fullCharacter)
        {
            boardWidth = width;
            boardHeight = height;
            boardLimitCharacter = limitCharacter;
            boardEmptyCharacter = emptyCharacter;
            boardFullCharacter = fullCharacter;
            lines = 0;
            score = 0;
        }

        public void setContent(bool[,] content)
        {
            boardContent = content;
        }

        public void upLines()
        {
            lines = lines + 1;         
            score = score + (lines * 100);
        }

        public void drawBoard()
        {
            Console.WriteLine("║Projeto_I_OneTetris ║");
            Console.WriteLine("══════════════════════");
            Console.WriteLine("║Score: "+score+  "  ║Lines: "+lines);
            Console.WriteLine("══════════════════════");
            Console.WriteLine(" ");

            for (int i = 0; i <= boardHeight-1; i++)
            {
                string lineToDraw = boardLimitCharacter;

          
                for (int j = 0; j <= boardWidth-1; j++)
                {
                    if (boardContent!=null && boardContent[i,j] == true)
                    {
                        lineToDraw = lineToDraw + boardFullCharacter;
                    }
                    else
                    {
                        lineToDraw = lineToDraw + boardEmptyCharacter;
                    }
                  
                }

                lineToDraw = lineToDraw + boardLimitCharacter;
                Console.WriteLine(lineToDraw);
            }

            string floorLimitLine = boardLimitCharacter;


            for (int k = 0; k <= boardWidth - 1; k++)
            {
                floorLimitLine = floorLimitLine + boardLimitCharacter;
            }

            floorLimitLine = floorLimitLine + boardLimitCharacter;


            Console.WriteLine(floorLimitLine);
        }
    }
}
