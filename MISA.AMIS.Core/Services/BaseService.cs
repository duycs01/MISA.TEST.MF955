using MISA.TEST.API.Interfaces.Repository;
using MISA.TEST.API.Interfaces.Services;
using MISA.TEST.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.TEST.Core.MISAAttribute.MISAAttribute;

namespace MISA.TEST.API.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        #region Declaration

        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceResult;
        string _tableName;

        #endregion

        #region Contructor

        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
            _tableName = typeof(MISAEntity).Name;

        }

        #endregion

        #region Method

        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns>Trả về số bản ghi xóa được</returns>
        /// CreatedBy: duylv - 27/08/2021
        public ServiceResult DeleteById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.DeleteById(entityId);
            return _serviceResult;
        }

        /// <summary>
        /// Lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Trả về danh sách bản ghi</returns>
        /// CreatedBy: duylv - 27/08/2021
        public ServiceResult GetAll()
        {
            _serviceResult.Data = _baseRepository.GetAll();
            return _serviceResult;
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <returns>Trả về thông tin bản ghi theo id</returns>
        /// CreatedBy: duylv - 27/08/2021
        public ServiceResult GetById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.GetById(entityId);
            return _serviceResult;
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>Số bản ghi thêm được</returns>
        /// CreatedBy: duylv - 27/08/2021
        public ServiceResult Insert(MISAEntity entity)
        {
            var isValid = ValidateData(entity);
            if (isValid == true)
            {
                isValid = ValidateCustom(entity);
            }
            if (isValid == true)
            {
                _serviceResult.Data = _baseRepository.Insert(entity);
            }
            return _serviceResult;
        }

        public ServiceResult Update(Guid entityId, MISAEntity entity)
        {
            var isValid = ValidateData(entity);

            var tableId = $"{_tableName}Id";

            var prop = entity.GetType().GetProperty(tableId);
            prop.SetValue(entity, entityId);
           
            if (isValid == true)
            {
                isValid = ValidateCustom(entity);
            }
            if (isValid == true)
            {
                _serviceResult.Data = _baseRepository.Update(entity);
            }
            return _serviceResult;
        }

        /// <summary>
        /// Sử lí validate chung
        /// </summary>
        /// <param name="entity">Đối tượng muốn validate</param>
        /// <returns>True bản ghi hợp lệ - False bản ghi không hợp lệ</returns>
        /// CreatedBy: duylv - 27/08/2021
        protected bool ValidateData(MISAEntity entity)
        {
            // validate Bắt buộc nhập
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                var propType = prop.PropertyType;

                var propMISARequired = prop.GetCustomAttributes(typeof(MISARequired), true);
                var propMISAExportProperty = prop.GetCustomAttributes(typeof(MISAExportProperty), true);

                var fieldName = string.Empty;
                if ( propMISAExportProperty.Length > 0)
                {
                     fieldName = (propMISAExportProperty[0] as MISAExportProperty).FieldName;
                }

                if (propMISARequired.Length > 0)
                {
                    if (propType == typeof(string) && propValue == null || propValue.ToString() == string.Empty)
                    {
                        _serviceResult.IsValid = false;
                        _serviceResult.Messenger = $"Thông tin {fieldName} không được phép để trống";
                    }
                }
                if (propType == typeof(DateTime?) && propValue != null)
                {
                    var dateNow = DateTime.Now;
                    int result = DateTime.Compare((DateTime)propValue, dateNow);
                    if (result > 0)
                    {
                        _serviceResult.IsValid = false;
                        _serviceResult.Messenger = $"{fieldName} không được lớn hơn ngày hiện tại";
                    }
                }

            }
            return _serviceResult.IsValid;
        }

        /// <summary>
        /// Sử lí validate riêng
        /// </summary>
        /// <param name="entity">Đối tượng muốn validate</param>
        /// <returns>True bản ghi hợp lệ - False bản ghi không hợp lệ</returns>
        /// Created by: duylv - 27/08/2021
        protected virtual bool ValidateCustom(MISAEntity entity)
        {
            return true;
        }
        #endregion
    }
}
