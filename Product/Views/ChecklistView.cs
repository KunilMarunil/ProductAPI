using Microsoft.Data.SqlClient;
using Product.Models;
using System.Data;

namespace Product.Views
{
    public class ChecklistView
    {
        public static List<Checklist> SelectChecklist()
        {
            try
            {
                List<Checklist> result = new List<Checklist>();

                string query = @"SELECT *
                                FROM Checklist";


                DataTable dt = CRUD.ExecuteQuery(query);
                foreach (DataRow row in dt.Rows)
                {
                    Checklist tempData = new Checklist
                    {
                        Name = (string)row["Name"],
                    };

                    result.Add(tempData);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void InsertChecklist(Checklist Checklist)
        {
            string query = "INSERT INTO [dbo].[Checklist] (Name) VALUES (@Name)";
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.VarChar){ Value = Checklist.Name }
            };

            CRUD.ExecuteNonQuery(query, sqlParams);
        }

        public static void DeleteChecklist(int checklistId)
        {
            string query = @"DELETE Checklist WHERE PK_Checklist = @checklistId";

            SqlParameter[] sqlParams = new SqlParameter[]
            {
            new SqlParameter("@checklistId", SqlDbType.VarChar){ Value = checklistId }
            };

            CRUD.ExecuteNonQuery(query, sqlParams);
        }
    }
}
