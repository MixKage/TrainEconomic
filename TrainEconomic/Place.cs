using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainEconomic
{
    internal class Place
    {
        public List<int> valutesCount = new List<int>();
        public Place(List<int> valutesCount)
        {
            this.valutesCount = valutesCount;
        }
    }
}
