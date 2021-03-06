using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Visitor.GetClasses
{
    public class GetClassesViewModel
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