using System;

namespace DASP.Domain.Entitys
{
    public class TBUserEntity : BaseEntity
    {
        /// <summary>
        /// �û��� -- ����
        /// </summary>
        public virtual Guid UserId
        {
            get;
            set;
        }

        /// <summary>
        /// �û���
        /// </summary>
        public virtual string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// ��¼��
        /// </summary>
        public virtual string LoginName
        {
            get;
            set;
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public virtual string UserPassword
        {
            get;
            set;
        }


        /// <summary>
        /// �绰
        /// </summary>
        public virtual string UserTel
        {
            get;
            set;
        }

        /// <summary>
        /// ��ַ
        /// </summary>
        public virtual string UserAddress
        {
            get;
            set;
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual string UserEmail
        {
            get;
            set;
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public virtual string UserRemark
        {
            get;
            set;
        }

        /// <summary>
        /// ��ɫ
        /// </summary>
        public virtual TBRoleEntity Role 
        { 
            get;
            set;
        }
    }
}