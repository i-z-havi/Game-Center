using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Center.Projects.BezierCurveProject.Models
{
    class CurveList
    {
        public ObservableCollection<CurvePoints> Points { get; set; }
        public CurveList() 
        {
        
        }
    }
}
