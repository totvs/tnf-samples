﻿using Case2.Infra.Entities;
using DapperExtensions.Mapper;

namespace Case2.Infra.Dapper
{
    public class EmployeeMapper : ClassMapper<Employee>
    {
        public EmployeeMapper()
        {
            Table("Employees");

            Map(x => x.Id).Key(KeyType.Guid);

            AutoMap();
        }
    }
}
