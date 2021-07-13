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

namespace Rhetos.FloydExtensions
{
	[Export(typeof(IConceptInfo))]
    [ConceptKeyword("AutoApplyRowPermissionsFilter")]
    public class AutoApplyRowPermissionsFilterConcept : IMacroConcept
    {
        [ConceptKey]
        public ModuleInfo Module { get; set; }

        public IEnumerable<IConceptInfo> CreateNewConcepts()
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
                    
                    // When reading only one record by ID, automatic filtering by user's row permissions will not be applied.
                    // If the user does not have read permissions for that record, standar row permissions error response will be returned.
                    Where = @"command => !Rhetos.FloydExtensions.AutoApplyRowPermissionsFilterMacro.IsFilterById(command)"
				});
        }

        public static bool IsFilterById(ReadCommandInfo command)
        {
            if (command.Filters == null || command.Filters.Length != 1)
                return false;

            var f = command.Filters[0];

            bool specificFilterById = // Specific filter IEnumerable<Guid> (or derivation) with 1 GUID:
                f.Filter != null
                && f.Value != null
                && f.Value is IEnumerable<Guid> enumerable
                && enumerable.Count() == 1;
        
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
            if (!_configuration.GetValue("FloydExtensions:AutoApplyRowPermissionsFilterGlobally", false))
                return Array.Empty<IConceptInfo>();

            return new[] { new AutoApplyRowPermissionsFilterWithExceptionConcept
                {
                    Module = moduleInfo,
                    Except = string.Empty
                } };
        }
    }
}
