using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageClass.GetClasses
{
    public class GetClassesViewModel : OperationResultViewModel
    {
        public IEnumerable<ClassModel> Classes { get; set; }

        public GetClassesViewModel(IEnumerable<Class> classes)
        {
            Classes = from @class in classes
                select new ClassModel
                {
                    Id = @class.Id,
                    Name = @class.Name
                };
        }
    }
}