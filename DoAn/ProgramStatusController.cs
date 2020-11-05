using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn
{
    class ProgramStatusController
    {
        static public bool getThoiHanDiemDanh()
        {
            int nowHour = DateTime.Now.Hour;
            if (nowHour >= 9) return false;
            return true;
        }

        static public List<string> LopDaDiemDanh = new List<string>();
    }
}
