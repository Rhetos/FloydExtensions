using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Processing.DefaultCommands;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(IConceptMacro))]
    public class AutoApplyRowPermissionsFilterMacro : IConceptMacro<ModuleInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(ModuleInfo moduleInfo, IDslModel existingConcepts)
        {
            var result = existingConcepts.FindByType<RowPermissionsReadInfo>()
                .Where(rpr => rpr.Source.Module == moduleInfo)
                .Where(rpr => existingConcepts.FindByKey($"ApplyFilterOnClientReadWhereInfo {rpr.Source.Module.Name}.{rpr.Source.Name}.'{rpr.Parameter}'") == null)
                .Select(rpr => new ApplyFilterOnClientReadWhereInfo
                {
                    DataStructure = rpr.Source,
                    FilterName = rpr.Parameter,
                    
                    // Ako se dohvaća točno jedan zapis po IDju, onda se ne primjenjuje automatski filter po row permissionima.
                    // Ako user nema prava za taj zapis očekuje se standardna greška od row permissiona.
                    Where = @"command => !Rhetos.FloydExtensions.AutoApplyRowPermissionsFilterMacro.IsFilterById(command)"
                });

            return result;
        }

        public static bool IsFilterById(ReadCommandInfo command)
        {
            if (command.Filters == null || command.Filters.Length != 1)
                return false;

            var f = command.Filters[0];

            var specificFilterById = // Specific filter IEnumerable<Guid> (or derivation) with 1 guid:
                f.Filter != null
                && f.Value != null
                && f.Value is IEnumerable<Guid> guids
                && guids.Count() == 1;
        
            var genericFilterById = // Generic filter on property ID
                f.Property != null
                && f.Operation != null
                && f.Property.Equals("ID", StringComparison.OrdinalIgnoreCase)
                &&
                (
                    f.Operation.Equals("equals", StringComparison.OrdinalIgnoreCase)
                    || f.Operation.Equals("equal", StringComparison.OrdinalIgnoreCase)
                );

            return specificFilterById || genericFilterById;
        }
    }
}
