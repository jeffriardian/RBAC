﻿using System.Diagnostics.CodeAnalysis;

namespace RBAC.Core.ViewModel
{
    [ExcludeFromCodeCoverage]
    public partial class ExampleViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string RowStatus { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
