using Microsoft.Data.SqlClient;
using Product.Models;
using System.Data;

namespace Product.Views
{
    public class ChecklistItemView
    {
        public static List<Checklist> SelectChecklistItem(int checklistId)
        {
            try
            {
                List<Checklist> result = new List<Checklist>();

                string query = @"SELECT *
                                FROM ChecklistItem where FK_Checklist = @id";

                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.VarChar){ Value = checklistId }
                };

                DataTable dt = CRUD.ExecuteQuery(query, sqlParams);
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
    }
}
