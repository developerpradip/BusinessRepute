using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHelper
{
    public class PatientTreatmentist
    {
        List<PatientTreatmentData> lstPatienttreatementList = null;
        public PatientTreatmentist()
        {
            lstPatienttreatementList = new List<PatientTreatmentData>();
        }

        public List<PatientTreatmentData> GetPatienttreatementList()
        {
            return lstPatienttreatementList;
        }

    }
    public class PatientTreatmentData
    {
        private string patientName= string.Empty;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }
        private string patientPhone = string.Empty;

        public string PatientPhone
        {
            get { return patientPhone; }
            set { patientPhone = value; }
        }
        private string patientEmail = string.Empty;

        public string PatientEmail
        {
            get { return patientEmail; }
            set { patientEmail = value; }
        }
        private int userid = -1;

        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        private int active = -1;

        public int Active
        {
            get { return active; }
            set { active = value; }
        }
        private int createdBy=-1;

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        private string patientSource= string.Empty;

        public string PatientSource
        {
            get { return patientSource; }
            set { patientSource = value; }
        }
        private int patientId=-1;

        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        private string treatmentTypeName= string.Empty;

        public string TreatmentTypeName
        {
            get { return treatmentTypeName; }
            set { treatmentTypeName = value; }
        }
        private int treatmentTypeId=-1;

        public int TreatmentTypeId
        {
            get { return treatmentTypeId; }
            set { treatmentTypeId = value; }
        }
        private int coordinatorId=-1;

        public int CoordinatorId
        {
            get { return coordinatorId; }
            set { coordinatorId = value; }
        }
        private int doctorId=-1;

        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }
        private DateTime firstConsultDate;

        public DateTime FirstConsultDate
        {
            get { return firstConsultDate; }
            set { firstConsultDate = value; }
        }
        private string statusName= string.Empty;

        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }
        private int status=-1;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private int treatmentActive=-1;

        public int TreatmentActive
        {
            get { return treatmentActive; }
            set { treatmentActive = value; }
        }
        private int treatmentLength=0;

        public int TreatmentLength
        {
            get { return treatmentLength; }
            set { treatmentLength = value; }
        }
        private float treatmentFee=0;

        public float TreatmentFee
        {
            get { return treatmentFee; }
            set { treatmentFee = value; }
        }
        private float treatmentInsurance=0;

        public float TreatmentInsurance
        {
            get { return treatmentInsurance; }
            set { treatmentInsurance = value; }
        }
        private string ttAdditionalOpt1Text= string.Empty;

        public string TtAdditionalOpt1Text
        {
            get { return ttAdditionalOpt1Text; }
            set { ttAdditionalOpt1Text = value; }
        }
        private float ttAdditionalOpt1Value=0;

        public float TtAdditionalOpt1Value
        {
            get { return ttAdditionalOpt1Value; }
            set { ttAdditionalOpt1Value = value; }
        }
        private string ttAdditionalOpt2Text= string.Empty;

        public string TtAdditionalOpt2Text
        {
            get { return ttAdditionalOpt2Text; }
            set { ttAdditionalOpt2Text = value; }
        }
        private float ttAdditionalOpt2Value=0;

        public float TtAdditionalOpt2Value
        {
            get { return ttAdditionalOpt2Value; }
            set { ttAdditionalOpt2Value = value; }
        }
        private string ttAdditionalOpt3Text= string.Empty;

        public string TtAdditionalOpt3Text
        {
            get { return ttAdditionalOpt3Text; }
            set { ttAdditionalOpt3Text = value; }
        }
        private float ttAdditionalOpt3Value=0;

        public float TtAdditionalOpt3Value
        {
            get { return ttAdditionalOpt3Value; }
            set { ttAdditionalOpt3Value = value; }
        }
        private float ttSinglePayDiscountAmt=0;

        public float TtSinglePayDiscountAmt
        {
            get { return ttSinglePayDiscountAmt; }
            set { ttSinglePayDiscountAmt = value; }
        }
        private float ttSinglePayCost= 0;

        public float TtSinglePayCost
        {
            get { return ttSinglePayCost; }
            set { ttSinglePayCost = value; }
        }
        private float ttEmiMonthlyPayDownPayAmt=0;

        public float TtEmiMonthlyPayDownPayAmt
        {
            get { return ttEmiMonthlyPayDownPayAmt; }
            set { ttEmiMonthlyPayDownPayAmt = value; }
        }
        private float ttEmiMonthlyPayMonthlyAmt=0;

        public float TtEmiMonthlyPayMonthlyAmt
        {
            get { return ttEmiMonthlyPayMonthlyAmt; }
            set { ttEmiMonthlyPayMonthlyAmt = value; }
        }
        private float ttEmiMonthlyPayMonths=0;

        public float TtEmiMonthlyPayMonths
        {
            get { return ttEmiMonthlyPayMonths; }
            set { ttEmiMonthlyPayMonths = value; }
        }
        private float ttNoDOwnPayMonthlyAmt=0;

        public float TtNoDOwnPayMonthlyAmt
        {
            get { return ttNoDOwnPayMonthlyAmt; }
            set { ttNoDOwnPayMonthlyAmt = value; }
        }
        private float ttNoDOwnPayMonths=0;

        public float TtNoDOwnPayMonths
        {
            get { return ttNoDOwnPayMonths; }
            set { ttNoDOwnPayMonths = value; }
        }
        private string ttOtherPayType= string.Empty;

        public string TtOtherPayType
        {
            get { return ttOtherPayType; }
            set { ttOtherPayType = value; }
        }
        private float ttOtherPayFrequency=0;

        public float TtOtherPayFrequency
        {
            get { return ttOtherPayFrequency; }
            set { ttOtherPayFrequency = value; }
        }
        private float ttOtherPayAmount=0;

        public float TtOtherPayAmount
        {
            get { return ttOtherPayAmount; }
            set { ttOtherPayAmount = value; }
        }

        private string notes= string.Empty;

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

    }
}
