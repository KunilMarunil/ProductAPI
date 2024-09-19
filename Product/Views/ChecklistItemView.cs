using Microsoft.Data.SqlClient;
using Product.Models;
using System.Data;

namespace Product.Views
{
    public class ChecklistItemView
    {
        public static List<ChecklistItem> SelectChecklistItem(int checklistId)
        {
            try
            {
                List<ChecklistItem> result = new List<ChecklistItem>();

                string query = @"SELECT * FROM ChecklistItem where FK_Checklist = @id";

                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.VarChar){ Value = checklistId }
                };

                DataTable dt = CRUD.ExecuteQuery(query, sqlParams);
                foreach (DataRow row in dt.Rows)
                {
                    ChecklistItem tempData = new ChecklistItem
                    {
                        ItemName = (string)row["ItemName"],
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
        public static void InsertChecklistItem(int checklistId, ChecklistItem Checklist)
        {
            string query = "INSERT INTO [dbo].[ChecklistItem] (ItemName, FK_Checklist) VALUES (@Name, @id)";
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.VarChar){ Value = Checklist.ItemName },
                new SqlParameter("@id", SqlDbType.VarChar){ Value = checklistId }
            };

            CRUD.ExecuteNonQuery(query, sqlParams);
        }
        public static List<ChecklistItem> SelectedChecklistItem(int checklistId, int checklistItemId)
        {
            try
            {
                List<ChecklistItem> result = new List<ChecklistItem>();

                string query = @"SELECT * FROM ChecklistItem where FK_Checklist = @id and PK_ChecklistItem = @idItem";

                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.VarChar){ Value = checklistId },
                    new SqlParameter("@idItem", SqlDbType.VarChar){ Value = checklistItemId }
                };

                DataTable dt = CRUD.ExecuteQuery(query, sqlParams);
                foreach (DataRow row in dt.Rows)
                {
                    ChecklistItem tempData = new ChecklistItem
                    {
                        ItemName = (string)row["ItemName"],
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
        public static void UpdateChecklistItem(int checklistId, int checklistItemId)
        {
            string query = @"Update ChecklistItem set FK_Checklist = @id where PK_ChecklistItem = @idItem";

            SqlParameter[] sqlParams = new SqlParameter[]
            {
            new SqlParameter("@id", SqlDbType.VarChar){ Value = checklistId },
            new SqlParameter("@idItem", SqlDbType.VarChar){ Value = checklistItemId },
            };

            CRUD.ExecuteNonQuery(query, sqlParams);
        }
        public static void DeleteChecklistItem(int checklistId, int checklistItemId)
        {
            string query = @"Delete ChecklistItem where FK_Checklist = @id and PK_ChecklistItem = @idItem";

            SqlParameter[] sqlParams = new SqlParameter[]
            {
            new SqlParameter("@id", SqlDbType.VarChar){ Value = checklistId },
            new SqlParameter("@idItem", SqlDbType.VarChar){ Value = checklistItemId },
            };

            CRUD.ExecuteNonQuery(query, sqlParams);
        }

        public static void RenameChecklistItem(int checklistId, int checklistItemId, ChecklistItem checklistItem)
        {

            string query = @"Update ChecklistItem set ItemName = @name where FK_Checklist = @id and PK_ChecklistItem = @idItem";

            SqlParameter[] sqlParams = new SqlParameter[]
            {
            new SqlParameter("@name", SqlDbType.VarChar){ Value = checklistItem.ItemName },
            new SqlParameter("@id", SqlDbType.VarChar){ Value = checklistId },
            new SqlParameter("@idItem", SqlDbType.VarChar){ Value = checklistItemId },
            };

            CRUD.ExecuteNonQuery(query, sqlParams);
        }
    }
}
