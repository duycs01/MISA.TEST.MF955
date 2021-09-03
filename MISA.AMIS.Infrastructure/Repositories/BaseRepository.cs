using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.TEST.API.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.TEST.Core.MISAAttribute.MISAAttribute;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>, IDisposable
    {
        #region Declaration
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection;
        string _tableName;
        #endregion

        #region Contructor

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        /// CreateBy duylv - 27/08/2021
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISATESTLocalConnectionString");
            _tableName = typeof(MISAEntity).Name;
            _dbConnection = new MySqlConnection(_connectionString);
            _dbConnection.Open();
        }

        #endregion

        #region Method

        /// <summary>
        /// Xóa theo id
        /// </summary>
        /// <param name="entityId">Id cần xóa</param>
        /// <returns>Trả về số bản ghi xóa được</returns>
        /// CreateBy duylv - 27/08/2021
        public int DeleteById(Guid entityId)
        {
            // Khai báo Dynamic Param
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{_tableName}Id", entityId);
            var sqlCommand = $"DELETE FROM {_tableName} WHERE {_tableName}Id=@{_tableName}Id";
            var res = _dbConnection.Execute(sqlCommand, dynamicParameters);
            return res;
        }

        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns>Trả về danh sách bản ghi</returns>
        /// CreateBy duylv - 27/08/2021
        /// 
        public List<MISAEntity> GetAll()
        {
            var sqlCommand = $"SELECT * FROM {_tableName}";
            var res = _dbConnection.Query<MISAEntity>(sqlCommand);
            return (List<MISAEntity>)res;
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <returns>Trả về thông tin bản ghi</returns>
        /// CreateBy duylv - 27/08/2021
        public MISAEntity GetById(Guid entityId)
        {
            var _tableName = typeof(MISAEntity).Name;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            var sqlCommand = $"SELECT * FROM {_tableName} WHERE {_tableName}Id = @{_tableName}Id LIMIT 1";
            var rowEffect = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters);
            return rowEffect;
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>Số bản ghi thêm mới</returns>
        /// CreateBy duylv - 27/08/2021
        /// 
        public int Insert(MISAEntity entity)
        {
            var parameters = MappingDBType(entity);
            var res = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);
            return res;
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>Số bản ghi thêm được</returns>
        /// CreateBy duylv - 27/08/2021
        /// 
        public int Update(MISAEntity entity)
        {
            var parameters = MappingDBType(entity);
            var res = _dbConnection.Execute($"Proc_Update{_tableName}", param: parameters, commandType: CommandType.StoredProcedure);
            return res;
        }

        /// <summary>
        /// Gán giá trị cho parameters
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>parameter đã được gán các giá trị của prop</returns>
        /// CreateBy duylv - 27/08/2021
        /// 
        protected DynamicParameters MappingDBType(MISAEntity entity)
        {
            //Đọc từng property của object:
            var properties = entity.GetType().GetProperties();
            DynamicParameters dynamicParameters = new DynamicParameters();

            //Duyệt từng property
            foreach (var prop in properties)
            {
                var propAttributeNotMap = prop.GetCustomAttributes(typeof(MISANotMap), true);
                if (propAttributeNotMap.Length == 0)
                {
                    // Lấy tên của prop
                    var propName = prop.Name;

                    // Lấy value của prop
                    var propValue = prop.GetValue(entity);

                    // Lấy kiểu bản ghi của prop
                    var propType = prop.PropertyType;

                    // Thêm param tương ứng với mỗi property của đối tượng
                    dynamicParameters.Add($"@{propName}", propValue);
                }
            }
            return dynamicParameters;
        }

        /// <summary>
        /// Đóng kết nối
        /// CreateBy duylv - 27/08/2021
        /// </summary>
        public void Dispose()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }
        #endregion

    }
}
