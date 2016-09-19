using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EasyTech.Data;
using EasyTech.Utils;

namespace Biz
{
    public class BizCenter
    {
        private const string cacheID_DataFactory = "DataFactory";
        private const string cacheID_SysConfigInfo = "SysConfigInfo";
        private static string bizProfileTag;
        private static string profileTag = "REG";
        public BizCenter()
        {
            //
            // TODO@@ 在此处添加构造函数逻辑
            //

        }
        //系统配置文件
        private static string sysConfigFileName
        {
            get
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + "APP_Data\\sys.config";
                if (!System.IO.File.Exists(fileName))
                    throw new Exception("The file 'App_Data\\Sys.config' is not exists.");
                return fileName;
            }
        }

        public static DataFactory DataFactory
        {
            get
            {
                DataFactory df;
                if (HttpRuntime.Cache[cacheID_DataFactory] == null)
                {
                    df = new DataFactory();
                    df.ConfigFileName = sysConfigFileName;
                    df.ExceptionLog =
                        new ExceptionLog(AppDomain.CurrentDomain.BaseDirectory + "APP_Data\\Log.config");
                    HttpRuntime.Cache[cacheID_DataFactory] = df;
                    //init();

                    IBizDataAccess da = df.GetDataAccessor(profileTag);
                    da.ParamMustInSql = false;
                    Dictionary<string, DataProfileRecord> dataProfiles = new Dictionary<string, DataProfileRecord>();
                    dataProfiles.Add("ValidateReg",
                         new DataProfileRecord()
                         {
                             Table = "USER_INFO",
                             SQL = "select * from USER_CHECK(@NAME,@PASSWORD)"
                         });

                    da.SetDataProfiles(dataProfiles);

                    da.ExceptionLog =
                       new ExceptionLog(AppDomain.CurrentDomain.BaseDirectory + "APP_Data\\WorkBar.Log.config");
                }
                else
                    df = (DataFactory)HttpRuntime.Cache[cacheID_DataFactory];
                return df;
            }
        }

        ////系统配置接口。内置所有的系统配置信息。
        //public static SysConfigInfo SysConfigInfo
        //{
        //    get
        //    {
        //        SysConfigInfo sysConfigInfo = null;
        //        if (HttpRuntime.Cache[cacheID_SysConfigInfo] == null)
        //        {
        //            try
        //            {
        //                sysConfigInfo = new SysConfigInfo(sysConfigFileName);
        //                HttpRuntime.Cache[cacheID_SysConfigInfo] = sysConfigInfo;
        //            }
        //            catch (Exception e)
        //            {
        //                DataFactory.AddSysLog(LogMessageType.Error, "BizCenter->SysConfigInfo", e);
        //            }
        //        }
        //        else
        //            sysConfigInfo = (SysConfigInfo)HttpRuntime.Cache[cacheID_SysConfigInfo];

        //        return sysConfigInfo;
        //    }
        //}

        public static IBizDataAccess DataAccessor
        {
            get
            {
                /*
                获得数据存取对象。
                1.WorkBar是指数据配置标识(ProfileTag)，通过ProfileConfig.aspx页面即可配置。
                2.需要在App_Data\sys.xml文件中的connectionStrings节点中，添加名为WorkBar的数据连接串，表示从该数据库中执行所有的数据操作。
                */

                IBizDataAccess da = DataFactory.GetDataAccessor(profileTag);
                return da;
            }
        }

        //public static void UpdateProfileCache(string ProfileTag)
        //{
        //    DataFactory.RefreshDataProfiles(ProfileTag);
        //}
        //internal static string HtmlEncode(string Text)
        //{
        //    if (string.IsNullOrEmpty(Text))
        //        return "";
        //    else
        //        return System.Web.HttpUtility.HtmlEncode(Text);
        //}
        //private static ConfigFile _configFile;
        //public static ConfigFile ConfigFile
        //{
        //    get
        //    {
        //        if (_configFile != null)
        //            return _configFile;
        //        return new ConfigFile(AppDomain.CurrentDomain.BaseDirectory + "APP_Data\\sys.config");
        //    }
        //}
    }
}
