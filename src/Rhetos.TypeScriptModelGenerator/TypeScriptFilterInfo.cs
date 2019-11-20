using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace Rhetos.TypeScriptModelGenerator
{
    [Export(typeof(IConceptInfo))]
    public class TypeScriptFilterInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class FilterByConceptMacro : IConceptMacro<FilterByInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(FilterByInfo conceptInfo, IDslModel existingConcepts)
        {
            var dataStructure = existingConcepts.FindByType<DataStructureInfo>()
                .FirstOrDefault(x => x.FullName == conceptInfo.Parameter);

            if (dataStructure == null)
            {
                dataStructure = existingConcepts.FindByType<DataStructureInfo>()
                    .FirstOrDefault(x => x.FullName == $"{conceptInfo.Source.Module}.{conceptInfo.Parameter}");
            }

            var result = new List<IConceptInfo>();

            if (dataStructure != null)
                result.Add(new TypeScriptFilterInfo{DataStructure = dataStructure});

            return result;
        }
    }
}