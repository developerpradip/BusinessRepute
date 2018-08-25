using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AppHelper
{
    public class CSVHandler
    {
        string resultStorageFolder = string.Empty;
        string resultStorageFilePath = string.Empty;
        string dataFolderName = string.Empty;
        string googleDataFolderName = string.Empty;
        TextWriter textWriter = null;

        public CSVHandler()
        {
            resultStorageFolder = ConfigurationSettings.AppSettings["RESULTSTOREPATH"];
            dataFolderName = ConfigurationSettings.AppSettings["DATAFOLDERNAME"];
            googleDataFolderName = ConfigurationSettings.AppSettings["HOUZZDATAFOLDERNAME"];

            if (string.IsNullOrEmpty(resultStorageFolder))
            {
                resultStorageFolder = Environment.CurrentDirectory;
            }
        }
        public string GetFileName(string reviewSiteName, string term, string location)
        {
            location = location.Replace(",", "_");
            location = location.Replace(".", "_");
            location = location.Replace(" ", "_");

            term = term.Replace(",", "_");
            term = term.Replace(".", "_");
            term = term.Replace(" ", "_");



            string fileName = reviewSiteName + "_ExportResult_" + term + "_" + location + ".csv";
            return fileName;
        }

        public string GetFileName(string reviewSiteName, string searchText)
        {
            searchText = searchText.Replace(",", "_");
            searchText = searchText.Replace(".", "_");
            searchText = searchText.Replace(" ", "_");

            string fileName = reviewSiteName + "_ExportResult_" + searchText.Trim() + ".csv";
            return fileName;
        }


        public string Write(string fileName, string webpath, DataTable detailedDataTable)
        {
            // create a writer and open the file
            if (!fileName.EndsWith(".csv"))
                fileName += ".csv";


            string webFolderPath = webpath.Substring(0, webpath.LastIndexOf("/"));
            string fileWebPath = webFolderPath + "/" + dataFolderName + "/" + fileName;
            string fileStorageFilePath = resultStorageFolder + dataFolderName + "/" + fileName;

            try
            {
                if (File.Exists(fileStorageFilePath))
                {
                    File.Delete(fileStorageFilePath);
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
            string header = string.Empty;
            string rowText = string.Empty;
            if (detailedDataTable.Rows.Count > 0)
            {
                if (textWriter == null)
                {
                    textWriter = new StreamWriter(fileStorageFilePath);
                }
                header = CreateHeader(detailedDataTable);
                if (!string.IsNullOrEmpty(header))
                {
                    textWriter.WriteLine(header);

                    foreach (DataRow dataRow in detailedDataTable.Rows)
                    {
                        string joinedStr = string.Join(", ", dataRow.ItemArray.Select(p => p.ToString().Replace(",", ";")).ToArray());
                        textWriter.WriteLine(joinedStr);
                    }
                }
            }
            return fileWebPath;
        }

        public string Write(string fileName, string webpath, DoctorIncomeDataList displayResult)
        {
            // create a writer and open the file
            if (!fileName.EndsWith(".csv"))
                fileName += ".csv";

            //string webpath = HttpContext.Current.Request.Url.AbsoluteUri;
            string webFolderPath = webpath.Substring(0, webpath.LastIndexOf("/"));
            string fileWebPath = webFolderPath + "/" + dataFolderName + "/" + fileName;
            string fileStorageFilePath = resultStorageFolder + dataFolderName + "/" + fileName;

            try
            {
                if (File.Exists(fileStorageFilePath))
                {
                    File.Delete(fileStorageFilePath);
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
            string header = string.Empty;
            string rowText = string.Empty;
            if (displayResult.LstDoctorIncomeDataList.Count > 0)
            {
                if (textWriter == null)
                {
                    textWriter = new StreamWriter(fileStorageFilePath);
                }

                string reportHeader =string.Empty;
                if (fileName.StartsWith("Conversion_Report"))
                {
                    reportHeader = "Conversion Report as of " + DateTime.Today.ToShortDateString();
                }
                else
                {
                    reportHeader = "Potential Profit/Loss Value Report as of " + DateTime.Today.ToShortDateString();
                }
                
                header = "Name, Email, Phone, First Consult, Treatment, Source, Status Group, Status, Treatment Cost, Aging";
                header = reportHeader + Environment.NewLine + Environment.NewLine + Environment.NewLine + header;

                if (!string.IsNullOrEmpty(header))
                {
                    textWriter.WriteLine(header);

                    foreach (DoctorIncomeData doctorIncomeData in displayResult.LstDoctorIncomeDataList)
                    {
                        string joinedStr = doctorIncomeData.Name.Replace(",", " ") + ", ";
                        joinedStr += doctorIncomeData.Email.Replace(",", ";") + ", ";
                        joinedStr += doctorIncomeData.Phone.Replace(",", ";") + ", ";
                        string firstConsultDate = (doctorIncomeData.FirstConsultDate == null) ? string.Empty : doctorIncomeData.FirstConsultDate.ToShortDateString();
                        joinedStr += ((firstConsultDate.Equals("01/01/0001")) || (firstConsultDate.Equals("1/1/0001")) == true) ? string.Empty : firstConsultDate + ", ";
                        joinedStr += doctorIncomeData.Treatment.Replace(",", ";") + ", ";
                        joinedStr += doctorIncomeData.Source.Replace(",", ";") + ", ";
                        joinedStr += doctorIncomeData.StatusGroup.Replace(",", ";") + ", ";
                        joinedStr += doctorIncomeData.StatusName + ", ";
                        joinedStr += (doctorIncomeData.TxCost == 0) ? string.Empty : doctorIncomeData.TxCost.ToString("C2").Replace(",", "") + ", ";
                        joinedStr += (doctorIncomeData.Ageing == 0) ? string.Empty : doctorIncomeData.Ageing + ", ";
                        textWriter.WriteLine(joinedStr);
                    }
                }
            }
            return fileWebPath;
        }

        public void Clear()
        {
            if (textWriter != null)
            {
                textWriter.Close();
                textWriter = null;
            }
        }

        public string CreateHeader(DataTable detailedDataTable)
        {
            string columnHeader = string.Empty;
            if (detailedDataTable != null)
            {
                if (detailedDataTable.Rows.Count > 0)
                {
                    foreach (DataColumn dataColumn in detailedDataTable.Columns)
                    {
                        columnHeader += dataColumn.ColumnName.ToString() + ",";
                    }
                }
            }

            return columnHeader;
        }

        public string Write(CSVHandler csvHandler, string webpath, DoctorIncomeDataList doctorIncomeDataList)
        {
            throw new NotImplementedException();
        }

        public string Read(CSVHandler csvHandler, string webpath, DoctorIncomeDataList doctorIncomeDataList)
        {
            throw new NotImplementedException();
        }

        public PatientTreatmentist ReadFile(string filePath)
        {
            PatientTreatmentist patientTreatmentist = new PatientTreatmentist();
            PatientTreatmentData patientTreatmentData = null;
            try
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(filePath)))
                {
                    char[] separator = { ',' };

                    string status = "0";
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        string[] columns = line.Split(separator);
                        patientTreatmentData = new PatientTreatmentData();

                        if ((columns[0] != null) && (!string.IsNullOrEmpty(columns[0])))
                        {
                            if(string.Compare(columns[0], "patient", true) == 0)
                            {
                                continue;
                            }
                            patientTreatmentData.PatientName = columns[0];
                        }

                        if ((columns[1] != null) && (!string.IsNullOrEmpty(columns[1])))
                        {
                            patientTreatmentData.PatientSource = columns[1];
                        }

                        if ((columns[2] != null) && (!string.IsNullOrEmpty(columns[2])))
                        {
                            patientTreatmentData.TreatmentTypeName = columns[2];
                            //store the treatment type length

                        }


                        if ((columns[3] != null) && (!string.IsNullOrEmpty(columns[3])))
                        {
                            DateTime tempFirstDateTime;
                            if (DateTime.TryParse(columns[3], out tempFirstDateTime) == true)
                            {
                                patientTreatmentData.FirstConsultDate = tempFirstDateTime;
                            }
                        }

                        if ((columns[4] != null) && (!string.IsNullOrEmpty(columns[4])))
                        {
                            if ((!string.IsNullOrEmpty(columns[2])))
                            {
                                patientTreatmentData.StatusName = columns[4]; 
                            }
                            else
                            {
                                status = columns[4];
                            }
                        }

                        if ((columns[5] != null) && (!string.IsNullOrEmpty(columns[5])))
                        {
                            float tempTreatmentFee = 0;
                            if(float.TryParse(columns[5], out tempTreatmentFee))
                            {
                                patientTreatmentData.TreatmentFee = tempTreatmentFee;
                                patientTreatmentData.TtSinglePayCost = tempTreatmentFee;
                            }
                        }

                        if ((columns[6] != null) && (!string.IsNullOrEmpty(columns[6])))
                        {
                            patientTreatmentData.Notes = columns[6];
                        }
                        if (!string.IsNullOrEmpty(patientTreatmentData.PatientName) && !string.IsNullOrEmpty(patientTreatmentData.PatientSource) && !string.IsNullOrEmpty(patientTreatmentData.TreatmentTypeName)
                            && !string.IsNullOrEmpty(patientTreatmentData.FirstConsultDate.ToString()) && !string.IsNullOrEmpty(patientTreatmentData.StatusName))
                        {
                            patientTreatmentist.GetPatienttreatementList().Add(patientTreatmentData);
                            YelpTrace.Write("Read details about patient " + patientTreatmentData.PatientName);
                        }
                    }
                    YelpTrace.Write("Summary : total " + patientTreatmentist.GetPatienttreatementList().Count.ToString() + " records are found");
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
            finally
            { 
            
            }
            return patientTreatmentist;
        }
    }
}
