using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCApplication.Models
{
    public class Category
    {
       //NOt set any validations for CategoyID
        public int CategoryID { get; set; }
        [Required(ErrorMessage="Category Name is required")]
        [StringLength(50,ErrorMessage ="CategoryName cannot exceed 50 characters")]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }


    public class CategoryDataAccess
    {
        SqlConnection connection;
        const string defaultConnectionString = "server=VAAYU\\SQLDEV2016; database=northwind:user id=sa;pwd=sa";


        public CategoryDataAccess()
        {
            var Connstr = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(Connstr)) Connstr = defaultConnectionString;
            connection = new SqlConnection(Connstr);
        }

        public IEnumerable<Category> GetCategories()
        {
            string sql = "SELECT CategoryId, CategoryName, Description FROM Categories";
            List<Category> categoryList = new List<Category>();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            SqlDataReader reader = null;
            try
            {
                connection.Open();
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    categoryList.Add(new Category
                    {
                        CategoryID = Convert.ToInt32("0" + reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString()
                    });
                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null) if (!reader.IsClosed) reader.Close();
                if (connection.State != ConnectionState.Closed) connection.Close();
            }
            return categoryList;
        }


        public Category GetCategory(int categoryId)
        {
            string sql = "SELECT CategoryId, CategoryName, Description FROM Categories";
            sql += " WHERE CategoryId=@categoryId";
            Category categoryObj = new Category();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@categoryId", categoryId);
            SqlDataReader reader = null;
            try
            {
                connection.Open();
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    categoryObj = new Category
                    {
                        CategoryID = Convert.ToInt32("0" + reader["CategoryId"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString()
                    };
                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (reader != null) if (!reader.IsClosed) reader.Close();
                if (connection.State != ConnectionState.Closed) connection.Close();
            }
            return categoryObj;
        }

        public void CreateOrUpdate(Category item, char createOrUpdate = 'C')
        {
            string sql = string.Empty;
            if (char.ToUpper(createOrUpdate) == 'C')
            {
                sql = " INSERT INTO Categories (CategoryName, Description) ";
                sql += " VALUES (@name, @description) ";
            }
            else if (char.ToUpper(createOrUpdate) == 'U')
            {
                sql = " UPDATE Categories SET CategoryName=@name, ";
                sql += " Description=@description WHERE CategoryId=@categoryId ";
            }
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@name", item.CategoryName);
            command.Parameters.AddWithValue("@description", item.Description);
            if (createOrUpdate == 'U')
                command.Parameters.AddWithValue("@categoryId", item.CategoryID);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally
            {
                if (connection.State != ConnectionState.Closed) connection.Close();
            }
        }


    }
}