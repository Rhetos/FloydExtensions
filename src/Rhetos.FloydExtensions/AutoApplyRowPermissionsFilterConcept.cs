/*
    Copyright (C) 2014 Omega software d.o.o.

    This file is part of Rhetos.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Processing.DefaultCommands;
using Rhetos.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(IConceptInfo))]
    [ConceptKeyword("AutoApplyRowPermissionsFilter")]
    public class AutoApplyRowPermissionsFilterConcept : IMacroConcept
    {
        [ConceptKey]
        public ModuleInfo Module { get; set; }

        public IEnumerable<IConceptInfo> CreateNewConcepts(IEnumerable<IConceptInfo> existingConcepts)
        {
            return new IConceptInfo[] { new AutoApplyRowPermissionsFilterWithExceptionConcept { Module = Module, Except = "" } }; 
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AutoApplyRowPermissionsFilter")]
    public class AutoApplyRowPermissionsFilterWithExceptionConcept : IConceptInfo
    {
        [ConceptKey]
        public ModuleInfo Module { get; set; }

        public string Except { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AutoApplyRowPermissionsFilterMacro : IConceptMacro<AutoApplyRowPermissionsFilterWithExceptionConcept>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AutoApplyRowPermissionsFilterWithExceptionConcept conceptInfo, IDslModel existingConcepts)
        {
            var exceptNames = conceptInfo.Except.Split(' ');
            return existingConcepts.FindByType<RowPermissionsReadInfo>()
                .Where(rpr => rpr.Source.Module == conceptInfo.Module && !exceptNames.Contains(rpr.Source.Name))
                .Select(rpr => new ApplyFilterOnClientReadWhereInfo
                {
                    DataStructure = rpr.Source,
                    FilterName = rpr.Parameter,
                    
                    // Ako se dohvaća točno jedan zapis po IDju, onda se ne primjenjuje automatski filter po row permissionima.
                    // Ako user nema prava za taj zapis očekuje se standardna greška od row permissiona.
                    Where = @"command => !Rhetos.FloydExtensions.AutoApplyRowPermissionsFilterMacro.IsFilterById(command)"
				});
        }

        public static bool IsFilterById(ReadCommandInfo command)
        {
            if (command.Filters == null || command.Filters.Count() != 1)
                return false;

            var f = command.Filters[0];

            bool specificFilterById = // Specific filter IEnumerable<Guid> (or derivation) with 1 guid:
                f.Filter != null
                && f.Value != null
                && f.Value as IEnumerable<Guid> != null
                && ((IEnumerable<Guid>)f.Value).Count() == 1;
        
            bool genericFilterById = // Generic filter on property ID
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

        [Export(typeof(IConceptMacro))]
        public class AutoApplyRowPermissionsFilterGlobalMacro : IConceptMacro<ModuleInfo>
        {
	        private readonly IConfiguration _configuration;

	        public AutoApplyRowPermissionsFilterGlobalMacro(IConfiguration configuration)
	        {
		        _configuration = configuration;
	        }

	        public IEnumerable<IConceptInfo> CreateNewConcepts(ModuleInfo moduleInfo, IDslModel existingConcepts)
	        {
		        if (!_configuration.GetBool("FloydExtensions:AutoApplyRowPermissionsFilterGlobally", false).Value)
			        return new IConceptInfo[] { };

		        return new[] { new AutoApplyRowPermissionsFilterWithExceptionConcept
		        {
			        Module = moduleInfo,
			        Except = string.Empty
		        } };
	        }
        }
	}
}
