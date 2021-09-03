using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.TEST.API.Entities;
using MISA.TEST.API.Interfaces.Repository;
using MISA.TEST.API.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.TEST.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseEntityController<Department>
    {
        IDepartmentService _departmentService;
        IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentService departmentService, IDepartmentRepository departmentRepository) : base(departmentService, departmentRepository)
        {
            _departmentService = departmentService;
            _departmentRepository = departmentRepository;
        }
    }
}
