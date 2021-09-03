using MISA.TEST.API.Entities;
using MISA.TEST.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.TEST.API.Interfaces.Services
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        string NewEmployeeCode();

        ServiceResult DeleteListId(List<Guid> listId);

        string FormatGender(int gender);
    }
}
