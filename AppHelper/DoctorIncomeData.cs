using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppHelper
{
    public class DoctorIncomeDataList
    {
        private List<DoctorIncomeData> lstDoctorIncomeDataList = null;

        public List<DoctorIncomeData> LstDoctorIncomeDataList
        {
            get { return lstDoctorIncomeDataList; }
            set { lstDoctorIncomeDataList = value; }
        }
        public DoctorIncomeDataList()
        {
            lstDoctorIncomeDataList = new List<DoctorIncomeData>();
        }
        public DoctorIncomeData GetEmptyDoctorIncomeRow()
        {
            DoctorIncomeData doctorIncomeData = new DoctorIncomeData();
            return doctorIncomeData;
        }
    }

    public class DoctorIncomeData
    {
        private string name=string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string phone = string.Empty;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private DateTime firstConsultDate;
        public DateTime FirstConsultDate
        {
            get { return firstConsultDate; }
            set { firstConsultDate = value; }
        }

        private string statusGroup = string.Empty;
        public string StatusGroup
        {
            get { return statusGroup; }
            set { statusGroup = value; }
        }

        private string statusName = string.Empty;
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }

        private string plSource = string.Empty;
        public string PlSource
        {
            get { return plSource; }
            set { plSource = value; }
        }

        private float txCost=0;
        public float TxCost
        {
            get { return txCost; }
            set { txCost = value; }
        }

        private double ageing=0;
        public double Ageing
        {
            get { return ageing; }
            set { ageing = value; }
        }

        private string treatment = string.Empty;
        public string Treatment
        {
            get { return treatment; }
            set { treatment = value; }
        }

        private string source = string.Empty;

        public string Source
        {
            get { return source; }
            set { source = value; }
        }

    }
}