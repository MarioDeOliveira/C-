using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Projeto_I_OneTetris
{
    class BlockLine : Block
    {
        public BlockLine(Cordenates one, Cordenates two, Cordenates three, Cordenates four)
        {
            base.a = one;
            base.b = two;
            base.c = three;
            base.d = four;
        }
        public Cordenates[] getBlockLine()
        {
            return new Cordenates[] { base.a, base.b, base.c, base.d };
        }
    }
}
