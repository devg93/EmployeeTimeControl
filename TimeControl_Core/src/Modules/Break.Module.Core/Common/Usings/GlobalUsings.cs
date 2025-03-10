
//*****************************Commons Services*********************************************
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

global using Modules.Break.Module.Core.Astractions.Iservices;
global using Modules.Break.Module.Core.Dto;
global using Modules.Break.Module.Core.Entity;
global using Shared.Dto;
global using Shared.Services.Tasks.ShedulerTuplelog.Enum;
global using Break.Module.Core.Abstraction.IServiceProvider;
global using Modules.Break.Module.Core.Astractions.Irepository;
global using Shared.Services.ModuleCommunication.Contracts;
global using Shared.Services.RunTime;
global using Modules.Break.Module.Core.Iservices;
global using Shared.Services.Tasks.PingCheker;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using Break.Module.Core.BreakWorker.CommonServices;

global using Break.Module.Core.DAL.GetNewServicesFactory;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
 
global using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;

global using Modules.Break.Module.Core.BreakWorker.Command;
global using Modules.Break.Module.Core.DAL;



global using Modules.Break.Module.Core.Repository.Busy.DAL;
global using Modules.Break.Module.Core.Repository.DAL;


global using System.Threading;

global using Shared.Services.Tasks.ShedulerTuplelog;
global using Microsoft.EntityFrameworkCore.Design;

global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;


//*********************************************************************************
