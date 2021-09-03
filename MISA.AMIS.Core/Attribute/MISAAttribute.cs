using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.TEST.Core.MISAAttribute
{
    public class MISAAttribute
    {
        

        /// <summary>
        /// Sử dụng thuộc tính tên tiếng việt cho property
        /// CreatedBy: mf955-duylv - 27/08/2021
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class MISAExportProperty:Attribute
        {
            public string FieldName = string.Empty;
            public MISAExportProperty(string fieldName)
            {
                FieldName = fieldName;
            }
        }

        /// <summary>
        /// Không nhận các property truyền lên cho database
        /// CreatedBy: mf955-duylv - 27/08/2021
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class MISANotMap : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class MISARequired : Attribute
        {
        }
        
    }
}
