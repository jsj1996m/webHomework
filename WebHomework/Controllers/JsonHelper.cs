using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace WebHomework.Controllers
{
    /// <summary>
    /// Json辅助类
    /// </summary>
    internal static class JsonHelper
    {
        #region 常量
        private static readonly JsonSerializerSettings DefaultSettings = null;
        #endregion

        #region 构造方法
        static JsonHelper()
        {
            DefaultSettings = new JsonSerializerSettings();
            DefaultSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 序列化指定实体
        /// </summary>
        /// <param name="value">实体</param>
        /// <returns>实体Json</returns>
        public static String SerializeObject(Object value)
        {
            return JsonConvert.SerializeObject(value, JsonHelper.DefaultSettings);
        }

        internal static Boolean TryDeserializeObject<T>(out T result, String json)
        {
            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch 
            {
                result = default(T);
                return false;
            }
        }

        /// <summary>
        /// 获取操作结果Json
        /// </summary>
        /// <param name="info">操作结果</param>
        /// <returns>操作结果Json</returns>
        internal static String GetOperationJson(String info)
        {
            return String.Format("{{\"result\":\"{0}\"}}", info.Replace('\r', ' ').Replace('\n', ' '));
        }

        /// <summary>
        /// 获取操作结果Json
        /// </summary>
        /// <param name="info">操作结果</param>
        /// <returns>操作结果Json</returns>
        internal static String GetOperationJson(Int32 info)
        {
            return String.Format("{{\"result\":{0}}}", info.ToString());
        }

        /// <summary>
        /// 获取操作结果Json
        /// </summary>
        /// <param name="info">操作结果</param>
        /// <returns>操作结果Json</returns>
        internal static String GetOperationJson(Boolean info)
        {
            return String.Format("{{\"result\":{0}}}", info.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// 获取操作成功结果Json
        /// </summary>
        /// <returns>操作结果Json</returns>
        internal static String GetSuccessJson()
        {
            return JsonHelper.GetOperationJson("success");
        }

        #endregion
    }
}