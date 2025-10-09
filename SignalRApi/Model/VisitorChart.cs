using System.Collections.Generic;

namespace SignalRApi.Model
{
    public class VisitorChart
    {
        public VisitorChart()
        {
            Counts = new List<int>();
        }

        public string VisitDate { get; set; }  //ziyaret tarihi
        public List<int> Counts { get; set; }   //bu tarihte ilgili şehri kaç kişi ziyaret etmiş
    }
}
