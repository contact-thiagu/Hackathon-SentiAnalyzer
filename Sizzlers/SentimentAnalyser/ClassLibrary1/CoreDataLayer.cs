using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;

namespace CoreDataLayer
{
    
   public class CoreDataLayer 
    {

        protected DbConnection database_ = null;
        protected SqlConnection connectionTrans_ = null;
        protected SqlTransaction transaction_ = null;

        protected bool isInTransaction_ = false;

        public bool IsInTransaction
		{
			get { return isInTransaction_; }
		}

        public CoreDataLayer()
		{
				database_ = new DbConnection();
			
		}
		
		/// <summary>
		/// Releases all file handles and all unmanaged memory resource 
		/// </summary>
		public void Dispose()
		{
			if(this.connectionTrans_!=null)
				this.connectionTrans_.Dispose();
			
		}

        public void BeginTransaction()
        {
            try
            {
                if (isInTransaction_ != true)
                {

                    connectionTrans_ = database_.GetConnection();
                    connectionTrans_.Open();

                    transaction_ = connectionTrans_.BeginTransaction();
                    isInTransaction_ = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            finally
            {

            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if (connectionTrans_ != null && transaction_ != null)
                {
                    transaction_.Rollback();
                    isInTransaction_ = false;
                    connectionTrans_.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (transaction_ != null)
                    transaction_ = null;
                if (connectionTrans_ != null)
                    connectionTrans_.Dispose();
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (connectionTrans_ != null && transaction_ != null)
                {
                    transaction_.Commit();
                    isInTransaction_ = false;
                    connectionTrans_.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                if (transaction_ != null)
                    transaction_ = null;
                if (connectionTrans_ != null)
                    connectionTrans_.Dispose();
            }
        }

  
        public DataSet ExecuteDataSet(string StoredProcedureName, object[] ParameterValues)
        {

           
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                if (StoredProcedureName.IndexOf("dbo.") < 0)
                    StoredProcedureName = "dbo." + StoredProcedureName;
                try
                {

                    if (!isInTransaction_)
                    {

                        DbConnection oDbConnection = new DbConnection();
                        using (SqlConnection oConn = oDbConnection.GetConnection())
                        {
                            oConn.Open();

                            using (SqlCommand objCommand = new SqlCommand())
                            {
                                objCommand.Connection = oConn;

                                objCommand.CommandText = StoredProcedureName;

                                objCommand.CommandType = CommandType.StoredProcedure;
                                objCommand.CommandTimeout = 240;

                                SqlCommandBuilder.DeriveParameters(objCommand);

                                int i = 0;
                                foreach (SqlParameter sp in objCommand.Parameters)
                                {
                                    if (sp.ParameterName == "@RETURN_VALUE")
                                        continue;

                                    sp.Value = ParameterValues[i];
                                    i++;
                                }

                                SqlDataAdapter objDataAdapter = new SqlDataAdapter();

                                objDataAdapter.SelectCommand = objCommand;
                                objDataAdapter.Fill(ds, "Results");
                            }

                        }

                        return ds;
                    }
                    else
                    {

                        using (SqlCommand objCommand = new SqlCommand())
                        {
                            objCommand.Connection = connectionTrans_;

                            objCommand.CommandText = StoredProcedureName;
                            objCommand.CommandType = CommandType.StoredProcedure;
                            objCommand.Transaction = transaction_;
                            objCommand.CommandTimeout = 240;


                            SqlCommandBuilder.DeriveParameters(objCommand);

                            int i = 0;
                            foreach (SqlParameter sp in objCommand.Parameters)
                            {
                                if (sp.ParameterName == "@RETURN_VALUE")
                                    continue;
                                sp.Value = ParameterValues[i];
                                i++;


                            }

                            SqlDataAdapter objDataAdapter = new SqlDataAdapter();

                            objDataAdapter.SelectCommand = objCommand;

                            objDataAdapter.Fill(ds, "Results");
                        }
                        return ds;
                    }

                   
                }
                catch (Exception ex)
                {
                   
                    throw ex;


                }

            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StoredProcedureName"></param>
        /// <param name="ParameterValues"></param>
        /// <param name="isNonQuery">bool isNonQuery - True (returns if insert, update or deleted statements success or not)
        /// - False (returns the first row of first column result value)</param>
        /// <returns></returns>
        public string ExecuteScalar(string StoredProcedureName, object[] ParameterValues, bool isNonQuery)
        {
            
                string retValue = string.Empty;

                try
                {

                    if (StoredProcedureName.IndexOf("dbo.") < 0)
                        StoredProcedureName = "dbo." + StoredProcedureName;
                    if (!isInTransaction_)
                    {
                        DbConnection oDbConnection = new DbConnection();
                        using (SqlConnection conn = oDbConnection.GetConnection())
                        {
                            conn.Open();
                            using (SqlCommand objCommand = new SqlCommand())
                            {
                                objCommand.Connection = conn;

                                objCommand.CommandText = StoredProcedureName;

                                objCommand.CommandType = CommandType.StoredProcedure;
                                objCommand.CommandTimeout = 240;

                                SqlCommandBuilder.DeriveParameters(objCommand);

                                int i = 0;
                                foreach (SqlParameter sp in objCommand.Parameters)
                                {
                                    if (sp.ParameterName == "@RETURN_VALUE")
                                    {
                                        sp.Direction = ParameterDirection.ReturnValue;
                                        continue;
                                    }
                                    
                                        sp.Value = ParameterValues[i];
                                    

                                    i++;


                                }

                                if (isNonQuery)
                                {
                                    objCommand.ExecuteNonQuery();
                                    if (objCommand.Parameters["@RETURN_VALUE"] != null && objCommand.Parameters["@RETURN_VALUE"].Value != null)
                                        retValue = objCommand.Parameters["@RETURN_VALUE"].Value.ToString();

                                }
                                else
                                {
                                    retValue = objCommand.ExecuteScalar().ToString();
                                }
                            }


                        }

                    }
                    else
                    {




                        using (SqlCommand objCommand = new SqlCommand())
                        {
                            objCommand.Connection = connectionTrans_;
                            objCommand.CommandText = StoredProcedureName;
                            objCommand.CommandType = CommandType.StoredProcedure;
                            objCommand.Transaction = transaction_;
                            objCommand.CommandTimeout = 240;


                            SqlCommandBuilder.DeriveParameters(objCommand);

                            int i = 0;

                            foreach (SqlParameter sp in objCommand.Parameters)
                            {
                                if (sp.ParameterName == "@RETURN_VALUE")
                                {
                                    sp.Direction = ParameterDirection.ReturnValue;
                                    continue;
                                }
                                
                                 sp.Value = ParameterValues[i];
                                

                                i++;

                            }

                            if (isNonQuery)
                            {
                                objCommand.ExecuteNonQuery();
                                if (objCommand.Parameters["@RETURN_VALUE"] != null && objCommand.Parameters["@RETURN_VALUE"].Value != null)
                                    retValue = objCommand.Parameters["@RETURN_VALUE"].Value.ToString();

                            }
                            else
                            {
                                retValue = objCommand.ExecuteScalar().ToString();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

             return retValue;
            
        }

      
      


    }
}
