using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_I_OneTetris
{
    class BlockSquare : Block
    {

        public BlockSquare(Cordenates one, Cordenates two, Cordenates three, Cordenates four)
        {
            base.a = one;
            base.b = two;
            base.c = three;
            base.d = four;
        }

        public Cordenates[,] getBlockSquare()
        {
            return new Cordenates[,] { { base.a, base.b }, { base.c, base.d } };
        }
    }
}
