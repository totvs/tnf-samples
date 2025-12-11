using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.Domain;

[SxRoutine(RoutineName = "DepartamentRoutine", Title = "DepartamentRoutine", Version = "1.0")]
public class DepartmentRoutineUI : DepartmentEntity, ISXRoutineLayoutDataOperations
{
    public void ConfigureLayout(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataEdit(x => x
            .WithIdentifier("dataEditDepartment")
            .WithTitle("Editar Departamento")
            .WithElementsBase("dataNewCompany")
            .AddSection(x => x
                .WithIdentifier("sectionDepartment")
                .WithTitle("Dados do Departamento")
                .AddElement(x => x.WithProperty(nameof(Name)))
                .AddElement(x => x.WithProperty(nameof(LastModificationTime)))
            )
            .SetProperty(x => x.WithProperty(nameof(Id)).WithHidden(true))
            .SetProperty(x => x.WithProperty(nameof(CompanyId)).WithHidden(true))
            .SetProperty(x => x.WithProperty(nameof(CreationTime)).WithHidden(true))
        );
    }
}
