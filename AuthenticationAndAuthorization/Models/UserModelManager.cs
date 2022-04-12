using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AuthenticationAndAuthorization.Models
{
    public class UserModelManager
    {
        public List<UserLoginModel> GetUserRegistration()
        {
            List<UserLoginModel> userLoginModel = new List<UserLoginModel>();
            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SPGetUserRegistration", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            UserLoginModel ouserLoginModel = new UserLoginModel();
                            ouserLoginModel.UserName = Convert.ToString(dr["StudentName"]);
                            ouserLoginModel.UserEmail = Convert.ToString(dr["StudentEmail"]);
                            ouserLoginModel.Password = Convert.ToString(dr["StudentPassword"]);
                            ouserLoginModel.Role = Convert.ToString(dr["Role"]);

                            userLoginModel.Add(ouserLoginModel);

                        }
                        dr.Close();


                    }
                    catch (Exception ex)
                    {
                        var excetion = ex;
                    }


                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }



                }
            }
            return userLoginModel;

        }

        public void InsertUserDetails(UserLoginModel userLoginModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SPInsertUserDetails", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        cmd.Parameters.AddWithValue("@StudentName", userLoginModel.UserName);
                        cmd.Parameters.AddWithValue("@StudentEmail", userLoginModel.UserEmail);
                        cmd.Parameters.AddWithValue("@StudentPassword", userLoginModel.Password);
                        cmd.Parameters.AddWithValue("@Role", userLoginModel.Role);
                       
                        cn.Open();
                        int count = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }

                }
            }

        }
        public UserLoginModel UserAuth(UserLoginModel userLoginModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using (SqlConnection cn=new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SPValidateUserInfo", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserEmail", System.Data.SqlDbType.VarChar, 40).Value = userLoginModel.UserEmail;
                    cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 40).Value = userLoginModel.Password;
                    try
                    {
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["StudentEmail"] != DBNull.Value && dr["Role"] != DBNull.Value)
                            {
                                userLoginModel.UserEmail = Convert.ToString(dr["StudentEmail"]);
                                userLoginModel.Role = Convert.ToString(dr["Role"]);
                                userLoginModel.UserName = Convert.ToString(dr["StudentName"]);
                                userLoginModel.IsValid = 1;

           
                            }
                            else
                            {
                                userLoginModel.IsValid = 0;
                            }
                        }
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cn.State == System.Data.ConnectionState.Open)
                        {
                            cn.Close();

                        }
                    }

                }
            }
            return userLoginModel;
        }
    }
}