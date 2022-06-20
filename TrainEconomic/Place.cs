using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainEconomic
{
    internal class Place
    {
        private List<int> valutesCount = new List<int>();
        private List<int> tmpValutesCount = new List<int>();
        public Place(List<int> valutesCount)
        {
            this.valutesCount = valutesCount;
            tmpValutesCount = valutesCount;
        }        

        public int GetValuteCount(int index)
        {
            return valutesCount[index];
        }
        public int GetValuteTmpCount(int index)
        {
            return tmpValutesCount[index];
        }
        public void SetValutesCount(List<int> tmpValutesCount)
        {
            this.tmpValutesCount = tmpValutesCount;
        }
        public void SetValutesCount(int index, int newValue)
        {
            tmpValutesCount[index] = newValue;
        }
        public void ReturnValue()
        {
            tmpValutesCount = valutesCount;
        }
        public void SaveValutes()
        {
            valutesCount = tmpValutesCount;
        }
    }
}
