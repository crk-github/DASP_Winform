using System;

namespace DASP.Domain.Entitys
{

    /// <summary>
    /// ϵͳ��־��
    /// </summary> 
    public class TBSysLogEntity : BaseEntity
    {
        /// <summary>
        /// ����
        /// </summary>
        public virtual Guid SyslogId
        {
            get;
            set;
        }
      
        /// <summary>
        /// ��־����ʱ��
        /// </summary>
        public virtual string LogDatetime
        {
            get;
            set;
        }

        /// <summary>
        /// ��־����
        /// </summary>
        public virtual string LogContent
        {
            get;
            set;
        }

        /// <summary>
        /// ģ������
        /// </summary>
        public virtual string LogModuleName
        {
            get;
            set;
        }

        /// <summary>
        /// ��־����
        /// </summary>
        public virtual int LogTypeId
        {
            get;
            set;
        }
    }
}