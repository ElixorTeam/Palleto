// 1. .NET
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Reflection;

// 2. Microsoft
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;

// 3. External
// pass

// 4. Internal
global using Pl.Database.Common;
global using Pl.Database.Shared.Constants;

global using Pl.Database.Entities.Print.Labels;
global using Pl.Database.Entities.Print.LabelsZpl;
global using Pl.Database.Entities.Print.Pallets;
global using Pl.Database.Entities.Ref.PalletMen;
global using Pl.Database.Entities.Ref.Printers;
global using Pl.Database.Entities.Ref.ProductionSites;
global using Pl.Database.Entities.Ref.Users;
global using Pl.Database.Entities.Ref.Warehouses;
global using Pl.Database.Entities.Ref1C.Boxes;
global using Pl.Database.Entities.Ref1C.Brands;
global using Pl.Database.Entities.Ref1C.Bundles;
global using Pl.Database.Entities.Ref1C.Characteristics;
global using Pl.Database.Entities.Ref1C.Clips;
global using Pl.Database.Entities.Ref1C.Nestings;
global using Pl.Database.Entities.Ref1C.Plus;
global using Pl.Database.Entities.Zpl.Templates;
global using Pl.Database.Entities.Zpl.ZplResources;

global using Pl.Database.Views.Diag.DatabaseTables;

// 5. Modules
global using Pl.Shared.Utils;