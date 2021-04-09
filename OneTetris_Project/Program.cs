using System;

namespace Projeto_I_OneTetris
{
    class Program
    {
    
        private static int boardWidth = 20;
        private static int boardHeight = 10;

        private static string boardLimitCharacter = "║";
        private static string boardFullCharacter = "▓";
        private static string boardEmptyCharacter = " ";

        private static Board board;
        private static bool[,] boardContent;
        private static bool gameRunning;
        private static Block currentFallingBlock;
        private static bool addFallingBlock;

        private static void Main(string[] args)
        {
            board = new Board(boardWidth, boardHeight, boardLimitCharacter, boardEmptyCharacter, boardFullCharacter);
            boardContent = new bool[boardHeight, boardWidth];

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            gameRunning = true;
            
            addFallingBlock = true;

            do
            {
                if (addFallingBlock)
                {
                    currentFallingBlock = generateTetrisBlock();
                    addBlockToGame(currentFallingBlock);
                }
                Console.Clear();
                board.setContent(boardContent);
                board.drawBoard();
                
                ConsoleKeyInfo Key = Console.ReadKey();
                switch (Key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        
                        moveBlockLeft();
                        break;

                    case ConsoleKey.RightArrow:
                       
                        moveBlockRight();
                        break;

                    case ConsoleKey.DownArrow:
                      
                        moveBlockDown();
                        break;

                    case ConsoleKey.Escape:

                        gameRunning = false;
                        Console.WriteLine("GAME OVER!!!!!");
                        break;
                        
                }
            } while (gameRunning);
        }   
        private static void moveBlockDown()
        {
            if (currentFallingBlock is BlockLine)
            {
               
                BlockLine b = (BlockLine)currentFallingBlock;
                
                Cordenates[] tmpBlock = b.getBlockLine();

                
                if(!checkBotomColision(
                    new Cordenates(tmpBlock[0].getX(), tmpBlock[0].getY() + 1),
                    new Cordenates(tmpBlock[1].getX(), tmpBlock[1].getY() + 1),
                    new Cordenates(tmpBlock[2].getX(), tmpBlock[2].getY() + 1),
                    new Cordenates(tmpBlock[3].getX(), tmpBlock[3].getY() + 1)) &&
                    !checkDownMovementColision(new Cordenates(tmpBlock[0].getX(), tmpBlock[0].getY() + 1),
                    new Cordenates(tmpBlock[1].getX(), tmpBlock[1].getY() + 1),
                    new Cordenates(tmpBlock[2].getX(), tmpBlock[2].getY() + 1),
                    new Cordenates(tmpBlock[3].getX(), tmpBlock[3].getY() + 1)))
                {
                   
                    boardContent[tmpBlock[0].getY(), tmpBlock[0].getX()] = false;
                    boardContent[tmpBlock[1].getY(), tmpBlock[1].getX()] = false;
                    boardContent[tmpBlock[2].getY(), tmpBlock[2].getX()] = false;
                    boardContent[tmpBlock[3].getY(), tmpBlock[3].getX()] = false;

                  
                    boardContent[(tmpBlock[0].getY()+1), tmpBlock[0].getX()] = true;
                    boardContent[(tmpBlock[1].getY()+1), tmpBlock[1].getX()] = true;
                    boardContent[(tmpBlock[2].getY()+1), tmpBlock[2].getX()] = true;
                    boardContent[(tmpBlock[3].getY()+1), tmpBlock[3].getX()] = true;

                  
                    currentFallingBlock.setPointA(new Cordenates(tmpBlock[0].getX(), tmpBlock[0].getY() + 1));
                    currentFallingBlock.setPointB(new Cordenates(tmpBlock[1].getX(), tmpBlock[1].getY() + 1));
                    currentFallingBlock.setPointC(new Cordenates(tmpBlock[2].getX(), tmpBlock[2].getY() + 1));
                    currentFallingBlock.setPointD(new Cordenates(tmpBlock[3].getX(), tmpBlock[3].getY() + 1));
                }
                else
                {                  
                    checkFullLines();
                
                    addFallingBlock = true;

                }
            }
            else if (currentFallingBlock is BlockSquare)
            {
                
                BlockSquare b = (BlockSquare)currentFallingBlock;
                
                Cordenates[,] tmpBlock = b.getBlockSquare();
            
                if (!checkBotomColision(
                    new Cordenates(tmpBlock[0,0].getX(), tmpBlock[0,0].getY() + 1),
                    new Cordenates(tmpBlock[0,1].getX(), tmpBlock[0,1].getY() + 1),
                    new Cordenates(tmpBlock[1,0].getX(), tmpBlock[1,0].getY() + 1),
                    new Cordenates(tmpBlock[1,1].getX(), tmpBlock[1,1].getY() + 1)) &&
                    !checkDownMovementColision(new Cordenates(tmpBlock[0, 0].getX(), tmpBlock[0, 0].getY() + 1),
                    new Cordenates(tmpBlock[0, 1].getX(), tmpBlock[0, 1].getY() + 1),
                    new Cordenates(tmpBlock[1, 0].getX(), tmpBlock[1, 0].getY() + 1),
                    new Cordenates(tmpBlock[1, 1].getX(), tmpBlock[1, 1].getY() + 1)))
                {
                   
                    boardContent[tmpBlock[0, 0].getY(), tmpBlock[0, 0].getX()] = false;
                    boardContent[tmpBlock[0, 1].getY(), tmpBlock[0, 1].getX()] = false;
                    boardContent[tmpBlock[1, 0].getY(), tmpBlock[1, 0].getX()] = false;
                    boardContent[tmpBlock[1, 1].getY(), tmpBlock[1, 1].getX()] = false;

                   
                    boardContent[(tmpBlock[0, 0].getY() +1), tmpBlock[0, 0].getX()] = true;
                    boardContent[(tmpBlock[0, 1].getY() +1), tmpBlock[0, 1].getX()] = true;
                    boardContent[(tmpBlock[1, 0].getY() +1), tmpBlock[1, 0].getX()] = true;
                    boardContent[(tmpBlock[1, 1].getY() +1), tmpBlock[1, 1].getX()] = true;

                   
                    currentFallingBlock.setPointA(new Cordenates(tmpBlock[0, 0].getX(), (tmpBlock[0, 0].getY() + 1)));
                    currentFallingBlock.setPointB(new Cordenates(tmpBlock[0, 1].getX(), (tmpBlock[0, 1].getY() + 1)));
                    currentFallingBlock.setPointC(new Cordenates(tmpBlock[1, 0].getX(), (tmpBlock[1, 0].getY() + 1)));
                    currentFallingBlock.setPointD(new Cordenates(tmpBlock[1, 1].getX(), (tmpBlock[1, 1].getY() + 1)));
                }

                else
                {
                    checkFullLines();
                    
                    addFallingBlock = true;

                }
            }
        }

        private static void moveBlockRight()
        {
            if (currentFallingBlock is BlockLine)
            {
               
                BlockLine b = (BlockLine)currentFallingBlock;
      
                Cordenates[] tmpBlock = b.getBlockLine();
              
                if (!checkBotomColision(
                    new Cordenates(tmpBlock[0].getX() + 1, tmpBlock[0].getY()),
                    new Cordenates(tmpBlock[1].getX() + 1, tmpBlock[1].getY()),
                    new Cordenates(tmpBlock[2].getX() + 1, tmpBlock[2].getY()),
                    new Cordenates(tmpBlock[3].getX() + 1, tmpBlock[3].getY())))
                {
                    if(!checkSideColision(new Cordenates(tmpBlock[0].getX() + 1, tmpBlock[0].getY()),
                    new Cordenates(tmpBlock[1].getX() + 1, tmpBlock[1].getY()),
                    new Cordenates(tmpBlock[2].getX() + 1, tmpBlock[2].getY()),
                    new Cordenates(tmpBlock[3].getX() + 1, tmpBlock[3].getY())) &&
                    !checkRightMovementColision(new Cordenates(tmpBlock[0].getX() + 1, tmpBlock[0].getY()),
                    new Cordenates(tmpBlock[1].getX() + 1, tmpBlock[1].getY()),
                    new Cordenates(tmpBlock[2].getX() + 1, tmpBlock[2].getY()),
                    new Cordenates(tmpBlock[3].getX() + 1, tmpBlock[3].getY())))
                    {
                        
                        boardContent[tmpBlock[0].getY(), tmpBlock[0].getX()] = false;
                        boardContent[tmpBlock[1].getY(), tmpBlock[1].getX()] = false;
                        boardContent[tmpBlock[2].getY(), tmpBlock[2].getX()] = false;
                        boardContent[tmpBlock[3].getY(), tmpBlock[3].getX()] = false;

                       
                        boardContent[tmpBlock[0].getY(), (tmpBlock[0].getX() + 1)] = true;
                        boardContent[tmpBlock[1].getY(), (tmpBlock[1].getX() + 1)] = true;
                        boardContent[tmpBlock[2].getY(), (tmpBlock[2].getX() + 1)] = true;
                        boardContent[tmpBlock[3].getY(), (tmpBlock[3].getX() + 1)] = true;

                       
                        currentFallingBlock.setPointA(new Cordenates(tmpBlock[0].getX() + 1, tmpBlock[0].getY()));
                        currentFallingBlock.setPointB(new Cordenates(tmpBlock[1].getX() + 1, tmpBlock[1].getY()));
                        currentFallingBlock.setPointC(new Cordenates(tmpBlock[2].getX() + 1, tmpBlock[2].getY()));
                        currentFallingBlock.setPointD(new Cordenates(tmpBlock[3].getX() + 1, tmpBlock[3].getY()));
                    }
                    else
                    {
                         
                    }
                   
                }
                else
                {
                   
                    checkFullLines();
                
                    addFallingBlock = true;

                }
            }
            else if (currentFallingBlock is BlockSquare)
            {
                
                BlockSquare b = (BlockSquare)currentFallingBlock;
               
                Cordenates[,] tmpBlock = b.getBlockSquare();
             
                if (!checkBotomColision(
                    new Cordenates(tmpBlock[0, 0].getX() + 1, tmpBlock[0, 0].getY()),
                    new Cordenates(tmpBlock[0, 1].getX() + 1, tmpBlock[0, 1].getY()),
                    new Cordenates(tmpBlock[1, 0].getX() + 1, tmpBlock[1, 0].getY()),
                    new Cordenates(tmpBlock[1, 1].getX() + 1, tmpBlock[1, 1].getY())))
                {
                    if (!checkSideColision(new Cordenates(tmpBlock[0, 0].getX() + 1, tmpBlock[0, 0].getY()),
                    new Cordenates(tmpBlock[0, 1].getX() + 1, tmpBlock[0, 1].getY()),
                    new Cordenates(tmpBlock[1, 0].getX() + 1, tmpBlock[1, 0].getY()),
                    new Cordenates(tmpBlock[1, 1].getX() + 1, tmpBlock[1, 1].getY())) &&
                    !checkRightMovementColision(new Cordenates(tmpBlock[0, 0].getX() + 1, tmpBlock[0, 0].getY()),
                    new Cordenates(tmpBlock[0, 1].getX() + 1, tmpBlock[0, 1].getY()),
                    new Cordenates(tmpBlock[1, 0].getX() + 1, tmpBlock[1, 0].getY()),
                    new Cordenates(tmpBlock[1, 1].getX() + 1, tmpBlock[1, 1].getY())))
                    {                       
                        boardContent[tmpBlock[0, 0].getY(), tmpBlock[0, 0].getX()] = false;
                        boardContent[tmpBlock[0, 1].getY(), tmpBlock[0, 1].getX()] = false;
                        boardContent[tmpBlock[1, 0].getY(), tmpBlock[1, 0].getX()] = false;
                        boardContent[tmpBlock[1, 1].getY(), tmpBlock[1, 1].getX()] = false;
                       
                        boardContent[tmpBlock[0, 0].getY(), (tmpBlock[0, 0].getX() + 1)] = true;
                        boardContent[tmpBlock[0, 1].getY(), (tmpBlock[0, 1].getX() + 1)] = true;
                        boardContent[tmpBlock[1, 0].getY(), (tmpBlock[1, 0].getX() + 1)] = true;
                        boardContent[tmpBlock[1, 1].getY(), (tmpBlock[1, 1].getX() + 1)] = true;
                      
                        currentFallingBlock.setPointA(new Cordenates((tmpBlock[0, 0].getX() + 1), tmpBlock[0, 0].getY()));
                        currentFallingBlock.setPointB(new Cordenates((tmpBlock[0, 1].getX() + 1), tmpBlock[0, 1].getY()));
                        currentFallingBlock.setPointC(new Cordenates((tmpBlock[1, 0].getX() + 1), tmpBlock[1, 0].getY()));
                        currentFallingBlock.setPointD(new Cordenates((tmpBlock[1, 1].getX() + 1), tmpBlock[1, 1].getY()));
                    }
                    else
                    {
                       
                    }
                }
                else
                {                  
                    checkFullLines();

                    addFallingBlock = true;

                }
            }

        }

        private static void moveBlockLeft()
        {
            if (currentFallingBlock is BlockLine)
            {
                
                BlockLine b = (BlockLine)currentFallingBlock;
               
                Cordenates[] tmpBlock = b.getBlockLine();
             
                if (!checkBotomColision(
                    new Cordenates(tmpBlock[0].getX() - 1, tmpBlock[0].getY()),
                    new Cordenates(tmpBlock[1].getX() - 1, tmpBlock[1].getY()),
                    new Cordenates(tmpBlock[2].getX() - 1, tmpBlock[2].getY()),
                    new Cordenates(tmpBlock[3].getX() - 1, tmpBlock[3].getY())) )
                {
                    if (!checkSideColision(new Cordenates(tmpBlock[0].getX() - 1, tmpBlock[0].getY()),
                    new Cordenates(tmpBlock[1].getX() - 1, tmpBlock[1].getY()),
                    new Cordenates(tmpBlock[2].getX() - 1, tmpBlock[2].getY()),
                    new Cordenates(tmpBlock[3].getX() - 1, tmpBlock[3].getY())) &&
                    !checkLeftMovementColision(new Cordenates(tmpBlock[0].getX() - 1, tmpBlock[0].getY()),
                    new Cordenates(tmpBlock[1].getX() - 1, tmpBlock[1].getY()),
                    new Cordenates(tmpBlock[2].getX() - 1, tmpBlock[2].getY()),
                    new Cordenates(tmpBlock[3].getX() - 1, tmpBlock[3].getY())))
                    {                      
                        boardContent[tmpBlock[0].getY(), tmpBlock[0].getX()] = false;
                        boardContent[tmpBlock[1].getY(), tmpBlock[1].getX()] = false;
                        boardContent[tmpBlock[2].getY(), tmpBlock[2].getX()] = false;
                        boardContent[tmpBlock[3].getY(), tmpBlock[3].getX()] = false;
                   
                        boardContent[tmpBlock[0].getY(), (tmpBlock[0].getX() - 1)] = true;
                        boardContent[tmpBlock[1].getY(), (tmpBlock[1].getX() - 1)] = true;
                        boardContent[tmpBlock[2].getY(), (tmpBlock[2].getX() - 1)] = true;
                        boardContent[tmpBlock[3].getY(), (tmpBlock[3].getX() - 1)] = true;
                       
                        currentFallingBlock.setPointA(new Cordenates(tmpBlock[0].getX() - 1, tmpBlock[0].getY()));
                        currentFallingBlock.setPointB(new Cordenates(tmpBlock[1].getX() - 1, tmpBlock[1].getY()));
                        currentFallingBlock.setPointC(new Cordenates(tmpBlock[2].getX() - 1, tmpBlock[2].getY()));
                        currentFallingBlock.setPointD(new Cordenates(tmpBlock[3].getX() - 1, tmpBlock[3].getY()));
                    }
                    else
                    {
                         
                    }

                }
                else
                {                   
                    checkFullLines();

                    
                    addFallingBlock = true;

                }
            }
            else if (currentFallingBlock is BlockSquare)
            {
                
                BlockSquare b = (BlockSquare)currentFallingBlock;
              
                Cordenates[,] tmpBlock = b.getBlockSquare();
              
                if (!checkBotomColision(
                    new Cordenates(tmpBlock[0, 0].getX() - 1, tmpBlock[0, 0].getY()),
                    new Cordenates(tmpBlock[0, 1].getX() - 1, tmpBlock[0, 1].getY()),
                    new Cordenates(tmpBlock[1, 0].getX() - 1, tmpBlock[1, 0].getY()),
                    new Cordenates(tmpBlock[1, 1].getX() - 1, tmpBlock[1, 1].getY())))
                {
                    if (!checkSideColision(new Cordenates(tmpBlock[0, 0].getX() - 1, tmpBlock[0, 0].getY()),
                    new Cordenates(tmpBlock[0, 1].getX() - 1, tmpBlock[0, 1].getY()),
                    new Cordenates(tmpBlock[1, 0].getX() - 1, tmpBlock[1, 0].getY()),
                    new Cordenates(tmpBlock[1, 1].getX() - 1, tmpBlock[1, 1].getY())) &&
                    !checkLeftMovementColision(new Cordenates(tmpBlock[0, 0].getX() - 1, tmpBlock[0, 0].getY()),
                    new Cordenates(tmpBlock[0, 1].getX() - 1, tmpBlock[0, 1].getY()),
                    new Cordenates(tmpBlock[1, 0].getX() - 1, tmpBlock[1, 0].getY()),
                    new Cordenates(tmpBlock[1, 1].getX() - 1, tmpBlock[1, 1].getY())))
                    {                    
                        boardContent[tmpBlock[0, 0].getY(), tmpBlock[0, 0].getX()] = false;
                        boardContent[tmpBlock[0, 1].getY(), tmpBlock[0, 1].getX()] = false;
                        boardContent[tmpBlock[1, 0].getY(), tmpBlock[1, 0].getX()] = false;
                        boardContent[tmpBlock[1, 1].getY(), tmpBlock[1, 1].getX()] = false;
                       
                        boardContent[tmpBlock[0, 0].getY(), (tmpBlock[0, 0].getX() - 1)] = true;
                        boardContent[tmpBlock[0, 1].getY(), (tmpBlock[0, 1].getX() - 1)] = true;
                        boardContent[tmpBlock[1, 0].getY(), (tmpBlock[1, 0].getX() - 1)] = true;
                        boardContent[tmpBlock[1, 1].getY(), (tmpBlock[1, 1].getX() - 1)] = true;

                        currentFallingBlock.setPointA(new Cordenates((tmpBlock[0, 0].getX() - 1), tmpBlock[0, 0].getY()));
                        currentFallingBlock.setPointB(new Cordenates((tmpBlock[0, 1].getX() - 1), tmpBlock[0, 1].getY()));
                        currentFallingBlock.setPointC(new Cordenates((tmpBlock[1, 0].getX() - 1), tmpBlock[1, 0].getY()));
                        currentFallingBlock.setPointD(new Cordenates((tmpBlock[1, 1].getX() - 1), tmpBlock[1, 1].getY()));
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    
                    checkFullLines();

                    
                    addFallingBlock = true;

                }
            }
        }     
        private static bool checkBotomColision(Cordenates oneNew, Cordenates twoNew, Cordenates threeNew, Cordenates fourNew)
        {
         
            if(oneNew.getY()>=boardHeight || twoNew.getY()>= boardHeight || threeNew.getY() >= boardHeight || fourNew.getY() >= boardHeight)
            {
                return true;
            }
          
            else
            {
                Console.WriteLine("Game Over!!!");
                return false;
            }
        }

        private static bool checkSideColision(Cordenates oneNew, Cordenates twoNew, Cordenates threeNew, Cordenates fourNew)
        {
           
            if (oneNew.getX() <= -1 || twoNew.getX() <= -1 || threeNew.getX() <= -1 || fourNew.getX() <= -1)
            {
                return true;
            }
           
            else if (oneNew.getX() >= boardWidth || twoNew.getX() >= boardWidth || threeNew.getX() >= boardWidth || fourNew.getX() >= boardWidth)
            {
                return true;
            }
            else
            {               
                return false;
            }
        }

        private static bool checkDownMovementColision(Cordenates oneNew, Cordenates twoNew, Cordenates threeNew, Cordenates fourNew)
        {
            if(currentFallingBlock is BlockLine && 
                (boardContent[oneNew.getY(), oneNew.getX()] ||
                    boardContent[twoNew.getY(), twoNew.getX()] ||
                    boardContent[threeNew.getY(), threeNew.getX()] ||
                    boardContent[fourNew.getY(), fourNew.getX()]))
            {
                return true;

            }else if(currentFallingBlock is BlockSquare && (
                    boardContent[threeNew.getY(), threeNew.getX()] ||
                    boardContent[fourNew.getY(), fourNew.getX()]))
            {
                return true;
            }
            else
            {               
                return false;
            }
        }
        private static bool checkLeftMovementColision(Cordenates oneNew, Cordenates twoNew, Cordenates threeNew, Cordenates fourNew)
        {
            if (currentFallingBlock is BlockLine &&
               (boardContent[oneNew.getY(), oneNew.getX()]))
            {
                 return true;

            }
            else if (currentFallingBlock is BlockSquare && (
                   boardContent[oneNew.getY(), oneNew.getX()] ||
                   boardContent[threeNew.getY(), threeNew.getX()]))
            {
                 return true;
            }
            else
            {              
                return false;
            }
        }
        private static bool checkRightMovementColision(Cordenates oneNew, Cordenates twoNew, Cordenates threeNew, Cordenates fourNew)
        {
            if (currentFallingBlock is BlockLine &&
               (boardContent[fourNew.getY(), fourNew.getX()]))
            {
                  return true;

            }
            else if (currentFallingBlock is BlockSquare && (
                   boardContent[twoNew.getY(), twoNew.getX()] ||
                   boardContent[fourNew.getY(), fourNew.getX()]))
            {
                  return true;
            }
            else
            {               
                return false;
            }
        }
   
        public static void checkFullLines()
        {
            for (int i=0; i <= boardHeight - 1; i++)
            {
               
                if (checkLine(i))
                {
                                       
                    moveLines(i);

                   
                    board.upLines();
                   
                    i = 0;
                }
            }
        }

        public static bool checkLine(int lineNumber)
        {
            bool isLineFull = true;
            for (int k = 0; k <= boardWidth - 1; k++)
            {
                if (boardContent[lineNumber, k] == false)
                {
                    isLineFull = false; 
                    return false;
                }
                else
                {
                    continue;
                }
            }
                
            return isLineFull;
        }

        public static void moveLines(int current)
        {
            for (int i = current; i >= 1; i--)
            {
               
                for (int k = 0; k <= boardWidth - 1; k++)
                {
                    boardContent[i, k] = boardContent[i - 1, k];
                }
            }
        }     
        private static void addBlockToGame(Block block)
        {
            if(block is BlockLine)
            {
                
                BlockLine b = (BlockLine)block;
               
                Cordenates[] tmpBlock = b.getBlockLine();

             
                if(!boardContent[tmpBlock[0].getY(), tmpBlock[0].getX()] &&
                    !boardContent[tmpBlock[1].getY(), tmpBlock[1].getX()] &&
                    !boardContent[tmpBlock[2].getY(), tmpBlock[2].getX()] &&
                    !boardContent[tmpBlock[3].getY(), tmpBlock[3].getX()])
                {
                    boardContent[tmpBlock[0].getY(), tmpBlock[0].getX()] = true;
                    boardContent[tmpBlock[1].getY(), tmpBlock[1].getX()] = true;
                    boardContent[tmpBlock[2].getY(), tmpBlock[2].getX()] = true;
                    boardContent[tmpBlock[3].getY(), tmpBlock[3].getX()] = true;
                  
                    addFallingBlock = false;
                    currentFallingBlock = block;

                }
                else
                {
                    gameRunning = false;
                }
            }
            else if(block is BlockSquare)
            {
             
                BlockSquare b = (BlockSquare)block;
               
                Cordenates[,] tmpBlock = b.getBlockSquare();

             
                if (!boardContent[tmpBlock[0,0].getY(), tmpBlock[0,0].getX()] &&
                    !boardContent[tmpBlock[0,1].getY(), tmpBlock[0,1].getX()] &&
                    !boardContent[tmpBlock[1,0].getY(), tmpBlock[1,0].getX()] &&
                    !boardContent[tmpBlock[1,1].getY(), tmpBlock[1,1].getX()])
                {                   
                    boardContent[tmpBlock[0, 0].getY(), tmpBlock[0, 0].getX()] = true;
                    boardContent[tmpBlock[0, 1].getY(), tmpBlock[0, 1].getX()] = true;
                    boardContent[tmpBlock[1, 0].getY(), tmpBlock[1, 0].getX()] = true;
                    boardContent[tmpBlock[1, 1].getY(), tmpBlock[1, 1].getX()] = true;
                    
                    addFallingBlock = false;
                    currentFallingBlock = block;
                }
                else
                {
                    gameRunning = false;
                }

            }
        }
        private static Block generateTetrisBlock()
        {
           
            int xMidle = boardWidth / 2;

           
            if (new Random().Next(0, 2) == 0)
            {
                return new BlockLine(new Cordenates(xMidle-1,0), new Cordenates(xMidle, 0), new Cordenates(xMidle + 1, 0), new Cordenates(xMidle + 2, 0));
            }
            else
            {
                return new BlockSquare(new Cordenates(xMidle - 1, 0), new Cordenates(xMidle, 0), new Cordenates(xMidle - 1, 1), new Cordenates(xMidle, 1));
            }
        }
    }
}
