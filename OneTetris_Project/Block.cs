using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_I_OneTetris
{
    class Block
    {

        

        protected Cordenates a;
        protected Cordenates b;
        protected Cordenates c;
        protected Cordenates d;


        public Block()
        {

        }

        public void setPointA(Cordenates point)
        {
            a = point;
        }

        public void setPointB(Cordenates point)
        {
            b = point;
        }

        public void setPointC(Cordenates point)
        {
            c = point;
        }

        public void setPointD(Cordenates point)
        {
            d = point;
        }
    }
}
