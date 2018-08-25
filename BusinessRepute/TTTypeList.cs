using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessRepute
{
    public class TTTypeList
    {
        List<TTType> tTTypeList = null;


        public TTTypeList()
        {
            tTTypeList = new List<TTType>();
        }
        //public TTTypeList()
        //{
        //    tTTypeList = new List<TTType>();
        //}

        public List<TTType> GetTTTypeList
        {
            get { return tTTypeList; }
            set { tTTypeList = value; }
        }

        
    }

    public class TTType
    {
        int ttTypeId;

        public int TtTypeId
        {
            get { return ttTypeId; }
            set { ttTypeId = value; }
        }
        string tTname;

        public string TTname
        {
            get { return tTname; }
            set { tTname = value; }
        }
    }
}